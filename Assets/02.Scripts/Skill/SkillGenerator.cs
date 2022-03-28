using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillGenerator : SingletonMonoBehaviour<SkillGenerator>
{
    [SerializeField] private Skill[] _skills;

    private void Start()
    {
        PoolingManager.Instance.Create("SkillProjectile", "SkillProjectile", 10);

        Generate();
    }

    public Skill Generate()
    {
        // DEPRECATED
        Skill skill = _skills[Random.Range(0, _skills.Length)];
        Player.Instance.Skills.Add(skill);
        skill.Invoke();
        return skill;
    }
}