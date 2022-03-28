using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : SingletonMonoBehaviour<PoolingManager>
{
    [System.Serializable]
    public class Pool
    {
        [field: SerializeField] public string Key { get; private set; }

        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _amount;
        private Queue<GameObject> _queue;

        public Pool(string key, string path, int amount)
        {
            Key = key;

            _prefab = Resources.Load<GameObject>(path);
            if (_prefab == null)
            {
                Debug.LogError($"There's no prefab at {path}");
                return;
            }

            _amount = amount;

            Init();
        }

        public void Init()
        {
            _queue = new Queue<GameObject>();
            _prefab.SetActive(false);
            for (int i = 0; i < _amount; i++)
            {
                GameObject go = Instantiate(_prefab);
                go.transform.SetParent(Instance.transform);
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

    [SerializeField] private Pool[] _pools;
    private Dictionary<string, Pool> _dic = new Dictionary<string, Pool>();

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < _pools.Length; i++)
        {
            _pools[i].Init();
            _dic.Add(_pools[i].Key, _pools[i]);
        }
    }

    public void Create<T>(string key = null, string path = null, int amount = 5)
    {
        if (string.IsNullOrEmpty(key)) key = typeof(T).ToString();
        if (string.IsNullOrEmpty(path)) path = typeof(T).ToString();

        _dic.Add(key, new Pool(key, path, amount));
    }

    public void Create(string key, string path, int amount = 5)
    {
        _dic.Add(key, new Pool(key, path, amount));
    }

    public void Destroy(string key)
    {
        if (_dic.TryGetValue(key, out Pool pool)) pool.Destroy();
    }

    public GameObject Spawn(string key)
    {
        return _dic.TryGetValue(key, out Pool pool) ? pool.Spawn() : null;
    }

    public T Spawn<T>(string key = null) where T : Component
    {
        return Spawn(string.IsNullOrEmpty(key) ? typeof(T).ToString() : key).GetComponent<T>();
    }

    public void Despawn(GameObject go)
    {
        if (_dic.TryGetValue(go.name, out Pool pool))
        {
            go.transform.SetParent(transform);
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
