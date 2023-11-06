using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInfo
{
    public float speedAnim = 1f;
}
namespace LTA.Base.Character
{
    public abstract class BaseAnimation : MonoBehaviour, IAnimation,IOnUpLevel
    {
        [SerializeField]
        public float speedAnim = 1f;

        public string pre_Anim = "";
        
        public string anim = "";

        bool isLoop = true;

        protected string OwnName
        {
            get
            {
                return transform.parent.name;
            }
        }

        public abstract int LayerOrder { get; set; }

        public abstract float Height { get; }

        public abstract float Alpha { get; set; }

        public abstract bool CheckAnim(string animName);

        public void Play(string animName, bool loop = true)
        {
            anim = animName;
            isLoop = loop;
            Play(loop);
        }

        public abstract void Play(string animName,int track, bool loop = true);

        public abstract void RemoveTrack(int track);

        public abstract void Play(bool loop = true);
        protected virtual void OnStartAnim()
        {
            IOnStartAnimation[] onStartAnimations = transform.parent.GetComponentsInChildren<IOnStartAnimation>();
            foreach(IOnStartAnimation onStartAnimation in onStartAnimations)
            {
                onStartAnimation.OnStartAnimation(anim);
            }
        }

        protected virtual void OnEventAnim(string eventAnim)
        {
            IOnEventAnimation[] onEventAnimations = transform.parent.GetComponentsInChildren<IOnEventAnimation>();
            foreach(global::IOnEventAnimation onEventAnimation in  onEventAnimations)
            {
                onEventAnimation.OnEvent(anim, eventAnim);
            }
        }

        protected virtual void OnEndAnim()
        {
            IOnEndAnimation[] onEndAnimations = transform.parent.GetComponentsInChildren<IOnEndAnimation>();
            foreach(IOnEndAnimation onEndAnimation in onEndAnimations)
            {
                onEndAnimation.OnEndAnimation(anim);
            }
        }


        public void SetPreAnim(string preAnim)
        {
            this.pre_Anim = preAnim;
            Play(isLoop);
        }

        public virtual void OnUpLevel(int level)
        {
            if (!CharacterDataController.Instance.animationVO.CheckKey(OwnName)) return;
            AnimationInfo animationInInfo = CharacterDataController.Instance.animationVO.GetData<AnimationInfo>(OwnName,level);
            if (animationInInfo == null) return;
            speedAnim = animationInInfo.speedAnim;
            IEndLoadAnimation[] endLoadAnimations = transform.parent.GetComponents<IEndLoadAnimation>();
            foreach(IEndLoadAnimation endLoadAnimation in endLoadAnimations)
            {
                endLoadAnimation.OnEndLoadAnimtion();
            }
        }
    }
}
