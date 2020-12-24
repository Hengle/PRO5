using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

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
    
    public bool IsInRange(Transform target, Transform self, float range)
    {
        return (self.transform.position - target.position).sqrMagnitude < Mathf.Pow(range, 2);
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
        //Subtracts deltatime every frame/playerloop
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
