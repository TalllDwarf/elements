using System;
using UnityEngine;

public class Tiger : Animal
{
    protected override void Attack(Transform playerTransform)
    {
        throw new NotImplementedException();
    }

    // Use this for initialization
    protected override void Start()
    {
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
                Attack(GameObject.FindGameObjectWithTag(Tags.Player).transform);
                currentAnimalState = animalStates.Attacking;
            }
            else if (CanSeePlayer())
            {
                currentAnimalState = animalStates.Moveing;
                Move(GetPlayer().transform.position);
            }
            else
            {               
                //Animal brain switch statement
                switch (currentAnimalState)
                {
                    case animalStates.Idle:

                        if (timeLeft <= 0)
                        {
                            currentAnimalState = animalStates.Looking;
                            timeLeft = UnityEngine.Random.Range(1f, 3f);
                        }

                        break;

                    case animalStates.Looking:

                        if (timeLeft > 0)
                        {
                            Move(transform.position - transform.right);
                        }
                        else
                        {
                            currentAnimalState = animalStates.Idle;
                            timeLeft = UnityEngine.Random.Range(1f, 3f);
                        }

                        break;

                    case animalStates.Turning:

                        if(timeLeft <= 0)
                        {
                            Turn();

                            currentAnimalState = animalStates.Idle;
                            timeLeft = UnityEngine.Random.Range(.1f, .3f);
                        }

                        break;
                }
                timeLeft -= Time.deltaTime;
            }

            AnimationUpdate();
        }
    }

}
