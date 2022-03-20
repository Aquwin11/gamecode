using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinscript : MonoBehaviour
{
    private float speed=80f;
    public Text coinpoints;
    private int coincounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, speed*Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            Destroy(gameObject);
            
        }
    }
}
