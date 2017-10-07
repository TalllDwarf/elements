using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitOnClick : MonoBehaviour {

    public void Quit()
    {
        Debug.Log("Clicked");
#if UNITY_EDITOR
       UnityEditor.EditorApplication.isPaused = false;
       #else
       Application.Quit();
       #endif
    }
}
