using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

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
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                Debug.Log("playing");
            }
        }
    }

    public void QuitToMainMenu()
    {
        Application.LoadLevel(1);
    }
}
