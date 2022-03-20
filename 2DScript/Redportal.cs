using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redportal : MonoBehaviour
{

    public Transform Blueportal,player;
    public float positionoffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            Vector3 offset = new Vector3(1, 0, 0);
            player.position = new Vector3(Blueportal.position.x+positionoffset,Blueportal.position.y,0);
        }
    }
}
