using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOnThrowEntity
{
    void OnThrowEntity(Entity pickedEntity, StackEntityController stack,GameObject entityObject);
}
