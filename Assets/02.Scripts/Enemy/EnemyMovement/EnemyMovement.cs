using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyMovementType { FollowPlayer, CrossPlayer, CrossScreen, Cursed }

public abstract class EnemyMovement : ScriptableObject
{
    public const string FILE_NAME = "Enemy Movement ";
    public const string MENU_NAME = "ScriptableObject/Enemy/Movement/";

    [field: SerializeField] public EnemyMovementType Type { get; protected set; }

    public abstract void Movement_Start(EnemyPlayer enemy);
    public abstract void Movement_OnEanble(EnemyPlayer enemy);
    public abstract void Movement_Update(EnemyPlayer enemy);

    protected void Avoid(EnemyPlayer enemy)
    {
        // TODO :
        // balance offset and enemy avoidance distance threshold
        float offset = 0.03f;
        float ead = 0.5f * 0.5f;

        var enemies = EnemySpawner.Instance.Enemies;

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == enemy) continue;

            Vector2 dir = enemy.transform.position - enemies[i].transform.position;
            if (dir.sqrMagnitude < ead) enemy.transform.position += (Vector3)dir.normalized * offset;
        }
    }
}
