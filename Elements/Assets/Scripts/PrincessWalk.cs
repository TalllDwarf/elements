using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessWalk : MonoBehaviour
{
    public Transform playerPosition;

    private RaycastHit2D changeDirection;
    private bool wasAirborne;
    private RaycastHit2D grounded;
    private bool onGround;
    public float fallSpeed;
    private RaycastHit2D airFalling;
    private bool dead;
    private float movementSpeed;
    public bool falling;
    public bool jumping;

    private Animator animations;

    // Use this for initialization
    void Start ()
    {
        dead = false;
        movementSpeed = 1;
        animations = GetComponent<Animator>();
	}
    public void killPlayer()
    {
        dead = true;
    }
	// Update is called once per frame
	void Update ()
    {

        fallSpeed = GetComponent<Rigidbody2D>().velocity.y;
        if (!dead)
        {
            //Gets the Lemming going
            GetComponent<Rigidbody2D>().velocity = new Vector3(movementSpeed, GetComponent<Rigidbody2D>().velocity.y, 1);
            //Turns around at terrain walls
            changeDirection = Physics2D.Raycast(transform.position + transform.right*0.5f, transform.right,0.5f);
            //Debug.DrawRay(transform.position + transform.right * 1, transform.right* 0.5f,Color.red,4f);
            if (changeDirection)
            {
                switch (changeDirection.collider.tag)
                {
                    case "Ground":
                        transform.rotation =Quaternion.Euler(0f, transform.rotation.eulerAngles.y+180, 0f);
                        movementSpeed *= -1;
                        break;
                    case "Tree":
                        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 180, 0f);
                        movementSpeed *= -1;
                        break;
                    case "Snowboulder":
                        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 180, 0f);
                        movementSpeed *= -1;
                        break;
                    default:
                        break;
                }
            }
            //Checks if character is on the ground, if not takes the falling speed, if it is too much the princess dies
            grounded = Physics2D.Raycast(transform.position + -transform.up * 0.2f, -transform.up, 0.1f);
            if (grounded)
            {
                if (grounded.collider.tag == "Springboard")
                {
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 2));
                }
                else if (grounded.collider.tag == "Ground")
                {
                    onGround = true;
                    
                }
                else
                {
                    onGround = false;
                }
            }
            else
            {
                onGround = false;
            }
            if (onGround && fallSpeed < -4f)
            {
                killPlayer();
            }
            //Limits the fall speed if above and ait current
            airFalling = Physics2D.Raycast(transform.position, new Vector2(transform.position.x, transform.position.y - 3));
            if (airFalling)
            {
                if (airFalling.collider.tag == "Wind" && !onGround)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector3(0, -1, 1);
                }
            }
            //Looks for a tree that can be climbed, proceeds to go straight up it
            /**climbing = Physics2D.OverlapCircle(new Vector2(up.position.x, up.position.y), 1);
            if (climbing)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, 2, 1);
            }**/
            
            //Needs to be at/near the end for checking when player hits the floor
            wasAirborne = !onGround;
        }
        if(fallSpeed < 0)
        {
            falling = true;
            jumping = false;
        }
        else if(fallSpeed > 0)
        {
            falling = false;
            jumping = true;
        }
        else
        {
            falling = false;
            jumping = false;
        }
        animations.SetBool("dead", dead);
        animations.SetBool("falling",falling );
        animations.SetBool("jumping", jumping);
	}
}
