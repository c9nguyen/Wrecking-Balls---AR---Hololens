using UnityEngine;
using System.Collections;

public class AlexaMove : MonoBehaviour {

    //Declare variables
    Vector3 originalPosition;
    Quaternion originalRotation;

    bool move = false;
    float resetTime = 0;

	// Use this for initialization
	void Start () {
        //Grab the initial postion of the cylider when the app starts.
        originalPosition = this.transform.localPosition;
        originalRotation = this.transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (move)
            resetTime++;

        if (resetTime >= 100)
        {
            move = false;
            //Destroy(this.GetComponent<Rigidbody>());
            //this.transform.position = originalPosition;
            OnReset();
            resetTime = 0;
        }
    }

    //Called by GazeGestureManager when the user performs a select gesture.
    void OnSelect()
    {
        if (!this.GetComponent<Rigidbody>())
        {
            originalPosition = this.transform.localPosition;
            var rigidbody = this.gameObject.AddComponent<Rigidbody>();
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rigidbody.AddForce(Camera.main.transform.forward * 500);
            move = true;
        }
    }

    void OnReset()
    {
        // If the sphere has a Rigidbody component, remove it to disable physics.
        var rigidbody = this.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            DestroyImmediate(rigidbody);
        }

        // Put the sphere back into its original local position.
        this.transform.localPosition = originalPosition;
        this.transform.localRotation = originalRotation;
    }

    // Called by SpeechManager when the user says the "Drop sphere" command
    void OnDrop()
    {
        // Just do the same logic as a Select gesture.
        OnSelect();
    }
}
