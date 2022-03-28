using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "CrossPlayer", menuName = MENU_NAME + "CrossPlayer")]
public class EnemyMovementCrossPlayer : EnemyMovement
{
    private void OnValidate()
    {
        Type = EnemyMovementType.CrossPlayer;
    }

    public override void Movement_Start(EnemyPlayer enemy)
    {
        // move toward center
        enemy.Direction = Player.Instance.transform.position - enemy.transform.position;
    }

    public override void Movement_OnEanble(EnemyPlayer enemy)
    {

    }

    public override void Movement_Update(EnemyPlayer enemy)
    {
        enemy.transform.Translate(enemy.Direction * enemy.Speed * Time.deltaTime);
        Avoid(enemy);
    }
}
