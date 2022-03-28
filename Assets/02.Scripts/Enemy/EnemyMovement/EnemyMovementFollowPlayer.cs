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
        enemy.Direction = Player.Instance.transform.position - enemy.transform.position;
        Avoid(enemy);

        enemy.transform.Translate(enemy.Direction * enemy.Speed * Time.deltaTime);
    }
}
