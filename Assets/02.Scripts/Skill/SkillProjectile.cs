using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile : MonoBehaviour
{
    public SkillStat Stat { get; private set; }
    public SkillProperty Property { get; private set; }
    public SkillTargeting Targeting { get; private set; }
    public SkillProjection Projection { get; private set; }
    public SkillHit Hit { get; private set; }
    public SkillOnHit OnHit { get; private set; }
    public float Elapsed { get { return _birth == 0 ? -1 : Time.time - _birth; } }

    [SerializeField] private SpriteRenderer _sr;
    private float _birth;

    private void OnDisable()
    {
        // reset
        Stat = default;
        Property = default;
        Targeting = default;
        Projection = default;
        Hit = default;
        _birth = default;

        transform.DOKill(false);
    }

    private void Update()
    {
        Projection?.Projection_Update(this);
    }

    public void Init(LiveSkill liveSkill)
    {
        _sr.sprite = liveSkill.Skill.ProjectileSprite;

        Stat = liveSkill.Skill.Stats[liveSkill.Level];
        Property = liveSkill.Skill.Property;
        Targeting = liveSkill.Skill.Targeting;
        Projection = liveSkill.Skill.Projection;
        Hit = liveSkill.Skill.Hit;
        OnHit = liveSkill.Skill.OnHit;

        if (Stat == default) Debug.LogError("Stat must not be null");
        if (Property == default) Debug.LogError("Property must not be null");
        if (Targeting == default) Debug.LogError("Targeting must not be null");
        if (Hit == default) Debug.LogError("Hit must not be null");
        if (OnHit == default) Debug.LogError("OnHit must not be null");

        _birth = Time.time;

        transform.localScale = Vector2.one * Stat.Scale;
    }

    public void Init(Vector3 position = default, Vector3 scale = default, Vector3 direction = default, Transform parent = null)
    {
        // initialize position, scale and direction
        if (position != default) transform.localPosition = position;
        if (scale != default) transform.localScale = scale;
        if (direction != default) transform.right = direction;

        // set parent
        transform.SetParent(parent);
    }

    public void Init()
    {
        // calculate life time
        if (Stat.LifeSpan > 0) StartCoroutine(CoCalculateLifeSpan());

        // init function of projection
        Projection?.Projection_Start(this);

        // start checking hit
        StartCoroutine(Hit.CoCheckHit(this));
    }

    public void Damage(EnemyPlayer enemy)
    {
        if (enemy != null)
        {
            // apply damage
            enemy.HP -= Stat.Damage;

            // apply property effect
            if (enemy.HP > 0) Property.OnHit(this, enemy);
        }
    }

    private IEnumerator CoCalculateLifeSpan()
    {
        yield return WaitForSecondsFactory.Get(Stat.LifeSpan);

        PoolingManager.Instance.Despawn<SkillProjectile>(this);
    }
}
