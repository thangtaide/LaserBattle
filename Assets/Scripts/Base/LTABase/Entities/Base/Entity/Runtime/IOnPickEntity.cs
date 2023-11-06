using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOnPickEntity
{
    void OnPickEntity(Entity pickedEntity, StackEntityController stack);
}
