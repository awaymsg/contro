using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    public bool LadderTile;
    public bool JumpDownable;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (LadderTile && PlayerBehaviour.Instance.OnLadder)
        {
            GetComponent<PlatformEffector2D>().surfaceArc = 0;
        }
        if (LadderTile && !PlayerBehaviour.Instance.OnLadder && GetComponent<PlatformEffector2D>().surfaceArc == 0)
        {
            GetComponent<PlatformEffector2D>().surfaceArc = 90;
        }
    }
}