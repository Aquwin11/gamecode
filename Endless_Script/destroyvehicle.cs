﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyvehicle : MonoBehaviour
{
    public GameObject vehicles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="enemy")
        {
            Destroy(GameObject.FindGameObjectWithTag("enemy"));
        }
    }

}
