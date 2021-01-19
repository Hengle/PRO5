using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.AI;
public class AIUtilities : MonoBehaviour
{
    void OnEnable()
    {
        ScriptCollection.RegisterScript(this);
    }
    
    void OnDisable()
    {
        ScriptCollection.RemoveScript(this);
    }

    public void DestroyObject(GameObject obj, float time = 0.1f)
    {
        Destroy(obj, time);
    }

    public void MoveNavMeshAgent(NavMeshAgent agent, Vector3 target, Vector3 moveVector, float speed)
    {
        agent.destination = target;

        Vector3 moveTo = moveVector * speed * Time.deltaTime;

        agent.Move(moveTo);
    }

    public void LookAtTarget(Transform target, Transform self, float lerpSpeed = 0)
    {
        Vector3 dir = target.position - self.position;
        dir.y = 0;
        if (lerpSpeed == 0)
        {
            self.transform.rotation = Quaternion.LookRotation(dir);
        }
        else
        {
            Quaternion look = Quaternion.LookRotation(dir);
            self.transform.rotation = Quaternion.Lerp(self.transform.rotation, look, Time.deltaTime * lerpSpeed);
        }
    }

    public bool IsInRange(Transform target, Transform self, float range)
    {
        return (self.transform.position - target.position).sqrMagnitude < Mathf.Pow(range, 2);
    }

    public bool IsInRange(Vector3 target, Vector3 self, float range)
    {
        return (self - target).sqrMagnitude < Mathf.Pow(range, 2);
    }

    public class RandomPointLocator
    {
        NavMeshHit hit;
        Vector3 random;
        Vector3 basePos;
        float maxDist;
        bool isRunning = false;
        RaycastHit rayCornerHit;
        RaycastHit rayWallHit;

        public List<Vector3> wayPoints = new List<Vector3>();
        public void FindWaypoints(GameObject obj, EnemyBody enemyBody, float minAngle, float maxAngle, float awayFromWallAngle, float minWallDistance, bool async, int amount = 3, System.Action Finished = null)
        {
            if (async)
                FindWaypointsAsync(obj, enemyBody, minAngle, maxAngle, awayFromWallAngle, minWallDistance, Finished, amount);
            else
                FindWaypointsNotAsync(obj, enemyBody, minAngle, maxAngle, awayFromWallAngle, minWallDistance, amount);
        }
        public async UniTask FindWaypointsAsync(GameObject obj, EnemyBody enemyBody, float minAngle, float maxAngle, float awayFromWallAngle, float minWallDistance, System.Action Finished, int amount = 3)
        {
            float dot = 1;
            float dot2 = 2;

            random = Vector3.zero;
            basePos = obj.transform.position;

            float tempMinAngle = minAngle;

            Vector3 cornerDir = Vector3.zero;

            for (int i = 0; i < amount; i++)
            {
                bool done = false;
                int c = 0;
                Vector3 playerPos = enemyBody.aiManager.playerTarget.position;
                while (!done)
                {
                    float a = Random.Range(0f, 1f) * 2 * Mathf.PI;
                    maxDist = Random.Range(8f, 10f);

                    float x = Mathf.Cos(a);
                    float y = Mathf.Sin(a);

                    Vector3 unit = new Vector3(x, 0, y) * maxDist;
                    random = basePos + unit;

                    int wallcheck = CheckForWalls(ref cornerDir, minWallDistance);

                    //Get dot product from player to target position
                    dot = Vector3.Dot((new Vector3(playerPos.x, basePos.y, playerPos.z) - basePos).normalized,
                                                                (new Vector3(random.x, basePos.y, random.z) - basePos).normalized);

                    if (wallcheck == 1) //Behaviour when there is only one wall near the enemy
                    {
                        dot2 = Vector3.Dot(cornerDir.normalized,
                                            (new Vector3(random.x, basePos.y, random.z) - basePos).normalized);

                        done = CheckAngle(dot, tempMinAngle, obj) && CheckAngle(dot2, awayFromWallAngle, obj) &&
                            !CheckLineOfSight(new Vector3(random.x, basePos.y, random.z), obj, maxDist);
                    }
                    else if (wallcheck == 2) //Behaviour if there are to walls near the enemy (Enemy in a corner)
                    {

                        dot = Vector3.Dot(cornerDir.normalized,
                                            (new Vector3(random.x, basePos.y, random.z) - basePos).normalized);
                        if (CheckAngle(dot, -0.5f, obj) && !CheckLineOfSight(new Vector3(random.x, basePos.y, random.z), obj, maxDist))
                        {
                            done = true;
                        }

                    }
                    else // normal Behaviour when no walls are near
                    {
                        done = CheckAngle(dot, minAngle, obj) && !CheckLineOfSight(new Vector3(random.x, obj.transform.position.y, random.z), obj, maxDist);
                    }

                    c++;
                    if (c >= 3)
                    {
                        await UniTask.Yield();
                        c = 0;
                    }
                }

                tempMinAngle += 0.1f;
            }
            Finished();
        }
        public void FindWaypointsNotAsync(GameObject obj, EnemyBody enemyBody, float minAngle, float maxAngle, float awayFromWallAngle, float minWallDistance, int amount = 2)
        {
            float dot = 1;
            float dot2 = 2;

            random = Vector3.zero;
            basePos = obj.transform.position;

            float tempMinAngle = minAngle;

            Vector3 cornerDir = Vector3.zero;

            for (int i = 0; i < amount; i++)
            {
                bool done = false;

                Vector3 playerPos = enemyBody.aiManager.playerTarget.position;
                while (!done)
                {
                    float a = Random.Range(0f, 1f) * 2 * Mathf.PI;
                    maxDist = Random.Range(8f, 10f);

                    float x = Mathf.Cos(a);
                    float y = Mathf.Sin(a);

                    Vector3 unit = new Vector3(x, 0, y) * maxDist;
                    random = basePos + unit;

                    int wallcheck = CheckForWalls(ref cornerDir, minWallDistance);

                    //Get dot product from player to target position
                    dot = Vector3.Dot((new Vector3(playerPos.x, basePos.y, playerPos.z) - basePos).normalized,
                                                                (new Vector3(random.x, basePos.y, random.z) - basePos).normalized);

                    //Behaviour when there is only one wall near the enemy
                    if (wallcheck == 1)
                    {
                        dot2 = Vector3.Dot(cornerDir.normalized,
                                            (new Vector3(random.x, basePos.y, random.z) - basePos).normalized);

                        done = CheckAngle(dot, tempMinAngle, obj) && CheckAngle(dot2, awayFromWallAngle, obj) &&
                            !CheckLineOfSight(new Vector3(random.x, basePos.y, random.z), obj, maxDist);
                    }
                    else if (wallcheck == 2) //Behaviour if there are to walls near the enemy (Enemy in a corner)
                    {

                        dot = Vector3.Dot(cornerDir.normalized,
                                            (new Vector3(random.x, basePos.y, random.z) - basePos).normalized);
                        done = CheckAngle(dot, -0.5f, obj) && !CheckLineOfSight(new Vector3(random.x, basePos.y, random.z), obj, maxDist);
                    }
                    else // normal Behaviour when no walls are near
                    {
                        done = CheckAngle(dot, minAngle, obj) && !CheckLineOfSight(new Vector3(random.x, obj.transform.position.y, random.z), obj, maxDist);
                    }

                }

                tempMinAngle += 0.1f;
            }
        }

