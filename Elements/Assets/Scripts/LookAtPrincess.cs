using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPrincess : MonoBehaviour {
    GameObject princess;
	// Use this for initialization
	void Start ()
    {
        princess = GameObject.FindGameObjectWithTag("Princess");
    }
	// Update is called once per frame
	void Update ()
    {
        Transform princessLocation = princess.GetComponent<Transform>().transform;
        transform.LookAt(princessLocation);
    }
}
