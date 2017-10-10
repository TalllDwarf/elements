using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMamager : MonoBehaviour {

    public bool isPaused;
    public GameObject pauseScreen;

    public void start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                Debug.Log("Paused");
                pauseScreen.SetActive(true);
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                Debug.Log("playing");
                pauseScreen.SetActive(false);
            }
        }
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene("Playing", LoadSceneMode.Additive);
    }

    public void Quit()
    {
        Debug.Log("Clicked");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPaused = false;
#else
       Application.Quit();
#endif
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }
}
