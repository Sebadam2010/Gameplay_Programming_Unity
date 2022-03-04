using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{

    public GameObject player;
    public enum Type
    {
        SPEED,
        DOUBLEJUMP
    };

    public Type type;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            player.GetComponent<ThirdPersonController>().CallPowerup(type.ToString());
            Destroy(transform.parent.gameObject);
            
        }
    }
}
