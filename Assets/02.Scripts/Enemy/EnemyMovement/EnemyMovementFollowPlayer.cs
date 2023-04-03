using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME + "FollowPlayer", menuName = MENU_NAME + "FollowPlayer")]
public class EnemyMovementFollowPlayer : EnemyMovement
{
    [SerializeField] private float _weight;

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

    public override void Movement_Update(EnemyPlayer enemy, List<EnemyPlayer> neighbors)
    {
        var direction = Vector3.zero;

        // toward center
        direction += MoveTowardTarget();

        // backward from other
        direction += MoveBackwardFromOther();

        enemy.Direction = direction.normalized;

        enemy.transform.position += enemy.Speed * PlayTime.deltaTime * (Vector3)enemy.Direction;

        Vector3 MoveTowardTarget()
        {
            return Player.GetInstance().transform.position - enemy.transform.position;
        }

        Vector3 MoveBackwardFromOther()
        {
            var direction = Vector3.zero;

            foreach (var other in neighbors)
            {
                if (enemy != other)
                {
                    var dir = enemy.transform.position - other.transform.position;
                    var dst = dir.magnitude;

                    direction += dir.normalized / dst * _weight;
                }
            }

            return direction * neighbors.Count;
        }
    }
}
