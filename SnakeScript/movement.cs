using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private Vector2 griddirection;
    private Vector2 gridposition;
    public float movecounter;
    public float maxcounter;
    private float movebuffer;

    private List<Transform> bodyparts=new List<Transform>();
    public Transform tail;
    public Transform startbody;

    public Transform snakepart;
    public spawner spawner;
    private GameObject effect;

    private bool speedincre = false;

    public GameObject endscreen;

    private movement move;
    // Start is called before the first frame update

    private void Awake()
    {
        
        movebuffer = movecounter;
        effect = transform.GetChild(2).gameObject;
        move= GetComponent<movement>();
    }
    void Start()
    {
        effect.SetActive(false);
        griddirection = new Vector2(-2.5f, 0);
        bodyparts.Add(this.transform);
        bodyparts.Add(startbody);
        bodyparts.Add(tail);
        tail.position = bodyparts[bodyparts.Count - 1].position;
        startbody.position = bodyparts[bodyparts.Count - 2].position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentpos = transform.position;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            if(griddirection.y!=-2.5)
            {
                
                griddirection.y = 2.5f;
                griddirection.x = 0f;
                transform.eulerAngles = new Vector3(0, 0, -90);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(griddirection.y!=2.5)
            {
                
                griddirection.y = -2.5f;
                griddirection.x = 0f;
                transform.eulerAngles = new Vector3(0, 0, 90);
               
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(griddirection.x!=2.5)
            {
                
                griddirection.x = -2.5f;
                griddirection.y = 0f;
                transform.eulerAngles = new Vector3(0, 0, 0);
                
            }   
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(griddirection.x!=-2.5)
            {
                griddirection.x = 2.5f;
                griddirection.y = 0f;
                transform.eulerAngles = new Vector3(0, 0, -180);
            }
            
        }
        transform.position = new Vector2(gridposition.x,gridposition.y);

        if(movebuffer>movecounter)
        {
            gridposition += griddirection;
            movebuffer = 0;
            for (int i = bodyparts.Count - 1; i > 0; i--)
            {
                bodyparts[i].position = bodyparts[i - 1].position;
                for (int j = 2; j < bodyparts.Count; j++)
                {
                    float distance = Vector3.Distance(bodyparts[j].position,currentpos);
                    if(distance==0)
                    {
                        deadstate();
                    }
                    
                }
            }
        }
        movebuffer += Time.deltaTime;
        onContact();

        crossboarder();
        incrementspeed();

        tailposition();
    }
    void onContact()
    {
        
        Vector2 applegridpos = spawner.randposition;
        Vector2 diffpos = new Vector2(2.2f, 2.2f);
        float diffx = Mathf.Abs(applegridpos.x - transform.position.x);
        float diffy = Mathf.Abs(applegridpos.y - transform.position.y);
        if (diffx < diffpos.x && diffy < diffpos.y)
        {
            StartCoroutine(destroyeffect());
            speedincre = true;
            grow();
        }
        else
        {
            speedincre = false;
        }
    }
    IEnumerator destroyeffect()
    {
        effect.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        effect.SetActive(false);
    }
    void crossboarder()
    {
        if(transform.position.x==-72.5f || transform.position.x == 72.5f)
        {
            deadstate();
        }
        if (transform.position.y == -42.5f || transform.position.y == 42.5f)
        {
            deadstate();
        }
    }

    void incrementspeed()
    {
        if (bodyparts.Count % 15 == 0 && speedincre && movecounter >= maxcounter)
        {
            movecounter -= 0.01f;
        }
    }
    void tailposition()
    {
        for (int i = 1; i < bodyparts.Count; i++)
        {
            if (bodyparts[i - 1].position.y > tail.position.y && bodyparts[i - 1].position.x == tail.position.x)
            {
                tail.eulerAngles = new Vector3(0, 0, 180);
            }
            if (bodyparts[i - 1].position.y < tail.position.y && bodyparts[i - 1].position.x == tail.position.x)
            {
                tail.eulerAngles = new Vector3(0, 0, 0);
            }
            if (bodyparts[i - 1].position.x > tail.position.x && bodyparts[i - 1].position.y == tail.position.y)
            {
                tail.eulerAngles = new Vector3(0, 0, 90);
            }
            if (bodyparts[i - 1].position.x < tail.position.x && bodyparts[i - 1].position.y == tail.position.y)
            {
                tail.eulerAngles = new Vector3(0, 0, -90);
            }
        }
    }

    void deadstate()
    {
        Time.timeScale = 0f;
        move.enabled = false;
        endscreen.SetActive(true);
    }
    void grow()
    {
        Transform newpart=Instantiate(this.snakepart);   
        newpart.position = bodyparts[bodyparts.Count - 1].position;
        bodyparts.Insert(bodyparts.Count - 2, newpart);
    }
}
