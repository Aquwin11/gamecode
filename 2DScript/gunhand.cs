using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunhand : MonoBehaviour
{
    public crosshairposition pointer;
    public Transform boxy;
    public float distance;
    RectTransform rectTransform;
    SpriteRenderer sp;
    private Vector2 lookdir;

    Rigidbody2D boxyRB;
    float gravityvalue;
    float intaildrag;
    float intailangulardrag;

    [Header("Grapple")]
    [SerializeField]
    private GameObject gunpoint;
    private Vector3 grapplepoint;
    private LineRenderer lr;
    public LayerMask whatisgrapple;
    public SpringJoint2D dj;
    private bool isgrapple;
    private float grappledistance;
    public float grapplespeed;
    private bool grappledash;


    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        sp = GetComponent<SpriteRenderer>();
        lr.enabled = false;
        gravityvalue = boxyRB.gravityScale;
        intaildrag = boxyRB.drag;
        intaildrag = boxyRB.angularDrag;
    }
    private void Awake()
    {
        boxyRB = boxy.GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousepos = pointer.transform.position; 
        lookdir = pointer.transform.position - transform.position;
        float rads = Mathf.Atan2(lookdir.y, lookdir.x);
        float angle =rads * Mathf.Rad2Deg;
        float revangle = Mathf.Atan2(lookdir.x, lookdir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        //rectTransform.anchoredPosition = new Vector2(lookdir.normalized.x * (boxy.position.x / boxy.position.x-2), lookdir.normalized.y * (boxy.position.y / boxy.position.y-2)) * -1;
        if(boxy.localScale.x==-1)
        {
            Vector3 flips = transform.localScale;
            flips.x = -.7f;
            transform.localScale = flips;
            sp.flipY=true;
            //sp.flipX = true;
            rectTransform.anchoredPosition = new Vector2(lookdir.normalized.x * (boxy.position.x / boxy.position.x-distance), lookdir.normalized.y *-1f* (boxy.position.y / boxy.position.y-distance));
            if(isgrapple)
            {
                sp.flipY = true;
                float newrads = Mathf.Atan2(grapplepoint.y, grapplepoint.x);
                float newangle = newrads * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, newangle);
                rectTransform.anchoredPosition = new Vector2(grapplepoint.normalized.x * (boxy.position.x / boxy.position.x - distance), grapplepoint.normalized.y * -1f * (boxy.position.y / boxy.position.y - distance));

                /*if(boxy.position.y<=-.5f)
                {
                    rectTransform.anchoredPosition = new Vector2(grapplepoint.normalized.x * (boxy.position.x / boxy.position.x - distance), grapplepoint.normalized.y * -1f * (boxy.position.normalized.y / boxy.position.normalized.y + distance));

                }*/
            }
        }
        else
        {
            Vector3 flips = transform.localScale;
            flips.x = .7f;
            transform.localScale = flips;
            sp.flipY = false;
            //sp.flipX = false;
            rectTransform.anchoredPosition = new Vector2(lookdir.normalized.x * (boxy.position.x / boxy.position.x-distance), lookdir.normalized.y * (boxy.position.y /boxy.position.y-distance)) * -1;
            if (isgrapple)
            {
                sp.flipY = false;
                float newrads = Mathf.Atan2(grapplepoint.y, grapplepoint.x);
                float newangle = newrads * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, newangle);
                rectTransform.anchoredPosition = new Vector2(grapplepoint.normalized.x * (boxy.position.x / boxy.position.x - distance), grapplepoint.normalized.y * (boxy.position.y / boxy.position.y - distance)) * -1;
                /*if (boxy.position.y <= -.5f)
                {
                    rectTransform.anchoredPosition = new Vector2(grapplepoint.normalized.x * (boxy.position.x / boxy.position.x - distance), grapplepoint.normalized.y * (boxy.position.normalized.y / boxy.position.normalized.y + distance)) * -1;

                }*/
            }
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            isgrappling();
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            notgrapple();
        }
        Drawrope();
        
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            grappledash = false;
        }
        //StopLookingatme();

    }
    private void FixedUpdate()
    {
        if (isgrapple)
        {
            boxyRB.gravityScale = 25f;
            boxyRB.drag = 0;
            boxyRB.angularDrag = 0;
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                grappledash = true;
            }
        }
        else if (!isgrapple)
        {
            boxyRB.gravityScale = gravityvalue;
            boxyRB.drag = intaildrag;
            boxyRB.angularDrag = intailangulardrag;
        }

        if (grappledash)
        {
            //Vector3 movefir = Vector3.MoveTowards(boxy.position, grapplepoint, grapplespeed * Time.deltaTime);
            Vector3 movetograpplepoint = (boxy.position - transform.position);
            dj.distance = movetograpplepoint.magnitude + .75f;
        }
    }
    void StopLookingatme()
    {
        if(lookdir.x<0.7 && lookdir.x>0.1 && boxy.localScale.x == 1)
        {
            lookdir.x = 0.7f;
        }
        else if (lookdir.x < -0.7 && lookdir.x > -0.1 && boxy.localScale.x == -1)
        {
            lookdir.x = -0.7f;
        }
        else if (lookdir.y < -0.7 && lookdir.y > -0.1 && boxy.localScale.x == -1)
        {
            lookdir.x = -0.7f;
        }
        else if (lookdir.y < 0.7 && lookdir.y > 0.1 && boxy.localScale.x == 1)
        {
            lookdir.x = 0.7f;
        }
    }
    void isgrappling()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(gunpoint.transform.position, lookdir, 7.5f, whatisgrapple);
        if (hit.collider != null)
        {
           
            isgrapple = true;
            grapplepoint = hit.point;
            grappledistance = Vector2.Distance(transform.position, grapplepoint);
            dj.autoConfigureConnectedAnchor = false;
            dj.connectedAnchor = grapplepoint;

            dj.distance = grappledistance;
            dj.frequency = 0f;
            dj.autoConfigureDistance = false;
            dj.enabled = true;
            lr.enabled = true;
        }
    }
    void notgrapple()
    {
        isgrapple = false;
        dj.enabled = false;
        lr.enabled = false;
    }
    void Drawrope()
    {
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, grapplepoint);
    }
}
