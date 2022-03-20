using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class applescript : MonoBehaviour
{

    private Vector3 applegridpos;
    private Transform snakepos;
    
    // Start is called before the first frame update

    private void Awake()
    {
        snakepos = GameObject.Find("snake").transform;
        
    }
    void Start()
    {
        applegridpos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 diffpos = new Vector2(2.2f, 2.2f);
        float diffx = Mathf.Abs(applegridpos.x - snakepos.position.x);
        float diffy = Mathf.Abs(applegridpos.y - snakepos.position.y);
        if (diffx < diffpos.x && diffy < diffpos.y)
        {
            Destroy(gameObject);
        }
    }
}
