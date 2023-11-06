using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOnRemoveEntity
{
    void OnRemoveEntity(Entity pickedEntity, StackEntityController stack);
}