        bool CheckLineOfSight(Vector3 pos, GameObject obj, float maxDist)
        {
            return Physics.Raycast(obj.transform.position, (pos - obj.transform.position).normalized, maxDist, LayerMask.GetMask("Wall"));
        }

        int CheckForWalls(ref Vector3 cornerDir, float minWallDistance)
        {
            RaycastHit tempHit = new RaycastHit();
            int count = 0;
            if (Physics.Raycast(basePos, Vector3.right, out tempHit, minWallDistance, LayerMask.GetMask("Wall")))
            {
                count++;
                cornerDir += Vector3.right / 2;
            }

            if (Physics.Raycast(basePos, -Vector3.right, out tempHit, minWallDistance, LayerMask.GetMask("Wall")))
            {
                count++;
                cornerDir -= Vector3.right / 2;
            }

            if (Physics.Raycast(basePos, Vector3.forward, out tempHit, minWallDistance, LayerMask.GetMask("Wall")))
            {
                count++;
                cornerDir += Vector3.forward;
            }

            if (Physics.Raycast(basePos, -Vector3.forward, out tempHit, minWallDistance, LayerMask.GetMask("Wall")))
            {
                count++;
                cornerDir -= Vector3.forward;
            }

            return count;
        }

        bool CheckAngle(float dot, float angle, GameObject obj)
        {
            if (dot < angle)
            {
                if (IsOnNavMesh(random, maxDist))
                {
                    Debug.Log("Find");
                    basePos = new Vector3(random.x, obj.transform.position.y, random.z);
                    wayPoints.Add(basePos);
                    return true;
                }
            }
            return false;
        }

        bool IsOnNavMesh(Vector3 random, float maxDist)
        {
            return NavMesh.SamplePosition(random, out hit, 0.4f, -1);
        }
    }
    //Simple Timer that counts down until 0 from a given float value
    public class Timer
    {
        float currentTime;
        float waitTime;
        bool timerDone;
        public bool timerStarted;
        public Timer()
        {

        }

        public Timer(float time)
        {
            waitTime = time;
            currentTime = 0;
        }


        async public void StartTimer(System.Action Finish)
        {
            timerDone = false;
            timerStarted = true;
            //Starting the async function
            await Timing();

            Finish();
            timerStarted = false;
        }

        //Async operation that uses the UniTask async library
        //Adds deltatime every frame/playerloop
        async UniTask Timing()
        {
            while (currentTime <= waitTime)
            {
                currentTime += Time.deltaTime;
                await UniTask.Yield();
                timerDone = true;
            }
            currentTime -= waitTime;
        }

        //Can be called to check if the timer has counted down to 0
        public bool TimerDone()
        {
            return timerDone;
        }

        public void setWaitTime(float time)
        {
            waitTime = time;
            if (currentTime >= waitTime)
                currentTime -= waitTime;
            else
                currentTime = 0;
        }
    }
}
