using UnityEngine;
using UnityEngine.VR.WSA.Input;
using System.Collections;

public class TapDragToPlaceAlexa : MonoBehaviour {

    public GameObject shootingStand;
    bool placing = true;
    GestureRecognizer recognizer;

	// Use this for initialization
	void Start () {
        SpatialMapping.Instance.DrawVisualMeshes = true;
        recognizer.SetRecognizableGestures(GestureSettings.Hold);
        recognizer.HoldStartedEvent += Recognizer_HoldStartedEvent;
        recognizer.HoldCompletedEvent += Recognizer_HoldCompletedEvent;
        recognizer.HoldCanceledEvent += Recognizer_HoldCompletedEvent;

    }

    private void Recognizer_HoldCompletedEvent(InteractionSourceKind source, Ray headRay)
    {
        Debug.LogWarning("HoldCompletedEvent");
        //throw new System.NotImplementedException();
    }

    private void Recognizer_HoldStartedEvent(InteractionSourceKind source, Ray headRay)
    {
        Debug.LogWarning("Hold Start");
        throw new System.NotImplementedException();
    }

    //Called by GazeGestureManager when the user performs a Select gesture
    //but how about drag?
    void OnSelect()
    {
        //On each Select gesture, toggle whether the use is in placing mode.
        
    }

	
	// Update is called once per frame
	void Update () {
	    //If the user is in placing mode, update the placement to match the user's gaze
        if (placing)
        {
            //Do a raycasr into the world that will only hit the Spatial Mapping Mesh
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo, 30.0f, SpatialMapping.PhysicsRaycastMask))
            {
                //Move this object's parent object to where the raycast hit the Spatial Mapping mesh.
                this.transform.parent.position = hitInfo.point;

                //Rotate this object's parent object to face the user
                Quaternion toQuat = Camera.main.transform.localRotation;
                toQuat.x = 0;
                toQuat.y = 0;
                this.transform.parent.rotation = toQuat;
            }
        }
	}
}
