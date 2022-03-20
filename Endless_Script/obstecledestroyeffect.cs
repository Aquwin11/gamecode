using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstecledestroyeffect : MonoBehaviour
{
    private GameObject destroyeffect;
    // Start is called before the first frame update
    void Start()
    {
        destroyeffect = transform.GetChild(1).gameObject;
        destroyeffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "obstecle")
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
