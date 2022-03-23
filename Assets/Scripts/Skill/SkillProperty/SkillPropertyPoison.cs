using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Poison", menuName = MENU_NAME + "Poison")]
public class SkillPropertyPoison : SkillProperty
{
    [SerializeField] private int _percent;
    [SerializeField] private float _duration;
    [SerializeField] private float _continuous;
    [SerializeField] private float _interval;

    private void OnValidate()
    {
        Type = SkillPropertyType.Poison;
    }

    public override void OnHit(SkillProjectile projectile, EnemyPlayer enemy)
    {
        // check aroused
        if (RandomExtension.CheckPercent(_percent))
        {
            IEnumerator cor = CoDamageContinuously(enemy);

            // check already applied
            if (enemy.CrowdControlCorDic.ContainsKey(Type))
            {
                // stop already ongoing one
                enemy.StopCoroutine(enemy.CrowdControlCorDic[Type]);
                enemy.CrowdControlCorDic[Type] = cor;
            }
            else
            {
                enemy.CrowdControlCorDic.Add(Type, cor);
            }

            // start coroutine
            enemy.StartCoroutine(cor);
        }
    }

    private IEnumerator CoDamageContinuously(EnemyPlayer enemy)
    {
        float elapsed = 0;
        float continuous = _continuous;

        while (elapsed < _duration)
        {
            enemy.HP -= (int)continuous;
            yield return WaitForSecondsFactory.Get(_interval);

            elapsed += _interval;
            // increase continuous
            continuous += 0.5f;
        }
    }
}
