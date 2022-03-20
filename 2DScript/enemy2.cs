using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2 : MonoBehaviour
{
    private Rigidbody2D enemyRB;
    public GameObject boxman;
    public GameObject leftcheck;
    public GameObject Rightcheck;
    public LayerMask player;
    private float Distancefromcharcater;
    public float enenmyspeed;
    private Vector3 startpos;

    public bool notchasing;
    public bool moveleft;
    public bool moveright;

    private float buffer = 4f;
    private float counter;
    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Distancefromcharcater = Vector2.Distance(boxman.transform.position,transform.position);
     
        RaycastHit2D Righthit = Physics2D.Raycast(Rightcheck.transform.position, Vector2.right, Distancefromcharcater, player);
        RaycastHit2D Lefthit = Physics2D.Raycast(leftcheck.transform.position, Vector2.left, Distancefromcharcater, player);
        if (Righthit.collider!=null)
        {
            moveright = true;
            moveleft = false;
        }
        else if(Lefthit.collider != null)
        {
            moveleft = true;
            moveright = false;
        }
        else
        {
            moveright = false;
            moveleft = false;
        }

        float returndistance = Vector2.Distance(transform.position, startpos);


        if(moveleft== false && moveright==false && returndistance > 0.3f)
        {

            notchasing = true;
            counter += Time.deltaTime;

        }

        if(moveright && Distancefromcharcater < 1.5f)
        {
            //Debug.Log("BOOM");
        }
        else if(moveleft && Distancefromcharcater < 1.5f)
        {
            //Debug.Log("BOOM");
        }
        if(notchasing && counter>buffer)
        {
            movetooriginalposition();
        }
    }

    private void FixedUpdate()
    {
        if(moveleft)
        {
            counter = 0;
            moveright = false;
            enemyRB.velocity = new Vector2(-enenmyspeed, enemyRB.velocity.y);
        }
        else if(moveright)
        {
            counter = 0;
            moveleft = false;
            enemyRB.velocity = new Vector2(enenmyspeed, enemyRB.velocity.y);
        }
        else
        {
            enemyRB.velocity = Vector2.zero;
            moveleft = false;
            moveright = false;
        }
    }
    void movetooriginalposition()
    {
        transform.position = Vector3.MoveTowards(transform.position,startpos, 4f * Time.deltaTime);
        notchasing = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(leftcheck.transform.position, leftcheck.transform.position + Vector3.left * Distancefromcharcater);
        Gizmos.DrawLine(Rightcheck.transform.position, Rightcheck.transform.position + Vector3.right* Distancefromcharcater);
    }
}
