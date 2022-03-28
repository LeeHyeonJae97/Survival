using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillInvocationType { Projection, Spawn }

public abstract class SkillInvocation : ScriptableObject
{
    public const string FILE_NAME = "Skill Invocation ";
    public const string MENU_NAME = "ScriptableObject/Skill/Invocation/";

    [field: SerializeField] public SkillInvocationType Type { get; protected set; }

    public abstract IEnumerator CoInvoke(Skill skill);
}
