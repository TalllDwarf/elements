using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitToMenu : MonoBehaviour {

    public void QuitToMainMenu()
    {
        Application.LoadLevel(0);
    }
}
