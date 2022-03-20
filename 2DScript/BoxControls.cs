using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxControls : MonoBehaviour
{
    public float speed;
    public float Jumpforce;
    private Rigidbody2D rb;
    private Animator anim;
    private BoxControls move;
    [SerializeField]
    private bool facingright = true;
    private float fullmultiplier = 2.5f;
    private float halfmultiplier = 2f;


    [Header("Groundcheck")]
    private bool isGrounded;
    public Transform checkifground;
    public float circleradius;
    public LayerMask GroundLayer;
    private bool isJumping;

    [Header("Hangtime")]
    public float hangtime;
    private float hangcounter;

    [Header("corner correction")]
    public Transform Innerline;
    public Transform Outerline;
    public Transform BOuterline;
    public Transform BInnerline;

    [Header("Dash")]
    [SerializeField]
    private float dashlength;
    [SerializeField]
    private float bufferBtwdashs;
    public float dashforce;
    private bool candash = true;
    private bool isDashing;
    private float normalgravity;
    private float normaldrag;
    IEnumerator dashcoroutine;


    [Header("Wall Interactions")]
    public bool iswalling;
    public Transform Rightwallpointer;
    public LayerMask walllayer;
    private SpriteRenderer myspriterenderer;


    public bool iswalljumping;
    public float xwalljumpforce;
    public float ywalljumpforce;
    [SerializeField]
    private float walljumpduration;

    [Header("The portals")]
    public  GameObject crossairobject;
    public GameObject Redportal;
    public GameObject Blueportal;
    private bool Redportalready=true;
    public LayerMask everything;
    public bool scriptready=false;

    // Start is called before the first frame update
    void Start()
    {
        normalgravity=rb.gravityScale;
        normaldrag=rb.drag;
    }
    //
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myspriterenderer=GetComponent<SpriteRenderer>();
        move = GetComponent<BoxControls>();
        crossairobject = GameObject.FindGameObjectWithTag("crosshair");
    }
    // Update is called once per frame
    private void Update()
    {
        //corner correction
        rightcornercorrection();
        if(Input.GetKeyDown(KeyCode.Space) && hangcounter>0f)
        {
            isJumping=true;
        }
        if(Input.GetKeyUp(KeyCode.Space) && rb.velocity.y>0f)
        {
            rb.velocity=new Vector2(rb.velocity.x,rb.velocity.y*0.5f);
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && candash==true)
        {
             if(dashcoroutine != null)
            {
                StopCoroutine(dashcoroutine);
            }
            dashcoroutine=Dash();
            StartCoroutine(dashcoroutine);
        }
        if(iswalling)
        {
            wallslide();
        }
        else if(!iswalling)
        {
            myspriterenderer.flipX=false;
        }
        //better jump
        if(rb.velocity.y<0)
        {
            rb.velocity += Vector2.up*Physics2D.gravity*(fullmultiplier-1)*Time.deltaTime;
        }
        if(rb.velocity.y>0 && Input.GetKeyUp(KeyCode.Space))
        {
            rb.velocity += Vector2.up*Physics2D.gravity*(halfmultiplier-1)*Time.deltaTime;
        }


        RaycastHit2D portaldis = Physics2D.Raycast(transform.position, crossairobject.transform.position, everything);
        float Distancefromboxtocrosshair = Vector2.Distance(transform.position, crossairobject.transform.position);
        //Debug.Log(Distancefromboxtocrosshair);
        if (Input.GetKeyDown(KeyCode.Q) && portaldis.collider != null)
        {
            Debug.Log(Distancefromboxtocrosshair);
        }
        /*if(isGrounded && Input.GetKeyDown(KeyCode.Q) && Redportalready && portaldis.collider==null)
        {
            Debug.Log(Distancefromboxtocrosshair);
        }
        else
        {
            scriptready = false;
        }
        else if (isGrounded && Input.GetKeyDown(KeyCode.Q) && !Redportalready && portaldi.collider == null)
        {
            BluePortal();
        }
        if(scriptready==true)
        {
        }
        */
    }
    private void FixedUpdate()
    {
        isGrounded=Physics2D.OverlapCircle(checkifground.position,circleradius,GroundLayer);
        float hmove = Input.GetAxis("Horizontal");

            rb.velocity = new Vector2(hmove * speed, rb.velocity.y);

            //hangtime
        if(isGrounded)
        {
            hangcounter=hangtime;
        }
        else
        {
            hangcounter-=Time.deltaTime;
        }
            //jumpbuffer

        if(hmove>0&&!facingright)
        {
            flip();
        }
        else if(hmove<0&&facingright)
        {
            flip();
        }
        if(isJumping)
        {
            rb.velocity =  Vector2.up*Jumpforce;
            isJumping=false;
        }
        
        if(isDashing)
        {
           if(facingright) 
           rb.velocity = new Vector2(dashforce,0);
           else if(!facingright)
           rb.velocity = new Vector2(-dashforce,0);
        }
        //wallinteraction
        RaycastHit2D wallhit=Physics2D.Raycast(Rightwallpointer.position,Vector3.right,0.25f,walllayer);
        if(wallhit.collider != null && !isGrounded && hmove!=0)
        {
            iswalling=true;
        }
        else{
            iswalling=false;
        }
        if(iswalljumping)
        {
            rb.velocity=new Vector2(xwalljumpforce*-hmove,ywalljumpforce);
            StartCoroutine(walljumpfalse());
        }
    }
    void Portal()
    {
        GameObject redp=Instantiate(Redportal);
        redp.transform.position =crossairobject.transform.position;
        Redportalready = false;
    }
    void BluePortal()
    {
        GameObject Bluep = Instantiate(Blueportal);
        Bluep.transform.position = crossairobject.transform.position;
        Redportalready = true;
    }
    void flip()
    {
        facingright = !facingright;
        Vector3 flips = transform.localScale;
        flips.x *= -1;
        transform.localScale = flips;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Outerline.position,Outerline.position + Vector3.up * 0.5f);
        Gizmos.DrawLine(Innerline.position,Innerline.position+Vector3.up*0.5f);
        Gizmos.DrawLine(BOuterline.position,BOuterline.position + Vector3.up * 0.5f);
        Gizmos.DrawLine(BInnerline.position,BInnerline.position+Vector3.up*0.5f);

        Gizmos.color=Color.green;
        Gizmos.DrawLine(Rightwallpointer.position,Rightwallpointer.position+Vector3.right*.25f);


        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, crossairobject.transform.position);
    }

    void rightcornercorrection()
    {
        RaycastHit2D fhit = Physics2D.Raycast(Outerline.position,Vector3.up,0.5f,GroundLayer);
        RaycastHit2D fhits= Physics2D.Raycast(Innerline.position,Vector3.up,0.5f,GroundLayer);
        RaycastHit2D bhit = Physics2D.Raycast(BOuterline.position,Vector3.up,0.5f,GroundLayer);
        RaycastHit2D bhits= Physics2D.Raycast(BInnerline.position,Vector3.up,0.5f,GroundLayer);

        float Fdistance = Vector2.Distance(Innerline.position, BOuterline.position);
        //Debug.Log(Fdistance);
        if (fhit.collider !=null && fhits.collider==null &&facingright)
        {
            Vector3 newpos=transform.position;
            newpos.x=Innerline.position.x-Fdistance;            //corrner correction was changing base on the is orginal position
            transform.position= newpos;
        }
        if(fhit.collider !=null && fhits.collider==null &&!facingright)
        {
            Vector3 newpos=transform.position;
            newpos.x=Innerline.position.x+Fdistance;            //corrner correction was changing base on the is orginal position
            transform.position= newpos;
        }
        if(bhit.collider !=null && bhits.collider==null &&facingright)
        {
            Vector3 newpos=transform.position;
            newpos.x=BInnerline.position.x+Fdistance;
            transform.position= newpos;
        }
        if(bhit.collider !=null && bhits.collider==null &&!facingright)
        {
            Vector3 newpos=transform.position;
            newpos.x=BInnerline.position.x-Fdistance;
            transform.position= newpos;
        }
        
    }
    IEnumerator Dash()
    {
        isDashing= true;
        candash = false;
        rb.gravityScale=0;
        rb.drag=0;
        yield return new WaitForSeconds(dashlength);
        isDashing = false;
        rb.velocity=Vector2.zero;
        rb.gravityScale=normalgravity;
        rb.drag=normaldrag;
        yield return new WaitForSeconds(bufferBtwdashs);
        candash = true;
    }
    void wallslide()
    {
        rb.velocity=new Vector2(0,rb.velocity.y*0.85f);
        myspriterenderer.flipX=true;
        if(Input.GetKeyDown(KeyCode.Space))
        {
           iswalljumping=true;
        }
    }
    IEnumerator walljumpfalse()
    {
        iswalljumping=true;
        move.enabled = false;
        yield return new WaitForSeconds(walljumpduration+0.03f);
        rb.velocity=Vector2.zero;
        move.enabled = true;
        yield return new WaitForSeconds(.05f);
        if(!iswalling)
        {
            iswalljumping=false;
        }
    }
}
