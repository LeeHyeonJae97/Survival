using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Curse", menuName = MENU_NAME + "Curse")]
public class SkillPropertyCurse : SkillProperty
{
    [SerializeField] private int _percent;
    [SerializeField] private float _duration;

    private void OnValidate()
    {
        Type = SkillPropertyType.Curse;
    }

    public override void OnHit(SkillProjectile projectile, Enemy enemy)
    {
        // check aroused
        if (RandomExtension.CheckPercent(_percent))
        {
            IEnumerator cor = CoCursed(enemy);

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

    private IEnumerator CoCursed(Enemy enemy)
    {
        // change enemy's movement type

        yield return WaitForSecondsFactory.Get(_duration);

        // recover enemy's movement type
    }
}
