using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessWalk : MonoBehaviour
{
    public Transform playerPosition;
    public Transform up;
    public Transform down;
    public Transform left;
    public Transform right;

    public LayerMask detectTerrain;
    public LayerMask detectElement;

    private bool changeDirection;
    private bool wasAirborne;
    private bool grounded;
    private float fallSpeed;
    private bool airFalling;
    private bool climbing;

    private float movementSpeed;

    private Animator animations;

    // Use this for initialization
    void Start ()
    {
        movementSpeed = 5;
	}
    void killPlayer()
    {
        //GoTo deathscreen
    }
	// Update is called once per frame
	void Update ()
    {
        //Gets the Lemming going
        GetComponent<Rigidbody2D>().velocity = new Vector3(movementSpeed, GetComponent<Rigidbody2D>().velocity.y, 1);
        //Turns around at terrain walls
        changeDirection = Physics2D.Linecast(playerPosition.position, new Vector2(playerPosition.position.x+movementSpeed, playerPosition.position.y),detectTerrain);
        if(changeDirection)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1);
            movementSpeed *= -1;
        }
        //Checks if character is on the ground, if not takes the falling speed, if it is too much the princess dies
        grounded = Physics2D.Linecast(down.position, right.position, detectTerrain);
        if(!grounded)
        {
            fallSpeed = GetComponent<Rigidbody2D>().velocity.x;
        }
        if (wasAirborne && grounded && fallSpeed > 3)
        {
            killPlayer();
        }
        //Limits the fall speed if above and ait current
        airFalling = Physics2D.Linecast(down.position, new Vector2(down.position.x, down.position.y - 5),detectElement);
        if(airFalling && !grounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, -1, 1);
        }
        //Looks for a tree that can be climbed, proceeds to go straight up it
        climbing = Physics2D.OverlapCircle(new Vector2(up.position.x,up.position.y), 1, detectElement);
        if(climbing)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 2, 1);
        }

        //Needs to be at/near the end for checking when player hits the floor
        wasAirborne = !grounded;
	}
}
