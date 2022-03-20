using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour
{
    public movement move;

    [SerializeField]
    private GameObject endscreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        //SceneManager.GetActiveScene();
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
        move.enabled = true;
        endscreen.SetActive(false);
    }
}
