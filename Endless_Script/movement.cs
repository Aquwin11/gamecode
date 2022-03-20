using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{
    private Rigidbody rb;
    private movement move;
    private bool moveforward;
    public float speed;
    public float Jumpforce;
    private Vector3 Startpos;
    private Vector3 currentpos;
    public float sideshift;
    private float maxside=19f;
    private float minside=-19f;

    public Transform checkifground;
    private float raylenght=0.5f;
    public bool isGround;
    public LayerMask ground;
    private bool isJumping;
    private bool isChanging;

    public Text coinpoints;
    private int coincounter;


    public GameObject endcanvas;

    private GameObject coinparticle;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveforward = true;
        Startpos = transform.position;
        coinparticle = transform.GetChild(4).gameObject;
        move = GetComponent<movement>();
    }

    // Update is called once per frame
    void Update()
    {
        currentpos = transform.position;
        //Debug.Log(Startpos);
        //Debug.Log(currentpos);
        if(isGround)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                currentpos += new Vector3(-sideshift, rb.velocity.y, rb.velocity.z);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) && currentpos.x == Startpos.x)
            {
                currentpos += new Vector3(sideshift, rb.velocity.y, rb.velocity.z);
            }
        }
        ckeckside();
        transform.position = currentpos;
        if(Input.GetKey(KeyCode.S))
        {
            transform.localScale = new Vector3(1.5f, 0.4f, 1f);
            isChanging = true;
        }
        else if(Input.GetKey(KeyCode.W))
        {
            transform.localScale = new Vector3(.5f, 1.5f, 1f);
            isChanging = true;
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            isChanging = false;
        }
        checkifgroundis();
        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
        }
        coincollecter();
    }

    private void FixedUpdate()
    {
        if (moveforward)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
        }

        if(isJumping)
        {
            rb.velocity += Vector3.up * Jumpforce;
            isJumping = false;
        }
    }
    void ckeckside()
    {
    if (currentpos.x > maxside)
    {
        currentpos.x = maxside;
    }
    else if (currentpos.x < minside)
    {
        currentpos.x = minside;
    }
    }
    void checkifgroundis()
    {
     if(Physics.Raycast(checkifground.position,Vector3.down,raylenght,ground))
        {
            isGround = true;
        }
     else
        {
            isGround = false;
        }
    }
    void coincollecter()
    {
        coinpoints.text = "" + coincounter;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(checkifground.position, checkifground.position + Vector3.down * raylenght);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin")
        {
            coincounter++;
            StartCoroutine(spawparticle());
        }
    }
    IEnumerator spawparticle()
    {
        coinparticle.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        coinparticle.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="enemy"|| collision.gameObject.tag == "obstecle")
        {
            Endstate();
        }
    }
    void Endstate()
    {
        endcanvas.SetActive(true);
        Time.timeScale = 0f;
        move.enabled = false;
    }
}
