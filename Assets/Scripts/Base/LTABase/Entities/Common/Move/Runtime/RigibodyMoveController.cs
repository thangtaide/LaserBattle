using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigibodyMoveController : MoveController
{
    Rigidbody rigidbody3D;

    Rigidbody Rigidbody3D
    {
        get
        {
            if (rigidbody3D == null)
            {
                rigidbody3D = GetComponent<Rigidbody>();
            }
            return rigidbody3D;
        }
        
    }

    public override void Move(Vector3 direction)
    {
        if (isStop) return;
        this.direction = direction;
        
        Rigidbody3D.velocity = direction * speed * Time.fixedDeltaTime + new Vector3(0,Rigidbody3D.velocity.y,0);
    }
}
