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
        public Vector3 point;

        public List<Vector3> movePoints = new List<Vector3>();

        /// <summary>
        /// Finds a list of points on a Navmesh in a direction away from the player with a minimum angle (dot product).
        /// </summary>
        /// <param name="amount">The amount of points you want</param>
        /// <param name="minAngle">The minimum angle written as the dot product</param>
        /// <param name="maxDist">The maximum distance from the center</param>
        /// <param name="minDist">The minimum distance from the center</param>
        /// <param name="self">The center</param>
        /// <param name="playerTarget">The player</param>
        /// <returns>Returns a list of Vector3 positions</returns>
        public async UniTask FindMultPoints(float amount, float minAngle, float minDist, float maxDist, Transform self, Transform playerTarget)
        {
            float dot = 1;
            Vector3 random = self.position;
            bool done = false;

            for (int i = 0; i < amount; i++)
                while (!done)
                {
                    float dist = Random.Range(minDist, maxDist);
                    Vector3 unit = Random.insideUnitSphere * maxDist;
                    random = self.transform.position + unit;

                    dot = Vector3.Dot((playerTarget.position - self.position).normalized,
                                        (new Vector3(random.x, self.position.y, random.z) - self.position).normalized);

                    if (dot < minAngle)
                    {
                        if (IsOnNavMesh(random, maxDist))
                        {
                            Debug.Log("HIT");
                            done = true;
                            movePoints.Add(new Vector3(random.x, self.position.y, random.z));
                        }
                    }
                    await UniTask.Yield();
                }
        }

        /// <summary>
        /// Finds a point on a Navmesh in a direction away from the player with a minimum angle (dot product).
        /// </summary>
        /// <param name="minAngle">The minimum angle written as the dot product</param>
        /// <param name="maxDist">The maximum distance from the center</param>
        /// <param name="minDist">The minimum distance from the center</param>
        /// <param name="self">The center</param>
        /// <param name="playerTarget">The player</param>
        /// <returns>Returns a single position as Vector3</returns>
        public async UniTaskVoid FindSinglePoint(float minAngle, float maxDist, float minDist, Transform self, Transform playerTarget)
        {
            float dot = 1;
            Vector3 random = self.position;
            bool done = false;
            point = Vector3.zero;
            while (!done)
            {
                float dist = Random.Range(minDist, maxDist);
                Vector3 unit = Random.insideUnitSphere * maxDist;
                random = self.position + unit;

                dot = Vector3.Dot((playerTarget.position - self.position).normalized,
                                    (new Vector3(random.x, self.transform.position.y, random.z) - self.transform.position).normalized);

                if (dot < minAngle)
                {
                    if (IsOnNavMesh(random, maxDist))
                    {
                        Debug.Log("HIT");
                        done = true;
                        point = new Vector3(random.x, self.transform.position.y, random.z);
                    }
                }
                await UniTask.Yield();
            }
        }

        bool IsOnNavMesh(Vector3 random, float maxDist)
        {
            NavMeshHit hit;
            return NavMesh.SamplePosition(random, out hit, 0.5f, NavMesh.AllAreas);
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


        async public void StartTimer(System.Action SetCanAttack)
        {
            timerDone = false;
            timerStarted = true;
            //Starting the async function
            await Timing();

            SetCanAttack();
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
