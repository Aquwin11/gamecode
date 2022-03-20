using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obsticlespawner : MonoBehaviour
{
    public GameObject[] obsticles;
    public float spawntimer;
    private float spawnbuffer;
    public Transform cubeposition;
    public float obstecleoffsetposition;
    public float obstecledelete;
    public float newoffsetx;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        //Debug.Log(spawnbuffer+"spawnbuffer");
        if (spawnbuffer > spawntimer)
        {
            float[] vehicle = { -19, 0f, 19f };
            int numberindex = Random.Range(0, vehicle.Length);
            float Randomposition = vehicle[numberindex];
            int randobstecle = Random.Range(0, obsticles.Length);
            GameObject rand = Instantiate(obsticles[randobstecle]);
            rand.transform.position = new Vector3(Randomposition, 0.36f, transform.position.z + obstecleoffsetposition);
            spawnbuffer = 0;
            Destroy(rand, obstecledelete);
        }
        spawnbuffer += Time.deltaTime;

    }
}
