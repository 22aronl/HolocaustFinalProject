using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    public GameObject location;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.transform.position = new Vector3(location.transform.position.x - col.gameObject.transform.localScale.x/2, col.gameObject.transform.position.y,  col.gameObject.transform.position.z);
        if(col.name == "Player")
        {
            col.gameObject.GetComponent<PlayerController>().newDay();
        }
    }
}

