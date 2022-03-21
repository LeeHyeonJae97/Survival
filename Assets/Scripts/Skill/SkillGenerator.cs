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


// DEPRECATED

//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//public class SkillGenerator : SingletonMonoBehaviour<SkillGenerator>
//{
//    [SerializeField] private SkillProperty[] _properties;
//    [SerializeField] private SkillInvocation[] _invocations;
//    [SerializeField] private SkillTargeting[] _targetings;
//    [SerializeField] private SkillProjection[] _projections;
//    [SerializeField] private SkillHit[] _hits;

//    private int _propertiesLength = System.Enum.GetValues(typeof(SkillPropertyType)).Length;
//    private int _invocationsLength = System.Enum.GetValues(typeof(SkillInvocationType)).Length;
//    private int _targetingsLength = System.Enum.GetValues(typeof(SkillTargetingType)).Length;
//    private int _projectionsLength = System.Enum.GetValues(typeof(SkillProjectionType)).Length;
//    private int _hitsLength = System.Enum.GetValues(typeof(SkillHitType)).Length;

//    protected override void Awake()
//    {
//        base.Awake();

//        // DEPRECATED
//        _propertiesLength = _properties.Length;
//        _invocationsLength = _invocations.Length;
//        _targetingsLength = _targetings.Length;
//        _projectionsLength = _projections.Length;
//        _hitsLength = _hits.Length;

//        // TODO :
//        // check error
//        //CheckLengthError();

//        // sorting
//        _properties = _properties.OrderBy((e) => (int)e.Type).ToArray();
//        _invocations = _invocations.OrderBy((e) => (int)e.Type).ToArray();
//        _targetings = _targetings.OrderBy((e) => (int)e.Type).ToArray();
//        _projections = _projections.OrderBy((e) => (int)e.Type).ToArray();
//        _hits = _hits.OrderBy((e) => (int)e.Type).ToArray();

//        // check error
//        //CheckElementErorr();

//        // create a pool for projectiles
//        PoolingManager.Instance.Create("SkillProjectile", "SkillProjectile", 10);

//        // DEPRECATED
//        Skill skill = Generate();
//        Player.Instance.Skills.Add(skill);
//        skill.Invoke(Player.Instance);
//    }

//    public Skill Generate()
//    {
//        SkillProperty property = _properties[Random.Range(0, _propertiesLength)];
//        SkillInvocation invocation = _invocations[Random.Range(0, _invocationsLength)];
//        SkillTargeting targeting = _targetings[Random.Range(0, _targetingsLength)];
//        SkillProjection projection = invocation is SkillInvocationProjection ? _projections[Random.Range(0, _projectionsLength)] : null;
//        SkillHit hit = _hits[Random.Range(0, _hitsLength)];

//        return new Skill(property, invocation, targeting, projection, hit);
//    }

//    private void CheckLengthError()
//    {
//        if (_properties.Length != _propertiesLength) Debug.LogError("there are more or less properties");
//        if (_invocations.Length != _invocationsLength) Debug.LogError("there are more or less invocations");
//        if (_targetings.Length != _targetingsLength) Debug.LogError("there are more or less targetings");
//        if (_projections.Length != _projectionsLength) Debug.LogError("there are more or less projections");
//        if (_hits.Length != _hitsLength) Debug.LogError("there are more or less hits");
//    }

//    private void CheckElementErorr()
//    {
//        for (int i = 0; i < _properties.Length; i++)
//        {
//            if ((int)_properties[i].Type != i) Debug.LogError("there's wrong property");
//        }
//        for (int i = 0; i < _invocations.Length; i++)
//        {
//            if ((int)_invocations[i].Type != i) Debug.LogError("there's wrong invocation type");
//        }
//        for (int i = 0; i < _targetings.Length; i++)
//        {
//            if ((int)_targetings[i].Type != i) Debug.LogError("there's wrong targeting type");
//        }
//        for (int i = 0; i < _projections.Length; i++)
//        {
//            if ((int)_projections[i].Type != i) Debug.LogError("there's wrong projection type");
//        }
//        for (int i = 0; i < _hits.Length; i++)
//        {
//            if ((int)_hits[i].Type != i) Debug.LogError("there's wrong hit type");
//        }
//    }
//}
