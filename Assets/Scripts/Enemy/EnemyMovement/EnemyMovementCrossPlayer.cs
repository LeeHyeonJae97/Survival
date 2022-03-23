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

    public override void Movement_Start(Enemy enemy)
    {
        // move toward center
        enemy.transform.right = Player.Instance.transform.position - enemy.transform.position;
    }

    public override void Movement_Update(Enemy enemy)
    {
        enemy.transform.Translate(Vector2.right * enemy.Speed * Time.deltaTime);
    }
}
