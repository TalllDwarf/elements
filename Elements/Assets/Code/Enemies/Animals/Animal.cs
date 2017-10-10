using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : MonoBehaviour
{

    /// <summary>
    /// Availables states for the animal
    /// Idle - Animal is stood still
    /// Moveing - Moving towards the player
    /// Looking - Randomly walking around
    /// Attacking - Attacking the player
    /// Dying - Animal is Dying
    /// Dead - Animal is dead and no longer moves
    /// </summary>
    public enum animalStates
    {
        Idle,
        Moveing,
        Looking,
        Turning,
        Attacking,
        Dying,
        Dead
    };


    public float walkSpeed = 2.0f;
    public float runSpeed = 4.0f;
    public float viewDistance = 0.5f;
    public float attackDistance = 0.25f;
    public bool playerVisible = false;
    public bool spriteFaceLeft = true;

    protected float timeLeft = 0.0f;

    //Used for the animal animation
    protected Animator animalAnimator;

    public animalStates currentAnimalState = animalStates.Idle;

    private EdgeDetector edgeDetection;

    protected virtual void Start()
    {
        Start(true, true);
    }


    protected virtual void Start(bool getEdge, bool startTimer)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if(getEdge)
            edgeDetection = GetComponentInChildren<EdgeDetector>();

        if (!spriteFaceLeft)
        {
            //Default sprite facing left
            spriteRenderer.flipX = true;
        }

        animalAnimator = GetComponent<Animator>();

        if(startTimer)
            timeLeft = UnityEngine.Random.Range(1.0f, 2.5f);
    }

    /// <summary>
    /// Kills the animal
    /// Used by animation
    /// </summary>
    public void kill()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Move to a new spot on the map
    /// </summary>
    /// <param name="toPosition">The new position on the map we want to move to</param>
    protected void Move(Vector3 toPosition, bool run = false)
    {
        Vector3 newPosition = toPosition - transform.position;
        newPosition.z = transform.position.z;
        newPosition.Normalize();

        float speed = (run) ? runSpeed : walkSpeed;

        transform.position += (newPosition * speed) * Time.deltaTime;

        if (NearEdge())
        {
            currentAnimalState = animalStates.Turning;
            timeLeft = UnityEngine.Random.Range(1f, 3f);
        }
    }

    /// <summary>
    /// Turn the animal 180 on the Y axis making the animal look in the other direction
    /// </summary>
    protected void Turn()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180, 0);
    }

    /// <summary>
    /// Attacks the player if they get to close
    /// </summary>
    /// <param name="playerTransform"></param>

    protected abstract void Attack(Transform playerTransform);

    /// <summary>
    /// Updates animation to the current animal state
    /// </summary>
    protected void AnimationUpdate()
    {
        animalAnimator.SetInteger(Tags.animAnimalStateID, (int)currentAnimalState);
    }

    /// <summary>
    /// Returns true if the animal can see the player
    /// </summary>
    /// <returns>Can animal see player</returns>
    protected virtual bool CanSeePlayer()
    {
        return HasHit(Tags.Player, viewDistance);
    }

    /// <summary>
    /// Returns true if the player is in attacking range
    /// </summary>
    /// <returns></returns>
    protected bool CanAttackPlayer()
    {
        return HasHit(Tags.Player, attackDistance);
    }

    protected bool HitTree()
    {
        return HasHit("Tree", 0.1f);
    }

    /// <summary>
    /// Raycast forward from the animal checking what it can see
    /// returns true if the object hit has the required tag
    /// </summary>
    /// <param name="tag">Tag we are looing for</param>
    /// <param name="distance">Distance of the raycast</param>
    /// <returns></returns>
    private bool HasHit(string tag, float distance)
    {
        Vector3 raycastVector = transform.position + (-transform.right * 0.5f);
        raycastVector.y -= 0.1f;

        RaycastHit2D animalVision = Physics2D.Raycast(raycastVector, -transform.right, distance);
        Debug.DrawRay(raycastVector, -transform.right * distance, Color.red);

        if (animalVision)
        {
            
            return (animalVision.collider.tag == Tags.Player);
        }
        return false;
    }

    //Checks for a drop will turn around if edge found
    protected bool NearEdge()
    {
        return edgeDetection.EdgeDetected || HitTree();
    }

    /// <summary>
    /// When the animal has been attacked set them to dying state
    /// </summary>
    protected void Damage()
    {
        currentAnimalState = animalStates.Dying;
        AnimationUpdate();
    }

    /// <summary>
    /// After the dying state we set the animal as dead
    /// </summary>
    public void Died()
    {
        currentAnimalState = animalStates.Dead;
        AnimationUpdate();
    }

    //Returns the player object
    protected GameObject GetPlayer()
    {
        foreach(GameObject player in GameObject.FindGameObjectsWithTag(Tags.Player))
        {
            if (player.GetComponent<PrincessWalk>() != null)
                return player;
        }
        return GameObject.FindGameObjectWithTag(Tags.Player);
    }

    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == Tags.Player)
        {
            collider.GetComponent<PrincessWalk>().killPlayer();
            Died();
        }
        if (collider.tag == "Springboard")
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100, 300));
        }
    }
}
