using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyMovementType { FollowPlayer, CrossPlayer, Cursed }

public abstract class EnemyMovement : ScriptableObject
{
    public const string FILE_NAME = "Enemy Movement ";
    public const string MENU_NAME = "ScriptableObject/Enemy/Movement/";

    [field: SerializeField] public EnemyMovementType Type { get; protected set; }

    public abstract void Movement_Start(Enemy enemy);
    public abstract void Movement_Update(Enemy enemy);
}
