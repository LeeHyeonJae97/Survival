using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : SingletonMonoBehaviour<PoolingManager>
{
    public class Pool
    {
        private GameObject _prefab;
        private Queue<GameObject> _queue;

        public Pool(string path, int amount)
        {
            _prefab = Resources.Load<GameObject>(path);
            if (_prefab == null)
            {
                Debug.LogError($"There's no prefab at {path}");
                return;
            }

            _queue = new Queue<GameObject>();
            _prefab.SetActive(false);
            for (int i = 0; i < amount; i++)
            {
                GameObject go = Instantiate(_prefab);
                go.name = _prefab.name;
                _queue.Enqueue(go);
            }
            _prefab.SetActive(true);
        }

        public GameObject Spawn()
        {
            GameObject go;
            if (_queue.Count > 0)
            {
                go = _queue.Dequeue();
                go.gameObject.SetActive(true);
            }
            else
            {
                go = Instantiate(_prefab);
                go.name = _prefab.name;
            }
            return go;
        }

        public void Despawn(GameObject go)
        {
            go.gameObject.SetActive(false);
            _queue.Enqueue(go);
        }

        public void Destroy()
        {
            while (_queue.Count > 0) GameObject.Destroy(_queue.Dequeue());
        }
    }

    private Dictionary<string, Pool> _dic = new Dictionary<string, Pool>();

    public void Create(string key, string path, int amount = 5)
    {
        _dic.Add(key, new Pool(path, amount));
    }

    public void Destroy(string key)
    {
        if (_dic.TryGetValue(key, out Pool pool)) pool.Destroy();
    }

    public GameObject Spawn(string key)
    {
        return _dic.TryGetValue(key, out Pool pool) ? pool.Spawn() : null;
    }

    public T Spawn<T>(string key) where T : Component
    {
        return Spawn(key).GetComponent<T>();
    }

    public void Despawn(GameObject go)
    {
        if (_dic.TryGetValue(go.name, out Pool pool))
        {
            pool.Despawn(go);
        }
        else
        {
            Destroy(go);
        }
    }

    public void Despawn<T>(T t) where T : Component
    {
        Despawn(t.gameObject);
    }
}
