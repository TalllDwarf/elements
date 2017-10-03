using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    Vector3 mousePos;
    string currentElement;
    int elementSwitch;
    GameObject princess;
    

    // Use this for initialization
    void Start ()
    {
        princess = GameObject.FindGameObjectWithTag("Princess");
    }
	
	// Update is called once per frame
	void Update ()
    {
        currentElement = princess.GetComponent<FollowCursor>().element;

	}
    void OnMouseDown()
    {
        //Destroy(gameObject);
       // Debug.Log(mousePos);
        switch(currentElement)
        {
            case "fire":
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case "water":
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case "wind":
                gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
                break;
            case "earth":
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue + Color.red;
                break;
            default:
                break;
        }
    }
}
