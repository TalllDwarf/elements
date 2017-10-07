using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    
    string currentElement;
    GameObject princess;
    Animator anim;
    public bool clicked;
    public bool fire;
    public bool earth;
    public bool wind;
    public bool water;
    bool moving = false;
    public float animationLength;
    public GameObject replacement;

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
        currentElement = princess.GetComponent<FollowCursor>().element;
        anim.SetBool("Clicked", clicked);
        anim.SetBool("Wind", wind);
        anim.SetBool("Water", water);
        anim.SetBool("Fire", fire);
        anim.SetBool("Earth", earth);

        
        if(moving && gameObject.tag == "SnowBoulder")
        {
            gameObject.GetComponent<Transform>().position = new Vector2(gameObject.GetComponent<Transform>().position.x + 0.1f, gameObject.GetComponent<Transform>().position.y);
        }

    }
    void OnMouseDown()
    {
        //Destroy(gameObject);
        // Debug.Log(mousePos);
        clicked = true;
        switch(currentElement)
        {
            case "fire":
                fire = true;
                animationLength = anim.GetCurrentAnimatorStateInfo(0).length;
                if (gameObject.tag == "Tree")
                {
                    StartCoroutine(Wait());
                }
                fire = true;
                break;
            case "water":
                water = true;
                animationLength = anim.GetCurrentAnimatorStateInfo(0).length;
                if (gameObject.tag == "Stump")
                {
                    StartCoroutine(Wait());
                }
                break;
            case "wind":
                wind = true;
                moving = true;
                break;
            case "earth":
                earth = true;
                break;
            default:
                break;
        }
    }
    public IEnumerator Wait()
    {
        
        yield return new WaitForSecondsRealtime(animationLength);
        Instantiate(replacement, gameObject.GetComponent<Transform>().position, Quaternion.identity);
        replacement.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        Destroy(gameObject);

    }
}
