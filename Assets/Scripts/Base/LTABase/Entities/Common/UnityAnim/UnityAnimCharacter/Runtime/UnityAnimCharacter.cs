using UnityEngine;
namespace LTA.Base.Character.UnityAnim
{

    public abstract class UnityAnimCharacter : BaseAnimation
    {
        Animator animator;

        protected Animator Animator
        {
            get
            {
                if (animator == null)
                    animator = GetComponent<Animator>();
                return animator;
            }
        }


        public override int LayerOrder
        {
            get => 1; set
            {
            }
        }

        public override float Height => 1f;

        AnimationClip[] animationClips;

        AnimationClip[] AnimationClips
        {
            get
            {
                if (animationClips == null)
                {
                    animationClips = Animator.runtimeAnimatorController.animationClips;
                }
                return animationClips;
            }

        }

        AnimationClip GetAnimatorClipInfo(string animName)
        {
            foreach (AnimationClip animationClip in AnimationClips)
            {
                if (animationClip.name == animName)
                    return animationClip;
            }
            return null;
        }

        public override bool CheckAnim(string animName)
        {
            return GetAnimatorClipInfo(animName) != null;
        }

        public override void Play(bool loop = true)
        {
            base.Play(anim, loop);
            if (currentAnim == anim) return;
            currentAnim = anim;
            AnimationClip animationClip = GetAnimatorClipInfo(anim);

            Animator.Rebind();
            Animator.Update(0f);

            Animator.speed = 1 / speedAnim;
            if (loop)
                animationClip.wrapMode = WrapMode.Loop;
            else
                animationClip.wrapMode = WrapMode.Once;
            Animator.Play(anim);
            //Animator.CrossFade
        }

        string currentAnim = "";


        public void StartAnim()
        {
            OnStartAnim();
        }

        public void OnEvent(string eventName)
        {
            OnEventAnim(eventName);
        }

        public void EndAnim()
        {
            OnEndAnim();
        }

        public override void Play(string animName, int track, bool loop = true)
        {
            AnimationClip animationClip = GetAnimatorClipInfo(animName);
            if (loop)
                animationClip.wrapMode = WrapMode.Loop;
            else
                animationClip.wrapMode = WrapMode.Once;
            Animator.Play(animName, track);
        }

        public override void RemoveTrack(int track)
        {
            //RemoveTrack
        }
    }
}
