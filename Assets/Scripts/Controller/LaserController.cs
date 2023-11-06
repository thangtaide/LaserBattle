using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform laserFirePoint;
    [SerializeField] private float _maxLength = 5f;
    [SerializeField] float timeEachDamage = 0.1f;
    [SerializeField] ParticleSystem hitParticleSystem;
    bool isPlay = false;
    float dmgLaser = 20f;
    float timeToDamage;
    private void Start()
    {
        dmgLaser = GetComponentInParent<PlayerController>().currentDmg/2;
        timeToDamage = Time.time;
    }
    private void Update()
    {
        dmgLaser = GetComponentInParent<PlayerController>().currentDmg / 2;
        ShootLaser();
    }

    void ShootLaser()
    {
        RaycastHit2D hit = Physics2D.Raycast(laserFirePoint.position, laserFirePoint.transform.up); ;
        if (hit)
        {
            IHit ihit = hit.transform.GetComponent<IHit>();
            if (!hit.collider.CompareTag("Bullet") && !hit.collider.CompareTag("Player"))
            {
                Draw2DRay(laserFirePoint.position, hit.point);
                hitParticleSystem.transform.position = hit.point;
                if (!isPlay)
                {
                    hitParticleSystem.Play();
                    isPlay = true;
                }
                if (ihit != null && Time.time > timeToDamage )
                {
                    timeToDamage = Time.time + timeEachDamage;
                    ihit.OnHit(dmgLaser);
                }
            }
            else
            {
                Draw2DRay(laserFirePoint.position, laserFirePoint.position + laserFirePoint.transform.up * _maxLength);
                hitParticleSystem.Stop();isPlay = false;
            }
        }
        else
        {
            Draw2DRay(laserFirePoint.position, laserFirePoint.position+laserFirePoint.transform.up * _maxLength);
            hitParticleSystem.Stop();
            isPlay = false;
        }
    }

    void Draw2DRay(Vector2 starPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, starPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
