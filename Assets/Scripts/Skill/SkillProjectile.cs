using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile : MonoBehaviour
{
    private const float DAMAGING_COOLDOWN = 0.3f;

    public SkillStat Stat { get; private set; }
    public SkillProperty Property { get; private set; }
    public SkillTargeting Targeting { get; private set; }
    public SkillProjection Projection { get; private set; }
    public SkillHit Hit { get; private set; }
    public float Elapsed { get { return _birth == 0 ? -1 : Time.time - _birth; } }

    private HashSet<int> _enemies;
    private float _birth;

    private void OnDisable()
    {
        // reset
        Stat = default;
        Property = default;
        Targeting = default;
        Projection = default;
        Hit = default;
        _enemies = default;
        _birth = default;

        transform.DOKill(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // to prevent error
        if (gameObject.activeInHierarchy) Hit?.Hit_OnTriggerEnter2D(this, collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // to prevent error
        if (gameObject.activeInHierarchy) Hit?.Hit_OnTriggerStay2D(this, collision);
    }

    private void Update()
    {
        Projection?.Projection_Update(this);
    }

    public void Init(Skill skill)
    {
        Stat = skill.Stat;
        Property = skill.Property;
        Targeting = skill.Targeting;
        Projection = skill.Projection;
        Hit = skill.Hit;

        if (Stat == default) Debug.LogError("stat must not be null");
        if (Property == default) Debug.LogError("property must not be null");
        if (Targeting == default) Debug.LogError("targeting must not be null");
        if (Hit == default) Debug.LogError("hit must not be null");

        _enemies = new HashSet<int>();

        _birth = Time.time;

        transform.localScale = Vector2.one * Stat.Scale;
    }

    public void Init(Vector3 position, Vector3 direction = default)
    {
        // initialize position and direction
        transform.position = position;
        if (direction != default) transform.right = direction;

        // calculate life time
        StartCoroutine(CoCalculateLifeSpan());

        //
        Projection?.Projection_Start(this);
    }

    public void Damage(EnemyPlayer enemy)
    {
        if (enemy != null && !_enemies.Contains(enemy.GetInstanceID()))
        {
            // apply damage
            enemy.HP -= Stat.Damage;

            if (enemy.HP > 0)
            {
                // calculate damaging cooldown
                StartCoroutine(CoCalculateDamagingCooldown(enemy.GetInstanceID()));

                // apply property effect
                Property?.OnHit(this, enemy);
            }
        }
    }

    private IEnumerator CoCalculateLifeSpan()
    {
        yield return WaitForSecondsFactory.Get(Stat.LifeSpan);

        PoolingManager.Instance.Despawn<SkillProjectile>(this);
    }

    private IEnumerator CoCalculateDamagingCooldown(int enemy)
    {
        _enemies.Add(enemy);
        yield return WaitForSecondsFactory.Get(DAMAGING_COOLDOWN);

        _enemies.Remove(enemy);
    }
}
