using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ThirdPersonController : MonoBehaviour
{
    private PlayerControls playerControls;
    private InputAction move;

    private Rigidbody rb;
    [SerializeField]
    private float movementForce = 1f;
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private float maxSpeed = 5f;
    private Vector3 forceDirection = Vector3.zero;
    [SerializeField]
    private Camera playerCamera;
    private Animator animator;
    bool can_jump = true;
    bool double_jump = false;

    int total_jumps = 1;

    public GameObject speedBoostImage;
    public Image speedBoostImagei;
    public GameObject doubleJumpImage;

    Color colorToFadeTo;
    float fadeTime = 5f;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        playerControls = new PlayerControls();
        animator = this.GetComponent<Animator>();

        speedBoostImagei = speedBoostImage.GetComponent<Image>();
        colorToFadeTo = new Color(1f, 1f, 1f, 0f);

    }

    private void OnEnable()
    {
        playerControls.Player.Jump.started += DoJump;
        playerControls.Player.Fire.started += DoAttack;
        move = playerControls.Player.Move;
        playerControls.Player.Enable();
    }

    private void OnDisable()
    {
        playerControls.Player.Jump.started -= DoJump;
        playerControls.Player.Fire.started -= DoAttack;
        playerControls.Player.Disable();
    }

    private void Update()
    {
        if(IsGrounded())
        {
            animator.SetBool("isFalling", false);
        }
        else
        {
            animator.SetBool("isFalling", true);
        }
    }

    private void FixedUpdate()
    {
        //Debug.Log(total_jumps);

        //pushing character relative to camera
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);

        //dont continue to accelerate after letting go of controller
        forceDirection = Vector3.zero;


        //rigidbody is falling
        if (rb.velocity.y < 0f)
        {
            //increasing acceleration of fall
            rb.velocity -= Vector3.down * Physics.gravity.y * 1.5f * Time.fixedDeltaTime;
            

            animator.SetBool("isFalling", true);
        }
            
        Vector3 horizontalVelocity = rb.velocity;

        //only have horizontal
        horizontalVelocity.y = 0;

        //checking if we are faster than max speed
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            
            //Setting it back to max speed
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;

        LookAt();

    }

    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);

        else
            rb.angularVelocity = Vector3.zero;
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        if((IsGrounded()||double_jump) && total_jumps > 0)
        {
            animator.SetBool("isJumping", true);
            forceDirection += Vector3.up * jumpForce;

            Debug.Log("Current jumps: " + total_jumps);

            total_jumps--;

            Debug.Log("now Current jumps: " + total_jumps);
            if (!IsGrounded() && total_jumps < 2)
            {
                DoubleJumpDisable();
            }
            
        }
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.5f))
        {
            animator.SetBool("isGrounded", true);
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);

            if (double_jump)
            {
                total_jumps = 2;
            } else
            {
                total_jumps = 1;
            }
            
            return true;
        }
            
        else
        {
            animator.SetBool("isGrounded", false);
            return false;
        }
            
    }

    private void DoAttack(InputAction.CallbackContext obj)
    {
        animator.SetTrigger("attack");
    }

    public void CallPowerup(string type)
    {
        if (type == "SPEED")
        {
            Debug.Log("SPEED");
            StartCoroutine(SpeedBoost());
        }

        else if (type == "DOUBLEJUMP")
        {
            Debug.Log("DOUBLEJUMP");
            DoubleJumpEnable();
        }

    }

    IEnumerator SpeedBoost ()
    {
        float tempMovementForce = movementForce;

        movementForce = 10f;
        Debug.Log("Fast speed now");

        speedBoostImage.SetActive(true);

        speedBoostImagei.CrossFadeColor(colorToFadeTo, fadeTime, true, true);

        yield return new WaitForSeconds(5f);
        speedBoostImage.SetActive(false);

        movementForce = tempMovementForce;

    }

    void DoubleJumpEnable()
    {

        doubleJumpImage.SetActive(true);
        double_jump = true;
        total_jumps = 2;
    }

    void DoubleJumpDisable()
    {

        doubleJumpImage.SetActive(false);
        double_jump = false;
        //total_jumps = 1;
    }

}
