using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    
    GameObject princess;
    Animator anim;
    public bool clicked;
    public bool fire;
    public bool earth;
    public bool wind;
    public bool water;
    bool moving = false;
    public SelectElement selectedElement;
    public GameObject fallbreaker;
    public Sprite ice;
    public Sprite stillWater;
    // Use this for initialization
    void Start ()
    {
        princess = GameObject.FindGameObjectWithTag("Princess");
        anim = GetComponent<Animator>();
        clicked = false;

    }

    // Update is called once per frame
    void Update()
    {
        selectedElement = princess.GetComponent<FollowCursor>().element;
        
        if(moving && gameObject.tag == "SnowBoulder")
        {
            gameObject.GetComponent<Transform>().position = new Vector2(gameObject.GetComponent<Transform>().position.x + 0.1f, gameObject.GetComponent<Transform>().position.y);
 
        }
    }
    void OnMouseDown()
    {
        selectedElement = princess.GetComponent<FollowCursor>().element;
       
        switch (selectedElement)
        {

            case SelectElement.fire:
                
                
                if (gameObject.tag == "Tree")
                {
                    //StartCoroutine(Wait());
                    anim.SetInteger("element", (int)selectedElement);
                    gameObject.tag = "Stump";
                    gameObject.layer = 11;
                    gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                }
                if (gameObject.tag == "Ice")
                {
                    GetComponent<SpriteRenderer>().sprite = stillWater;
                    GetComponent<PolygonCollider2D>().enabled = false;
                        gameObject.tag = "Water";
                }
                break;
            case SelectElement.water:
                
                if (gameObject.tag == "Stump")
                {
                    anim.SetInteger("element", (int)selectedElement);
                    gameObject.tag = "Tree";
                    gameObject.layer = 9;
                    gameObject.GetComponent<PolygonCollider2D>().enabled = true;
                }
                if(gameObject.tag == "Ground")
                {
                    anim.SetInteger("element", (int)selectedElement);
                }
                if (gameObject.tag == "Water")
                {
                    GetComponent<SpriteRenderer>().sprite = ice;
                    GetComponent<PolygonCollider2D>().enabled = true;
                    gameObject.tag = "Ice";
                }
                break;
            case SelectElement.wind:
                if (gameObject.tag == "SnowBoulder")
                {
                    moving = true;
                    anim.SetInteger("element", (int)selectedElement);
                }
                if (gameObject.tag == "Tree")
                {
                    gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                    gameObject.layer = 11;
                    anim.SetInteger("element", (int)selectedElement);
                }
                if (gameObject.tag == "Ground")
                {
                    Instantiate(fallbreaker, transform.position, Quaternion.identity);
                    gameObject.tag = "Wind";
                }
                break;
            case SelectElement.earth:
                if (gameObject.tag == "Ground")
                {
                    anim.SetInteger("element", (int)selectedElement);
                    gameObject.tag = "Springboard";
                }
                break;
            case SelectElement.idle:
                break;
            default:
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Tree")
        {
            moving = false;
            selectedElement = SelectElement.idle;
            anim.SetInteger("element", (int)selectedElement);
            Destroy(gameObject);
        }
        if (collision.tag == "Enemy" && gameObject.tag == "SnowBoulder")
        {
           Destroy(collision.gameObject);
        }
        if (collision.tag == "Water" && gameObject.tag == "SnowBoulder")
        {
            Destroy(gameObject);
        }
    }
}
