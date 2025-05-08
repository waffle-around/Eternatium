using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAppear : MonoBehaviour
{
    public GameObject gun;
    public float idleDelay = 1f;

    PlayerController playerController;
    private Coroutine idleCoroutine;
    private bool isIdle = false;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!playerController.isAlive)
        {
            if (gun.activeSelf)
                gun.SetActive(false);

            // Cancel coroutine if running
            if (idleCoroutine != null)
            {
                StopCoroutine(idleCoroutine);
                idleCoroutine = null;
            }

            return;
        }

        if (playerController.isAttacking)
        {
            if (gun.activeSelf)
                gun.SetActive(false);

            if (idleCoroutine != null)
            {
                StopCoroutine(idleCoroutine);
                idleCoroutine = null;
            }

            isIdle = false;
            return;
        }

        float verticalInput = Input.GetAxisRaw("Vertical");

        if (Mathf.Approximately(verticalInput, 0f))
        {
            if (!isIdle)
            {
                isIdle = true;
                idleCoroutine = StartCoroutine(ShowGunAfterDelay());
            }
        }
        else // Player is moving
        {
            isIdle = false;
            if (idleCoroutine != null)
            {
                StopCoroutine(idleCoroutine);
                idleCoroutine = null;
            }
            if (gun.activeSelf)
                gun.SetActive(false);
        }
    }

    IEnumerator ShowGunAfterDelay()
    {
        yield return new WaitForSeconds(idleDelay);
        if (isIdle && playerController.isAlive && !playerController.isAttacking)
        {
            gun.SetActive(true);
        }
    }
}
