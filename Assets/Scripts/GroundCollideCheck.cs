using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollideCheck : MonoBehaviour {

    float ladderTime = 0.2f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            if (!PlayerBehaviour.Instance.OnLadder && PlayerBehaviour.Instance.GetComponent<Rigidbody2D>().velocity.y <= 2)
            {
                PlayerBehaviour.Instance.grounded = true;
            } else if (PlayerBehaviour.Instance.OnLadder && !collision.gameObject.GetComponent<FloorScript>().LadderTile && Input.GetAxisRaw("Vertical") == -1)
            {
                PlayerBehaviour.Instance.OnLadder = false;
                PlayerBehaviour.Instance.PlayerBody.gravityScale = 2;
                PlayerBehaviour.Instance.grounded = true;
            }
        } 
        if (collision.gameObject.tag == "Ladder" && Input.GetAxisRaw("Vertical") != 0 && ladderTime < 0) 
        {
            //if (Vector2.Distance(collision.transform.position, transform.position) < 1f)
            //{
            if (!PlayerBehaviour.Instance.OnLadder)
            {
                StartCoroutine(PlayerBehaviour.Instance.ChangePositionX(collision.transform.position.x));
            }
                PlayerBehaviour.Instance.OnLadder = true;
                PlayerBehaviour.Instance.grounded = false;
                //PlayerBehaviour.Instance.transform.position = new Vector2(collision.transform.position.x + 0.05f, PlayerBehaviour.Instance.transform.position.y);
                PlayerBehaviour.Instance.PlayerBody.gravityScale = 0;
                if (Input.GetAxisRaw("Vertical") == -1)
                {
                    //collision.gameObject.GetComponent<PlatformEffector2D>().surfaceArc = 0;
                }
            //}
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            PlayerBehaviour.Instance.PlayerBody.gravityScale = 2;
            PlayerBehaviour.Instance.OnLadder = false;
            ladderTime = 0.3f;
            //gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            if (collision.GetComponent<PlatformEffector2D>().surfaceArc == 0)
            {
                collision.GetComponent<PlatformEffector2D>().surfaceArc = 180;
            }
        }
        //if (collision.gameObject.tag == "Floor")
        //{
        //    ladderTime = 0.7f;
        //    PlayerBehaviour.Instance.grounded = false;
        //}
    }

    // Use this for initialization
    void Start()
    {

    }

    private void Update()
    {
        ladderTime -= Time.deltaTime;
    }
}
