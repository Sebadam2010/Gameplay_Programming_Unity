using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using RPGCharacterAnimsFREE;

public class PlayerController2 : MonoBehaviour
{

    Animator animator;
    RPGCharacterController controller;

    PlayerControls controls;
    Vector2 move;
    float inputHorizontal = 0f;
    float inputVertical = 0f;
    public float speed = 10;



    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<RPGCharacterController>();


        controls = new PlayerControls();
        controls.Player.Move.performed += ctx => move =
    ctx.ReadValue<Vector2>();

        controls.Player.Move.canceled += ctx => move = Vector2.zero;
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    void SendMessage(Vector2 coordinates)
    {
        Debug.Log("Thumb-stick coordinates = " + coordinates);
    }

    void onJump()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        inputVertical = move.y;
        inputHorizontal = move.x;
        // Vector3 movement = new Vector3(move.x, 0.0f, move.y) * speed * Time.deltaTime;
        Vector3 movement2 = new Vector3(inputHorizontal, inputVertical, 0f);

        



        /* animator.SetFloat("Velocity X", inputHorizontal);
         animator.SetFloat("Velocity Z", inputVertical);
         transform.Translate(movement, Space.World);*/

        controller.SetMoveInput(movement2);

        if (controls.Player.Jump.triggered)
        {
            Debug.Log("Jump");
            animator.SetInteger("Jumping", 1);
        }
    }
}
