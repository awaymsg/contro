using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehaviour : MonoBehaviour {
    Vector2 Direction;
    Vector2 Speed;
    float Damage;
    float bulletLiveTime;
    [SerializeField] Rigidbody2D BulletBody;
    // Use this for initialization

    private static bulletBehaviour instance;
    public static bulletBehaviour Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<bulletBehaviour>();
            }
            return instance;
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<GroundEnemyBehaviour>().ChangeHealth(-Damage);
        }
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerBehaviour>().ChangeHealth(-Damage);
        }
        Destroy(gameObject);
    }

    void Awake () {
        Direction = Vector2.zero;
        bulletLiveTime = 5f;
	}
	
	// Update is called once per frame
	void Update () {
        BulletBody.velocity = (Speed * Time.deltaTime);
        bulletLiveTime -= Time.deltaTime;
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
        if (bulletLiveTime < 0)
        {
            Destroy(gameObject);
        }
    }

    public void MakeBullet(Vector2 direction, float speedmod, float damage)
    {
        Direction = direction;
        Damage = damage;
        Speed = direction * speedmod;
        //Debug.Log(Direction);
    }
}
