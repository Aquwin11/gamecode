using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class spawner : MonoBehaviour
{

    public GameObject GroundtoSpawn;
    public GameObject apple;
    public Vector2 randposition;
    private Transform snakepos;
    private GameObject rand;

    // Start is called before the first frame update

    private void Awake()
    {
        snakepos = GameObject.Find("snake").transform;
        
    }
    void Start()
    {
        
        spawnapple();
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 diffpos = new Vector2(2.2f, 2.2f);
        float diffx = Mathf.Abs(randposition.x - snakepos.position.x);
        float diffy = Mathf.Abs(randposition.y - snakepos.position.y);
        if (diffx < diffpos.x && diffy < diffpos.y)
        {
            spawnapple();
        }
    }

    public void spawnapple()
    {
        Collider2D spawnarea = GroundtoSpawn.GetComponent<BoxCollider2D>();
        float ScreenX, ScreenY;
        ScreenX = Random.Range(spawnarea.bounds.min.x, spawnarea.bounds.max.x);
        ScreenY = Random.Range(spawnarea.bounds.min.y, spawnarea.bounds.max.y);
        randposition = new Vector2(ScreenX, ScreenY);
        GameObject rand = Instantiate(apple);
        rand.transform.position = randposition;
    }
}
