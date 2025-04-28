using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector3 rotation;
    private bool groundedPlayer;
    private float playerSpeed;
    public float walkSpeed = 2.0f;
    public float sprintSpeed = 4.0f;
    public float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    public float rotationSpeed = 180;
    private bool isAlive = true;
    private Animator animator;
    public float deathVelocity;
    private bool canMove = true;
   
    private void Start()
    {
        playerSpeed = walkSpeed;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (!isAlive || !canMove) return; 

        float forwardMovement = Input.GetAxis("Vertical");
        animator.SetFloat("ForwardMovement", forwardMovement);

        playerVelocity.x = 0;
        playerVelocity.z = 0;
        Vector3 movementDirection = transform.forward * forwardMovement * playerSpeed * Time.deltaTime;
        playerVelocity.x = movementDirection.x;
        playerVelocity.z = movementDirection.z;
        this.rotation = new Vector3(0, Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime, 0);
        this.transform.Rotate(this.rotation);

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            animator.SetTrigger("Jump");
        }

        playerVelocity.y += gravityValue * Time.deltaTime;

        controller.Move(playerVelocity/* * Time.deltaTime*/);

        if (playerVelocity.y < deathVelocity)
        {
            isAlive = false;
            animator.SetBool("Dead", true);
        }

        if (Input.GetButtonDown("Fire2") && groundedPlayer)
        {
            animator.SetTrigger("Attacking1");
            canMove = false;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            animator.SetBool("Posing", true);
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            animator.SetBool("Posing", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("Running", true);
            playerSpeed = sprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("Running", false);
            playerSpeed = walkSpeed;
        }

            animator.SetBool("InAir", !groundedPlayer);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            isAlive = false;
            animator.SetBool("Dead", true);
        }
    }

    void OnDeathAnimComplete()
    {
        string activeSeceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(activeSeceneName);
    }
     public void EnableMovement()
    {
        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;
    }
}
