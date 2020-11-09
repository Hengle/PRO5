using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class AIUtilities : MonoBehaviour
{
    //Simple Timer that counts down until 0 from a given float value
    public class Timer
    {
        public float currentTime;
        float waitTime;

        public Timer()
        {

        }

        public Timer(float time)
        {
            waitTime = time;
            currentTime = waitTime;
        }

        //Starts the async operation
        public void StartTimer()
        {
            currentTime = waitTime;
            Timing();
        }

        //Async operation that uses the UniTask async library
        //Subtracts deltatime every frame/playerloop
        async UniTask Timing()
        {
            
            while (currentTime >= 0)
            {
                currentTime -= Time.deltaTime;
                await UniTask.Yield();
            }
        }

        //Can be called to see if the timer has counted down to 0
        public bool TimerDone()
        {
            return currentTime <= 0;
        }

        public void setWaitTime(float time)
        {
            waitTime = time;
            currentTime = waitTime;
        }
    }
}
