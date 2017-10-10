using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeJumper : Animal {

    public float dropSpeed = 2.0f;

    private float dropZ;
    private bool hitFloor = false;
    

    protected override void Attack(Transform playerTransform)
    {
        throw new NotImplementedException();
    }

    // Use this for initialization
    protected override void Start () {
        base.Start(false, false);
	}

    // Update is called once per frame
    void Update()
    {
        if (hitFloor == false)
        {
            Vector3 dropVector = GetPlayer().transform.position - transform.position;

            dropVector.Normalize();
            dropVector *= dropSpeed;

            transform.position += (dropVector * Time.deltaTime);

            if (transform.position.y <= dropZ || Vector3.Distance(transform.position, GetPlayer().transform.position) <= .1)
            {
                hitFloor = true;
                animalAnimator.SetBool(Tags.animHitFloorID, true);
            }
        }
       
    }

    
    public void Kill()
    {
        Destroy(gameObject);
    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == Tags.Player)
        {
            collider.GetComponent<PrincessWalk>().killPlayer();
            dropZ = collider.transform.position.y;
        }
    }
}
