using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : BulletController
{

    FillerTargetController fillerTarget;
    [SerializeField] float rotationSpeed = 30f;
    private void Start()
    {
        fillerTarget = GetComponent<FillerTargetController>();
    }
    protected override void Update()
    {
        base.Update();
        Transform target = TargetController.GetTarget(fillerTarget);
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            float angle = Vector3.SignedAngle(transform.up, direction, Vector3.forward);
            if (angle>1){
                transform.Rotate(Vector3.forward*rotationSpeed*Time.deltaTime);
            }else if (angle < -1)
            {
                transform.Rotate(-Vector3.forward*rotationSpeed * Time.deltaTime);
            }
        }
    }

    public override void DestroyBullet()
    {
        Create.Instance.CreateExplosionRocket(transform);
        Destroy(gameObject);
    }
}
