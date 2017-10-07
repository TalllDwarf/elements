using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Quit : MonoBehaviour {

    public void quitGame()
    {
        Debug.Log("Has Quit The Game");
        Application.Quit();
    }


}
