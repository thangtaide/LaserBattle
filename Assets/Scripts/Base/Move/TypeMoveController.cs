using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITypeMove
{
    Vector3 Move();
}

public class TypeMoveController : MoveController
{
    ITypeMove[] typeMoves;

    Vector3 direction;
    void Start()
    {
        typeMoves = GetComponents<ITypeMove>();
    }
    void Update()
    {

        direction = Vector3.zero;
        foreach (ITypeMove typeMove in typeMoves)
        {
            direction += typeMove.Move();
        }
        Move(direction);
    }
}
