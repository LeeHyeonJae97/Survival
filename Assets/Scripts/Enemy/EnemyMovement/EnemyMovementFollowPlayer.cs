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

    public override void Movement_Start(EnemyPlayer enemy)
    {

    }

    public override void Movement_OnEanble(EnemyPlayer enemy)
    {

    }

    public override void Movement_Update(EnemyPlayer enemy)
    {
        enemy.transform.right = Player.Instance.transform.position - enemy.transform.position;
        enemy.transform.Translate(Vector2.right * enemy.Speed * Time.deltaTime);
    }
}
