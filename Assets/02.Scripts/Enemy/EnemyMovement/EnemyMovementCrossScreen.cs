using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "CrossScreen", menuName = MENU_NAME + "CrossScreen")]
public class EnemyMovementCrossScreen : EnemyMovement
{
    private void OnValidate()
    {
        Type = EnemyMovementType.CrossScreen;
    }

    public override void Movement_Start(EnemyPlayer enemy)
    {
        Bounds bounds = MainCamera.OrthographicBounds();

        // from left to right
        if (enemy.transform.position.x <= bounds.min.x)
        {
            enemy.Direction = new Vector2(bounds.max.x, Random.Range(bounds.min.y, bounds.max.y)) - (Vector2)enemy.transform.position;
        }

        // from right to left
        else if (enemy.transform.position.x >= bounds.max.x)
        {
            enemy.Direction = new Vector2(bounds.min.x, Random.Range(bounds.min.y, bounds.max.y)) - (Vector2)enemy.transform.position;
        }

        // from bottom to top
        else if (enemy.transform.position.y <= bounds.min.y)
        {
            enemy.Direction = new Vector2(Random.Range(bounds.min.x, bounds.max.x), bounds.max.y) - (Vector2)enemy.transform.position;
        }

        // from top to bottom
        else if (enemy.transform.position.y >= bounds.max.y)
        {
            enemy.Direction = new Vector2(Random.Range(bounds.min.x, bounds.max.x), bounds.min.y) - (Vector2)enemy.transform.position;
        }
    }

    public override void Movement_OnEanble(EnemyPlayer enemy)
    {
    }

    public override void Movement_Update(EnemyPlayer enemy)
    {
        Avoid(enemy);
        enemy.transform.Translate(enemy.Direction * enemy.Speed * Time.deltaTime);
    }
}
