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
        //// move toward center
        //enemy.Direction = Player.GetInstance().transform.position - enemy.transform.position;
    }

    public override void Movement_OnEanble(EnemyPlayer enemy)
    {

    }

    public override void Movement_Update(EnemyPlayer enemy, List<EnemyPlayer> neighbors)
    {
        //enemy.transform.Translate(enemy.Direction * enemy.Speed * PlayTime.deltaTime);
        //Avoid(enemy, neighbors);
    }
}
