using UnityEngine;
using System.Collections;

public class ShootTheBall : MonoBehaviour
{
    float resetTime = 0;

    Vector3 originalPosition;
    Quaternion originalRotation;
    bool shot = false;

    // Use this for initialization
    void Start()
    {
        // Grab the original local position of the sphere when the app starts.
        originalPosition = this.transform.localPosition;
        originalRotation = this.transform.localRotation;

    }

    void Update()
    {

        if (shot)
            resetTime++;

        if (resetTime >= 100)
        {
            shot = false;
            //Destroy(this.GetComponent<Rigidbody>());
            //this.transform.position = originalPosition;
            OnReset();
            resetTime = 0;
        }


    }

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
    

        // If the sphere has no Rigidbody component, add one to enable physics.
        if (!this.GetComponent<Rigidbody>())
        {
            originalPosition = this.transform.localPosition;
            var rigidbody = this.gameObject.AddComponent<Rigidbody>();
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rigidbody.AddForce(Camera.main.transform.forward * 500);
            shot = true;
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