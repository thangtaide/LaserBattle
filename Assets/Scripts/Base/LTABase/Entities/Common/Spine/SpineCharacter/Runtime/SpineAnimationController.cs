using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Animation = Spine.Animation;
using System;
using LTA.Base.Character;
using Event = Spine.Event;
using Spine;
namespace LTA.Character.Spine
{
    public class SpineInfo
    {
        public string path;
        public string skinName;
        public bool isSpawnBones = false;
        public string[] overrideKeys = null;
        public int layer = -1;
    }

    [RequireComponent(typeof(SkeletonAnimation))]
    public class SpineAnimationController : BaseAnimation
    {

        SkeletonAnimation skeleton;
        
        SkeletonAnimation Skeleton
        {
            get
            {
                if (skeleton == null)
                    skeleton = GetComponent<SkeletonAnimation>();
                return skeleton;
            }
        }


        private void HandleAnimationStartEvent(TrackEntry trackEntry)
        {

            OnStartAnim();
        }

        private void HandleAnimationEndEvent(TrackEntry trackEntry)
        {
            Skeleton.timeScale = 1;
            OnEndAnim();
        }

        private void HandleAnimationStateEvent(TrackEntry trackEntry, Event e)
        {
            string eventName = e.Data.Name;
            OnEventAnim(eventName);
        }

        public override bool CheckAnim(string animName)
        {
            return Skeleton.skeleton.Data.FindAnimation(animName) != null;
        }

        public override void Play(bool loop = true)
        {
            Animation animation = Skeleton.skeleton.Data.FindAnimation(anim);
            float timeDelay = 1 / speedAnim;
            if (animation == null)
            {
                return;
            }
            if (animation.Duration > timeDelay)
            {
                Skeleton.timeScale = (float)animation.Duration / timeDelay;
            }
            skeleton.timeScale = 1;
            Skeleton.loop = loop;
            Skeleton.AnimationName = anim;
        }
        SpineInfo spineInfo;
        public override void OnUpLevel(int level)
        {
            
            spineInfo = SpineDataController.Instance.spinesVO.GetData<SpineInfo>(OwnName, level);
            LoadSpine(spineInfo);
            Skeleton.AnimationState.Start += HandleAnimationStartEvent;
            Skeleton.AnimationState.Event += HandleAnimationStateEvent;
            Skeleton.AnimationState.Complete += HandleAnimationEndEvent;
            if (spineInfo.isSpawnBones)
            {
                SkeletonUtility skeletonUtility = GetComponent<SkeletonUtility>();
                if (skeletonUtility == null) skeletonUtility = gameObject.AddComponent<SkeletonUtility>();
                bones = skeletonUtility.SpawnHierarchy(SkeletonUtilityBone.Mode.Follow, true, true, true).transform;
                if (spineInfo.overrideKeys == null) return;
                foreach (string key in spineInfo.overrideKeys)
                {
                    SkeletonUtilityBone bone = bones.Find(key).GetComponent<SkeletonUtilityBone>();
                    bone.mode = SkeletonUtilityBone.Mode.Override;
                }

            }
            base.OnUpLevel(level);

        }

        Transform bones;

        public Transform Bones
        {
            get
            {
                return bones;
            }
        }
        void LoadSpine(SpineInfo spineInfo)
        {
            if (spineInfo == null) return;
            if (spineInfo.layer != -1)
                LayerOrder = spineInfo.layer;
            SkeletonDataAsset asset = GlobalVO.GetAsset.GetAsset<SkeletonDataAsset>(spineInfo.path);
            Skeleton.skeletonDataAsset = asset;
            Skeleton.Initialize(true);
            if (!String.IsNullOrEmpty(spineInfo.skinName))
            {
                SetSkin(spineInfo.skinName);
            }
        }

        public void SetSkin(string skinName)
        {
            spineInfo.skinName = skinName;
            Skeleton.skeleton.SetSkin(skinName);
            Skeleton.Skeleton.SetSlotsToSetupPose();
            Skeleton.LateUpdate();
        }
            

        public override void Play(string animName,int track, bool loop = true)
        {
            Skeleton.state.SetAnimation(track, animName, loop);
        }

        public override void RemoveTrack(int track)
        {
            Skeleton.state.ClearTrack(track);
            LoadSpine(spineInfo);
        }

        public override int LayerOrder
        {
            set
            {
                GetComponent<MeshRenderer>().sortingOrder = value;
            }
            get
            {
                return GetComponent<MeshRenderer>().sortingOrder;
            }

        }

        public override float Height => Skeleton.skeleton.FindBone("All").ScaleX * Skeleton.skeleton.Data.Height;

        public override float Alpha { get => Skeleton.skeleton.A; set => Skeleton.skeleton.A = value; }
    }
}
