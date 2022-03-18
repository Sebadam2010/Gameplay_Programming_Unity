using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class ButtonScript : MonoBehaviour
{

    
    private GameObject triggerGO;
    private buttonTriggerScript triggerScript;
    private Animator buttonAnimator;

    public PlayableDirector timeline;


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

        timeline.Play();
        triggerScript.disableInteractUI();
    }


}
