using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "FollowPlayer", menuName = MENU_NAME + "FollowPlayer")]
public class EnemyMovementFollowPlayer : EnemyMovement
{
    private void OnValidate()
    {
        Type = EnemyMovementType.FollowPlayer;
    }

    public override void Movement_Start(Enemy enemy)
    {

    }

    public override void Movement_Update(Enemy enemy)
    {
        enemy.transform.right = Player.Instance.transform.position - enemy.transform.position;
        enemy.transform.Translate(enemy.transform.right * enemy.Speed * Time.deltaTime);
    }
}
