using System;
namespace LTA.Base.Character
{
    public interface IAnimation
    {
        bool CheckAnim(string animName);

        void Play(string animName, bool loop = true);

        void Play(string animName, int track, bool loop = true);

        void RemoveTrack(int track);
        //void SetEvent(string eventName, Action eventHandle);

        //void SetStartEvent(Action eventHandle);

        //void SetEndEvent(Action eventHandle);

        int LayerOrder { get; set; }

        void SetPreAnim(string preAnim);

        float Height { get; }

        float Alpha { set; get; }
    }
}
