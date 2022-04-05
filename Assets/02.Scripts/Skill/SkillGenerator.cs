using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillGenerator : SingletonMonoBehaviour<SkillGenerator>
{
    [SerializeField] private SkillSO[] _skills;

    private void Start()
    {
        PoolingManager.Instance.Create("SkillProjectile", "SkillProjectile", 10);

        Generate();
    }

    public LiveSkill Generate()
    {
        // DEPRECATED
        LiveSkill skill = new LiveSkill(_skills[Random.Range(0, _skills.Length)]);
        Player.Instance.Equip(skill);
        skill.Invoke(Player.Instance);
        
        return null;
    }
}