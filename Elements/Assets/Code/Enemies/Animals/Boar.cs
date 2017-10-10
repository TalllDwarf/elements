using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Animal {

	// Use this for initialization
	protected override void Start () {
        base.Start();
	}

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Renderer>().isVisible && currentAnimalState != animalStates.Dead)
        {
            //Attack player if we can
            if (CanAttackPlayer() && currentAnimalState != animalStates.Attacking)
            {
                currentAnimalState = animalStates.Attacking;
                Move(GetPlayer().transform.position);
            }
            else if (CanSeePlayer())
            {
                currentAnimalState = animalStates.Moveing;
                Move(GetPlayer().transform.position);
            }

            AnimationUpdate();
        }
    }

    /// <summary>
    /// Boar will charge towards the enemy increasing 
    /// </summary>
    /// <param name="playerTransform"></param>
    protected override void Attack(Transform playerTransform)
    {
        throw new NotImplementedException();
    }
}
