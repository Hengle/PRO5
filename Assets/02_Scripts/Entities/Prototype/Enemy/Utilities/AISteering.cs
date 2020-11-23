using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISteering
{
    EnemyStatistics stats;
    EnemyBody body;

    public AISteering(EnemyStatistics _stats, EnemyBody _body){
        stats = _stats;
        body = _body;
    }

    public Vector3 AvoidanceSteering(Vector3 dir){
        RaycastHit hit;

        if (!FindObstacle(dir, out hit, false))
        {
            return body.aiManager.playerTarget.position;
        }

        Vector3 targetpos = body.aiManager.playerTarget.position;

        float angle = Vector3.Angle(dir * Time.deltaTime, hit.normal);
        if (angle > 165f)
        {
            Vector3 perp;

            perp = new Vector3(-hit.normal.z, hit.normal.y, hit.normal.x);

            targetpos = targetpos + (perp * Mathf.Sin((angle - 165f) * Mathf.Deg2Rad) * 2 * body.aiManager.obstacleAvoidDistance);
        }

        return Seek(targetpos);
    }

    public void IsGrounded()
    {
        Vector3 velocity = Vector3.zero;
        if (!Physics.CheckSphere(body.transform.position + new Vector3(0, 1, 0), 1.1f))
        {
            // controller.isGrounded = false;
            // DoGravity(controller, velocity);
        }
        else
        {
            // controller.isGrounded = true;
        }
    }

    private void DoGravity(EnemyBody enemyBody, Vector3 velocity)
    {
        velocity.y = Physics.gravity.y * Time.deltaTime;
        enemyBody.transform.position += velocity;
    }

    Vector3 Seek(Vector3 targetPosition)
    {
        Vector3 acceleration = SteerTowards(targetPosition);

        acceleration.Normalize();

        return acceleration;
    }

    Vector3 SteerTowards(Vector3 vector)
    {
        Vector3 v = vector.normalized * (stats.GetStatValue(StatName.Speed) * stats.GetMultValue(MultiplierName.speed));
        return Vector3.ClampMagnitude(v, stats.GetStatValue(StatName.Speed) * stats.GetMultValue(MultiplierName.speed));
    }

    public bool FindObstacle(Vector3 dir, out RaycastHit hit,bool findPlayer)
    {
        dir = dir.normalized;

        Vector3[] dirs = new Vector3[body.aiManager.whiskerAmount];
        dirs[0] = dir;

        float orientation = VectorToOrientation(dir);
        float angle = orientation;
        for (int i = 1; i < (dirs.Length + 1) / 2; i++)
        {
            angle += body.aiManager.angleIncrement;
            dirs[i] = OrientationToVector(orientation + angle * Mathf.Deg2Rad);
        }
        angle = orientation;
        for (int i = (dirs.Length + 1) / 2; i < dirs.Length; i++)
        {
            angle -= body.aiManager.angleIncrement;
            dirs[i] = OrientationToVector(orientation - angle * Mathf.Deg2Rad);
        }
        return CastWhiskers(dirs, out hit, findPlayer);
    }

    bool CastWhiskers(Vector3[] dirs, out RaycastHit firsthit, bool findPlayer)
    {
        firsthit = new RaycastHit();
        for (int i = 0; i < dirs.Length; i++)
        {
            RaycastHit hit;

            if (findPlayer)
            {
                if (Physics.SphereCast(body.rayEmitter.position, 1f, dirs[i], out hit, stats.GetStatValue(StatName.Range), LayerMask.GetMask("Player")))
                {
                    firsthit = hit;
                    return true;
                }
            }
            else
            {
                float dist = (i == 0) ? body.aiManager.mainWhiskerL : body.aiManager.secondaryWhiskerL;
                if (Physics.SphereCast(body.rayEmitter.position, 1f, dirs[i], out hit, dist, body.aiManager.enemyMask))
                {
                    firsthit = hit;
                    return true;
                }
            }

        }
        return false;
    }

    static Vector3 OrientationToVector(float orientation)
    {
        return new Vector3(Mathf.Cos(-orientation), 0, Mathf.Sin(-orientation));
    }

    static float VectorToOrientation(Vector3 direction)
    {
        return -1 * Mathf.Atan2(direction.z, direction.x);
    }

    public bool IsInFront(Vector3 target)
    {
        return IsFacing(target, 0);
    }

    public bool IsFacing(Vector3 target, float cosineValue)
    {
        Vector3 facing = body.transform.right.normalized;

        Vector3 directionToTarget = (target - body.transform.position);
        directionToTarget.Normalize();

        return Vector3.Dot(facing, directionToTarget) >= cosineValue;
    }
}
