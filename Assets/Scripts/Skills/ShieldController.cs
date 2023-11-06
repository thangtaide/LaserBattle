using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField] float timeAcive = 3f;
    [SerializeField] GameObject shield;
    bool isShieldActive;
    private void Start()
    {
        isShieldActive = false;
        shield.SetActive(isShieldActive);
    }
    public void ActiveShield()
    {
        isShieldActive = !isShieldActive;
        OnActiveShield();
        shield.SetActive(isShieldActive);
    }
    private void OnActiveShield()
    {

        SpaceController spaceController = GetComponentInParent<SpaceController>();
        if (spaceController != null)
        {
            spaceController.canTakeDame = !isShieldActive;
        }
        if (isShieldActive)
        {
            SoundController.instance.PlaySound("sfx_shieldUp");
            StartCoroutine(DeActiveShield());
        }
    }
    IEnumerator DeActiveShield()
    {
        yield return new WaitForSeconds(timeAcive);
        SoundController.instance.PlaySound("sfx_shieldDown");
        ActiveShield();
    }
}
