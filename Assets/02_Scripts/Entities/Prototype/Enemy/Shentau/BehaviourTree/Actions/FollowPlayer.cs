using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;

namespace BBUnity.Actions
{
    public class FollowPlayer : GOAction
    {
        [InParam("EnemyBody")]
        public EnemyBody enemyBody;

        public AIUtilities util;

        bool init = false;
        void OnInit()
        {
            util = ScriptCollection.GetScript<AIUtilities>();
            init = true;
        }
        public override void OnStart()
        {
            OnInit();
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}