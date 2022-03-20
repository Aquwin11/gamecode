using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelgenarator : MonoBehaviour
{
    public GameObject roads;
    public Transform checkwhichground;
    public LayerMask checkinglevel;
    public float levelspawndeleter;
    public float raylength;
    private Vector3 newloc;
    public float locoffset;
    // Start is called before the first frame update
    void Start()
    {
       //Instantiate(roads, transform.position, roads.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 current =checkwhichground.position;
        current.z += 35;
        checkwhichground.position=current;
        if(Physics.Raycast(checkwhichground.position,Vector3.down, raylength, checkinglevel))
        {
            Debug.Log(current);
        }*/
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(checkwhichground.position, checkwhichground.position + Vector3.down*raylength);
    }*/


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            newloc = roads.transform.position;
            newloc.z = newloc.z + locoffset;
            Instantiate(roads, newloc, roads.transform.rotation);
            StartCoroutine(Removelevel());
        }
    }
    IEnumerator Removelevel()
    {
        yield return new WaitForSeconds(levelspawndeleter);
        Destroy(roads,levelspawndeleter);
    }
    
}
