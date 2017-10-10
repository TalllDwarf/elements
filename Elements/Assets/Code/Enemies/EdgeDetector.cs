using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeDetector : MonoBehaviour {

    private bool isEdgeInfront;
    private int inCollider;

    public bool EdgeDetected { get { return isEdgeInfront; } }
	
	void OnTriggerEnter2D(Collider2D collider)
    {
        inCollider++;

        isEdgeInfront = false;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        inCollider--;

        if(inCollider <= 0)
        {
            inCollider = 0;
            isEdgeInfront = true;
        }
    }
}
