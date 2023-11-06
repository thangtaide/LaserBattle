
using UnityEngine;
using System.Collections.Generic;


namespace LTA.Base.Character
{
    [System.Serializable]
    public class CharacterStateInfo
    {
        public string      state;
        public string      anim;
        public bool        loop;
        public bool        isCanSkip;
        public string  []  specialStates;
        public bool        isResetOnEnd = true;
    }

    [System.Serializable]
    public class CharacterInfo
    {
        public CharacterStateInfo[] characterStates;
    }

    public class LTACharacterController : MonoBehaviour,IOnEndAnimation,IOnUpLevel
    {
        Dictionary<string, CharacterStateInfo> dic_State_StateInfo = new Dictionary<string, CharacterStateInfo>();

        CharacterStateInfo defaultState;

        CharacterStateInfo[] CharacterStateInfos
        {
            set
            {
                dic_State_StateInfo.Clear();
                foreach(CharacterStateInfo characterStateInfo in value)
                {
                    if (dic_State_StateInfo.ContainsKey(characterStateInfo.state)) continue;
                    dic_State_StateInfo.Add(characterStateInfo.state,characterStateInfo);
                }
                defaultState = value[0];
                CurrentState = defaultState;
            }
        }

        bool isEnd = false;
        [SerializeField]
        CharacterStateInfo currentState;
        public CharacterStateInfo CurrentState
        {
            set
            {
                currentState = value;
                isEnd = false;
                Animator.Play(currentState.anim,currentState.loop);
            }
            get
            {
                return currentState;
            }
        }
        
        IAnimation animator;

        protected IAnimation Animator
        {
            get
            {
                if (animator == null)
                {
                    animator = GetComponent<IAnimation>();
                }
                return animator;
            }
        }

        public void SetState(string state)
        {
            if (!CheckChangeState(state)) return;
            if (!dic_State_StateInfo.ContainsKey(state)) return;
            CharacterStateInfo oldCharacterInfo = CurrentState;
            CurrentState = dic_State_StateInfo[state];
            IOnSetState[] onSetStates = transform.parent.GetComponents<IOnSetState>();
            //if (transform.parent.name == "AmbrushBot")
            //{
            //    Debug.Log(oldCharacterInfo.state + " " + CurrentState.state);
            //}
            foreach(IOnSetState onSetState in onSetStates)
            {
                onSetState.OnSetState(oldCharacterInfo, CurrentState);
            }

        }

        bool CheckChangeState(string state)
        {
            foreach (string checkState in CurrentState.specialStates)
            {
                if (state == checkState)
                    return true;
            }
            return isEnd || CurrentState.isCanSkip;
        }

        public void OnEndAnimation(string animationName)
        {
            isEnd = CurrentState.isResetOnEnd && !CurrentState.loop;
            if (isEnd)
            {
                CurrentState = defaultState;
            }
        }

        string OwnName
        {
            get
            {
                return transform.parent.name;
            }
        }

        public void OnUpLevel(int level)
        {
            if (!CharacterDataController.Instance.charactersVO.CheckKey(OwnName))
            {
                Destroy(this);
                return;
            }
            CharacterInfo characterInfo = CharacterDataController.Instance.charactersVO.GetData<CharacterInfo>(OwnName, level);
            if (characterInfo == null)
            {
                return;
            }
            CharacterStateInfos = characterInfo.characterStates;
        }
    }
}
