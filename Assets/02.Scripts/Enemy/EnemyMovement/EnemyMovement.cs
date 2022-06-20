using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyMovementType { FollowPlayer, CrossPlayer, CrossScreen, Cursed }

public abstract class EnemyMovement : ScriptableObject
{
    public const string FILE_NAME = "Enemy Movement ";
    public const string MENU_NAME = "ScriptableObject/Enemy/Movement/";
    // TODO :
    // balance offset and enemy avoidance distance threshold
    private const float avoidOffset = 0.03f;
    private const float avoidDstThreshold = 0.5f * 0.5f;

    [field: SerializeField] public EnemyMovementType Type { get; protected set; }

    public abstract void Movement_Start(EnemyPlayer enemy);
    public abstract void Movement_OnEanble(EnemyPlayer enemy);
    public abstract void Movement_Update(EnemyPlayer enemy);

    protected void Avoid(EnemyPlayer enemy)
    {
        AvoidPlayer(enemy);
        AvoidEnemy(enemy);
    }

    private void AvoidPlayer(EnemyPlayer enemy)
    {
        Vector2 dir = enemy.transform.position - Player.GetInstance().transform.position;
        if (dir.sqrMagnitude < avoidDstThreshold)
        {
            enemy.transform.position += (Vector3)dir.normalized * avoidOffset;
        }
    }

    private void AvoidEnemy(EnemyPlayer enemy)
    {
        var enemies = WaveManager.GetInstance().Enemies;

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == enemy) continue;

            Vector2 dir = enemy.transform.position - enemies[i].transform.position;
            if (dir.sqrMagnitude < avoidDstThreshold)
            {
                enemy.transform.position += (Vector3)dir.normalized * avoidOffset;
            }
        }
    }
}
