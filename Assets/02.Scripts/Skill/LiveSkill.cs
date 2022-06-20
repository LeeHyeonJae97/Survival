using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveSkill
{
    public int Level { get; private set; }
    public Skill Skill { get; private set; }

    public LiveSkill(Skill skill)
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
        // CONTINUE :
        // index out of range error occurs
        // + ui error when wave finished

        if (Skill.Info.Stats[Level].Stats[Skill.Reinforced].Cooldown == 0)
        {
            player.StartCoroutine(Skill.Info.Invocation?.CoInvoke(this));
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
            yield return WaitForSecondsFactory.GetPlayTime(Skill.Info.Stats[Level].Stats[Skill.Reinforced].Cooldown);

            // invoke skill
            Player.GetInstance().StartCoroutine(Skill.Info.Invocation?.CoInvoke(this));
        }
    }
}
