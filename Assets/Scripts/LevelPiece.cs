using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPiece : MonoBehaviour {
    public bool BottomConnect;
    public bool TopConnect;
    public bool LeftConnect;
    public bool RightConnect;
    public bool IsLevelBeginning;
    public bool IsLevelEnd;
    public bool IsDeadEnd;
    public Vector2 FloorPosition;
    public Vector2 LadderPosition;
    public Vector2 GridPosition;
    public GameObject LeftEdgeFloorPiece;
    public GameObject RightEdgeFloorPiece;
    public GameObject TopEdgeFloorPiece;
    public GameObject BottomEdgeFloorPiece;
    public GameObject[] TopEdgeLadderPiece;
    public GameObject[] BottomEdgeLadderPiece;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
