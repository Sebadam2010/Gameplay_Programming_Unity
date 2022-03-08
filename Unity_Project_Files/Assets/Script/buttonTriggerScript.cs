using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonTriggerScript : MonoBehaviour
{

    public bool triggerEntered = false;
    public bool buttonPressed = false;
    public GameObject interactUI;
    public Animator doorAnimator;
    // Start is called before the first frame update
    void Start()
    {
        interactUI.SetActive(false);
        doorAnimator = GameObject.FindWithTag("Door").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!buttonPressed)
            {
                enableInteractUI();
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            disableInteractUI();
        }

    }

    private void enableInteractUI()
    {
        triggerEntered = true;
        interactUI.SetActive(true);


    }

    public void disableInteractUI()
    {
        triggerEntered = false;
        interactUI.SetActive(false);
    }

    public void OpenDoor()
    {
        doorAnimator.SetBool("ButtonPressed", true);
        buttonPressed = true;
        disableInteractUI();
    }
}
