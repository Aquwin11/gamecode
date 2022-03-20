using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemygenerator : MonoBehaviour
{
    public GameObject vehicleprefab;
    private float vehiclespawnbuffer;
    public float vehiclespawntimer;
    public Transform player;
    public float positionoffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float[]vehicle= {-19,0f,19f};
        int numberindex = Random.Range(0, vehicle.Length);
        float Randomposition = vehicle[numberindex];
        if(vehiclespawnbuffer>vehiclespawntimer)
        {
            Instantiate(vehicleprefab, new Vector3(Randomposition, player.position.y, transform.position.z+positionoffset), vehicleprefab.transform.rotation);
            vehiclespawnbuffer = 0;
        }
        vehiclespawnbuffer += Time.deltaTime;
    }
}
