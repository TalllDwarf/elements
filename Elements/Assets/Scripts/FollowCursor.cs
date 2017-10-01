using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour {
    public Texture2D cursorTextureFire;
    public Texture2D cursorTextureWater;
    public Texture2D cursorTextureEarth;
    public Texture2D cursorTextureWind;
    Texture2D currentTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
	// Use this for initialization
	void Start ()
    {
        Cursor.visible = true;
        currentTexture = cursorTextureWind;
        Cursor.SetCursor(currentTexture, Vector2.zero, cursorMode);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            currentTexture = cursorTextureWind;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentTexture = cursorTextureFire;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentTexture = cursorTextureEarth;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentTexture = cursorTextureWater;
        }
        Cursor.SetCursor(currentTexture, Vector2.zero, cursorMode);
    }
}
