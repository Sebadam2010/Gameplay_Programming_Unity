using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{

    
    private GameObject triggerGO;
    private buttonTriggerScript triggerScript;
    private Animator buttonAnimator;


    // Start is called before the first frame update
    void Start()
    {
        triggerGO = this.transform.GetChild(0).gameObject;
        buttonAnimator = this.gameObject.GetComponent<Animator>();
        triggerScript = triggerGO.GetComponent<buttonTriggerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonPressed()
    {
        buttonAnimator.SetBool("ButtonPressed", true);
        triggerScript.OpenDoor();

    }


}
