using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Fire", menuName = MENU_NAME + "Fire")]
public class SkillPropertyFire : SkillProperty
{
    [SerializeField] private int _percent;
    [SerializeField] private float _duration;
    [SerializeField] private float _continuous;
    [SerializeField] private float _interval;

    private void OnValidate()
    {
        Type = SkillPropertyType.Fire;
    }

    public override void OnHit(SkillProjectile projectile, EnemyPlayer enemy)
    {
        // check constraint
        if (enemy.Enemy.Constraint == Constraint.Fire) return;

        // check aroused
        int percent = RandomExtension.percent;

        // get damaging coroutine by percent
        IEnumerator cor = percent < _percent ? CoDamageContinuously(percent < _percent / 2 ? _continuous * 2 : _continuous, enemy) : null;

        if (cor != null)
        {
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

    private IEnumerator CoDamageContinuously(float continuous, EnemyPlayer enemy)
    {
        float elapsed = 0;

        while (elapsed < _duration)
        {
            enemy.HP -= (int)continuous;
            yield return WaitForSecondsFactory.Get(_interval);

            elapsed += _interval;
        }
    }
}
