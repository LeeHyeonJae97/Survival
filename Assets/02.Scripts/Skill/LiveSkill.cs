using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveSkill
{
    public int Level { get; private set; }
    public SkillSO Skill { get; private set; }

    public LiveSkill(SkillSO skill)
    {
        Level = 0;
        Skill = skill;
    }

    public void LevelUp()
    {
        Level++;
    }

    public void Invoke(Player player)
    {
        if (Skill.Stats[Level].Cooldown == 0)
        {
            player.StartCoroutine(Skill.Invocation?.CoInvoke(this));
        }
        else
        {
            player.StartCoroutine(CoInvoke());
        }
    }

    private IEnumerator CoInvoke()
    {
        while (true)
        {
            // wait for cooldown
            yield return WaitForSecondsFactory.Get(Skill.Stats[Level].Cooldown);

            // invoke skill
            Player.Instance.StartCoroutine(Skill.Invocation?.CoInvoke(this));
        }
    }
}
