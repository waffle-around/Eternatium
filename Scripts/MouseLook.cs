using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public PlayerController playerController;
    public GameController gameController;
    public Camera FPCamera;
    public float mouseSensitivity = 7f;
    public Transform player;
    public bool canMove = true;
    public float maxLookUp = -20f;
    public float maxLookDown = 40f;
    public float normalNCP = 0.22f;
    public float sprintNCP = 0.39f;
    public float attackNCP = 0.45f;
    private bool isAttacking = false;

    private float xRotation = 0f;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        playerController = player.GetComponent<PlayerController>();
        gameController = FindObjectOfType<GameController>();
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    void Update()
    {
        if (!canMove) return;

        
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, maxLookUp, maxLookDown);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            player.Rotate(Vector3.up * mouseX);
        

        if (FPCamera != null && playerController != null)
        {
            if (!isAttacking) 
            {
                if (playerController.sprintActive)
                {
                    FPCamera.nearClipPlane = sprintNCP;
                }
                else
                {
                    FPCamera.nearClipPlane = normalNCP;
                }
            }
            if (!playerController.isAlive && gameController != null)
            {
                gameController.EnableC1();
            }
        }
       
    }

    public void attackSight()
    {
        isAttacking = true;
        if (FPCamera != null)
        {
            FPCamera.nearClipPlane = attackNCP;
        } 
    }

    public void regSight()
    {
        isAttacking = false;
        if (FPCamera != null)
        {
            FPCamera.nearClipPlane = normalNCP;
        }    
    }
}
