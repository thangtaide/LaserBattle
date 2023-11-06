using LTA.Base;
using System;
namespace LTA.Effect
{
    public abstract class BehaviourEffect : BaseEffect
    {
        public float timeEffect = 0.05f;
        
        public LeanTweenType _LeanTweenType = LeanTweenType.linear;
        
        BehaviourController behaviour;

        protected BehaviourController Behaviour
        {
            get
            {
                if (behaviour == null)
                {
                    behaviour = gameObject.AddComponent<BehaviourController>();
                    behaviour.timePerforme = timeEffect;
                    behaviour._LeanTweenType = _LeanTweenType;
                }
                return behaviour;
            }
        }

        private void OnDestroy()
        {
            if (behaviour != null)
                Destroy(behaviour);
        }
    }
}
