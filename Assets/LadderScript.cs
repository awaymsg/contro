using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour {
    public bool TopLadder;
    float ladderTime;

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && TopLadder)
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
            ladderTime = 0.2f;
        }
    }

    // Use this for initialization
    void Start () {
        ladderTime = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {
        ladderTime -= Time.deltaTime;
        if (ladderTime <= 0 && !GetComponent<BoxCollider2D>().isTrigger)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
	}
}
