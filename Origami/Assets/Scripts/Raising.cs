﻿using UnityEngine;
using System.Collections;

public class Raising : MonoBehaviour {

    public Transform location;
    public Transform plane;
    public Transform building;
    public GameObject locationPlane;
    public GameObject smoke1;
    public GameObject smoke2;

    bool raising;

	// Update is called once per frame
	void Update () {
        if (raising)
        {
            print("location: " + location.position.y);
            print(plane.position.y);
            if (location.position.y > plane.position.y)
            {
                building.transform.Translate(Vector3.up * 0.005f);
            }
            else
            {
                raising = false;
                smoke1.SetActive(false);
                smoke2.SetActive(false);

                GameObject[] buildings = GameObject.FindGameObjectsWithTag("Knockable");

                foreach (GameObject building in buildings)
                {
                    building.GetComponent<Collider>().enabled = true;
                    building.GetComponent<Rigidbody>().useGravity = true;
                }
            }
        }
	}

    public void Raise()
    {
        locationPlane.SetActive(false);

        raising = true;
        smoke1.SetActive(true);
        smoke2.SetActive(true);
    }
}
