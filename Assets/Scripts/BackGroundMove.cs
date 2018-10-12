using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour {
    public float moveOffsetSpeed;
    public GameObject TheCamera;

    private static BackGroundMove instance;
    public static BackGroundMove Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<BackGroundMove>();
            }
            return instance;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (TheCamera.transform.hasChanged)
        {
            MoveOffset();
        }
	}

    public void MoveOffset()
    {
        transform.localPosition = new Vector3(TheCamera.transform.position.x * moveOffsetSpeed, TheCamera.transform.position.y * moveOffsetSpeed / 2, transform.localPosition.z);
    }
}
