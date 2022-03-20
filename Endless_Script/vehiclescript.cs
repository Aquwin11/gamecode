using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vehiclescript : MonoBehaviour
{
    private float minScale = 1f;
    private float maxScale = 4.5f;
    private bool ismoving;
    public GameObject destroyeffect;
    public GameObject Endstate;


    //
    float maxspeed = 30f;
    float minspeed = 24f;
    float[] numbers = { 1f, -1f };
    float randomspeed;
    float randomposition;



    private Rigidbody vehiclerb;
    // Start is called before the first frame update
    private void Awake()
    {
        vehiclerb = GetComponent<Rigidbody>();
       
    }
    void Start()
    {
        transform.localScale = Vector3.one * Random.Range(minScale, maxScale);
        ismoving = true;
        randomspeed = Random.Range(minspeed, maxspeed);
        int positionss = Random.Range(0, numbers.Length);
        randomposition = numbers[positionss];
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        if(ismoving)
        {

            vehiclerb.velocity = new Vector3(vehiclerb.velocity.x, vehiclerb.velocity.y, randomspeed * -1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            //Endstate.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="obstecle")
        {
            StartCoroutine(destroycool());
        }
    }
    IEnumerator destroycool()
    {
        destroyeffect.SetActive(true);
        yield return new WaitForSeconds(0.65f);
        destroyeffect.SetActive(false);
    }
}
