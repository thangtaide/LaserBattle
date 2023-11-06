using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.Base;
namespace LTA.Base.Character.Anim3D
{
    [System.Serializable]
    public class Character3DInfo
    {
        public string path;
    }


    public class Load3DCharacter : MonoBehaviour, IOnUpLevel
    {
        GameObject currentModel;

        NonEntityController nonEntityController;

        NonEntityController NonEntityController
        {
            get
            {
                if (nonEntityController == null)
                    nonEntityController = GetComponent<NonEntityController>();
                return nonEntityController;
            }
        }

        public void OnUpLevel(int level)
        {
            if (currentModel!=null)
            {
                Destroy(currentModel);
            }
            Character3DInfo character3DInfo = Character3DDataController.Instance.character3DVO.GetData<Character3DInfo>(name, level);
            GameObject prefab = GlobalVO.GetAsset.GetAsset<GameObject>(character3DInfo.path);
            currentModel = Instantiate(prefab, transform);
            currentModel.transform.localPosition = Vector3.zero;
            NonEntityController.UpdateLevelInfo(this);
        }
    }
}
