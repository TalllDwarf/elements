using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHole : MonoBehaviour {

    public GameObject jumpBear;

    private float blinkTimer;
    private Animator anim;
    private bool alive = true;

	// Use this for initialization
	void Start () {
        blinkTimer = Random.Range(10, 100);
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (alive)
        {
            blinkTimer -= Time.deltaTime;

            if (blinkTimer <= 0)
            {
                anim.SetBool("Blink", true);
            }
        }
	}

    void BlinkReset()
    {
        anim.SetBool("Blink", false);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (alive)
        {
            Instantiate(jumpBear, transform.position, Quaternion.identity);
            alive = false;
            anim.SetBool("Blank", true);
        }
    }
}
