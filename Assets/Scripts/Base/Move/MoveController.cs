using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] float speed;
    protected virtual void Move(Vector3 direction)
    {
        transform.position += speed * Time.deltaTime * direction.normalized;
    }
}
