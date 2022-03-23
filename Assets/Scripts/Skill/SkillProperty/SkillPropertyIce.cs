using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Ice", menuName = MENU_NAME + "Ice")]
public class SkillPropertyIce : SkillProperty
{
    [SerializeField] private int _percent;
    [SerializeField] private float _duration;
    [SerializeField] private float _slow;

    private void OnValidate()
    {
        Type = SkillPropertyType.Ice;
    }

    public override void OnHit(SkillProjectile projectile, Enemy enemy)
    {
        // check aroused
        int percent = RandomExtension.percent;

        // TODO :
        // need to set each percent for freezing and slowing down

        // get damaging coroutine by percent
        IEnumerator cor = percent < _percent ? percent < _percent / 2 ? CoFreeze(enemy) : CoSlowDown(enemy) : null;

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

    private IEnumerator CoSlowDown(Enemy enemy)
    {
        enemy.Speed -= _slow;
        yield return WaitForSecondsFactory.Get(_duration);

        enemy.Speed += _slow;
    }

    private IEnumerator CoFreeze(Enemy enemy)
    {
        float org = enemy.Speed;
        enemy.Speed = -Enemy.MIN_SPEED;
        yield return WaitForSecondsFactory.Get(_duration);

        enemy.Speed = org;
    }
}
