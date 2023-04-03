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
    public abstract void Movement_Update(EnemyPlayer enemy, List<EnemyPlayer> neighbors);

    //protected Vector2 Avoid(EnemyPlayer enemy, List<EnemyPlayer> neighbors)
    //{
    //    var direction = Vector2.zero;

    //    //direction += AvoidPlayer(enemy);
    //    direction += AvoidEnemy(enemy, neighbors);

    //    return direction;
    //}

    //private Vector2 AvoidPlayer(EnemyPlayer enemy)
    //{
    //    Vector2 dir = enemy.transform.position - Player.GetInstance().transform.position;

    //    return dir.sqrMagnitude < avoidDstThreshold ? dir.normalized * avoidOffset : Vector2.zero;
    //}

    //private Vector2 AvoidEnemy(EnemyPlayer enemy, List<EnemyPlayer> neighbors)
    //{
    //    var direction = Vector2.zero;

    //    foreach (var other in neighbors)
    //    {
    //        if (enemy != other)
    //        {
    //            var dir = (Vector2)(enemy.transform.position - other.transform.position);
    //            var dst = dir.magnitude;

    //            direction += dir.normalized / dst * 11.52f;
    //        }
    //    }

    //    return direction * neighbors.Count;
    //}
}
