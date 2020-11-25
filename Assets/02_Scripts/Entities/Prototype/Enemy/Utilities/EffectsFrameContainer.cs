using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [System.Serializable]
    public class EffectsFrameContainer
    {
        public EffectType type;
        public float frame;
        public EffectsFrameContainer(EffectType _type, float _frame)
        {
            type = _type;
            frame = _frame;
        }
    }
}