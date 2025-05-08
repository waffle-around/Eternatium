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
    //public float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    public float rotationSpeed = 180;
    public bool isAlive = true;
    private Animator animator;
    public float deathVelocity;
    private bool canMove = true;
    public bool sprintActive = false;
    public Camera FPCamera;
    public MouseLook mouseLookFP;
    public bool isAttacking = false;
    //public bool isIdle;
    AudioSource audioSource;
    public AudioClip deathClip;
    public AudioClip footstep;

    private void Start()
    {
        playerSpeed = walkSpeed;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        mouseLookFP = FPCamera.GetComponent<MouseLook>();
        audioSource = GetComponent<AudioSource>();
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

        /*if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            animator.SetTrigger("Jump");
        }*/

        playerVelocity.y += gravityValue * Time.deltaTime;

        controller.Move(playerVelocity/* * Time.deltaTime*/);

        if (playerVelocity.y < deathVelocity)
        {
            death();
        }

        if (Input.GetButtonDown("Fire2") && groundedPlayer && !sprintActive)
        {
            animator.SetTrigger("Attacking1");
            canMove = false;
            mouseLookFP.attackSight();
            isAttacking = true;
        }
        else
        {
            mouseLookFP.regSight();
            isAttacking = false;
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
            isSprinting();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            notSprinting();
        }

        animator.SetBool("InAir", !groundedPlayer);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            death();
        }
    }

    public void death()
    {
        isAlive = false;
        animator.SetBool("Dead", true);
        audioSource.clip = deathClip;
        audioSource.Play();
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

    public void isSprinting()
    {
        sprintActive = true;
        animator.SetBool("Running", true);
        playerSpeed = sprintSpeed;
    }

    public void notSprinting()
    {
        sprintActive = false;
        animator.SetBool("Running", false);
        playerSpeed = walkSpeed;
    }

    public void attackSight()
    {
        if (mouseLookFP != null)
        {
            mouseLookFP.attackSight();
        }
    }

    public void regSight()
    {
        if (mouseLookFP != null)
        {
            mouseLookFP.regSight();
        }
    }

    public void PlayFootstep()
    {
        audioSource.clip = footstep;
        audioSource.Play();
    }

}
