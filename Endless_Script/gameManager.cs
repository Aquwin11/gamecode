using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{

    public GameObject pausestate;
    public movement move;
    // Start is called before the first frame update

    private void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            Time.timeScale = 0f;
            pausestate.SetActive(true);
            move.enabled = false;
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
        move.enabled = true;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        pausestate.SetActive(false);
        move.enabled = true;
    }
}
