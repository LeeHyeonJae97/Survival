using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "Cursed", menuName = MENU_NAME + "Cursed")]
public class EnemyMovementCursed : EnemyMovement
{
    private void OnValidate()
    {
        Type = EnemyMovementType.Cursed;
    }

    public override void Movement_Start(EnemyPlayer enemy)
    {

    }

    public override void Movement_OnEanble(EnemyPlayer enemy)
    {
        // move toward random direction
        enemy.transform.right = enemy.transform.position - Player.Instance.transform.position;
        enemy.transform.right += enemy.transform.up * Random.Range(-1f, 1f);
    }

    public override void Movement_Update(EnemyPlayer enemy)
    {
        enemy.transform.Translate(Vector2.right * enemy.Speed * Time.deltaTime);
    }
}
