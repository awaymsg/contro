using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyBehaviour : MonoBehaviour {
    public GameObject HealthShell;
    public GameObject HealthBar;
    public GameObject Bullet;
    public GameObject Gun;
    public GameObject Muzzle;
    public GameObject ThePlayer;
    public string EnemyType;
    public string GunType;
    public float ShootTime;
    public float bulletSpeed;

    bool HealthShow;
    bool CanShoot;
    float HealthTimer;
    float ShootTimer;
    GameObject bulletclone;
    Vector2 bulletDirection;
    [SerializeField] float MaxHealth;
    [SerializeField] Rigidbody2D EnemyBody;
    float HealthPoints;
    

    private static GroundEnemyBehaviour instance;
    public static GroundEnemyBehaviour Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GroundEnemyBehaviour>();
            }
            return instance;
        }
    }
	// Use this for initialization
	void Start () {
        ShootTimer = ShootTime;
        HealthPoints = MaxHealth;
        HealthTimer = 0f;
        HealthShow = false;
        EnemyBody = gameObject.GetComponent<Rigidbody2D>();
        Muzzle.GetComponent<SpriteRenderer>().enabled = false;
	}

    // Update is called once per frame
    void Update() {
        HealthTimer -= Time.deltaTime;
        if (HealthShow)
        {
            HealthBar.GetComponent<SpriteRenderer>().enabled = true;
            HealthShell.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (HealthTimer < 0)
        {
            HealthBar.GetComponent<SpriteRenderer>().enabled = false;
            HealthShell.GetComponent<SpriteRenderer>().enabled = false;
            HealthShow = false;
        }
        if (HealthPoints <= 0)
        {
            Destroy(gameObject);
        }
        if (EnemyType == "Shooter")
        {
            ShooterBehaviour();
        }
        EnemyBody.velocity = new Vector2(EnemyBody.velocity.x * 0.7f, EnemyBody.velocity.y);
        if (transform.position.x - ThePlayer.transform.position.x > 0)
        {
            if (gameObject.GetComponent<SpriteRenderer>().flipX)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                Gun.GetComponent<SpriteRenderer>().flipX = true;
                Gun.transform.localPosition = new Vector2(Gun.transform.localPosition.x - 0.2f, Gun.transform.localPosition.y);
                Muzzle.transform.localPosition = new Vector2(Muzzle.transform.localPosition.x - 0.61f, Muzzle.transform.localPosition.y);
            }

        }
        if (transform.position.x - ThePlayer.transform.position.x < 0)
        {
            if (gameObject.GetComponent<SpriteRenderer>().flipX == false)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                Gun.GetComponent<SpriteRenderer>().flipX = false;
                Gun.transform.localPosition = new Vector2(Gun.transform.localPosition.x + 0.2f, Gun.transform.localPosition.y);
                Muzzle.transform.localPosition = new Vector2(Muzzle.transform.localPosition.x + 0.61f, Muzzle.transform.localPosition.y);
            }
        }
    }

    private void FixedUpdate()
    {
        Muzzle.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void ChangeHealth(float amount)
    {
        HealthPoints += amount;
        HealthShow = true;
        HealthTimer = 5f;
        float healthratio = HealthPoints / MaxHealth;
        HealthBar.transform.localScale = new Vector3(HealthBar.transform.localScale.x * healthratio, HealthBar.transform.localScale.y, HealthBar.transform.localScale.z);
        //HealthBar.transform.localPosition = new Vector3(HealthBar.transform.localPosition.x - healthratio * (1/5), HealthBar.transform.localPosition.y, HealthBar.transform.localPosition.z);
    }

    void ShooterBehaviour()
    {
        ShootTimer -= Time.deltaTime;
        if (ShootTimer < 0)
        {
            CanShoot = true;
        }
        if (CanShoot && gameObject.GetComponent<SpriteRenderer>().flipX == false)
        {
            bulletDirection = new Vector2(-1, 0);
            bulletclone = Instantiate(Bullet, Muzzle.transform.position, Quaternion.identity);
            bulletclone.GetComponent<bulletBehaviour>().MakeBullet(bulletDirection, bulletSpeed, 100f);
            CanShoot = false;
            ShootTimer = ShootTime;
            Muzzle.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (CanShoot && gameObject.GetComponent<SpriteRenderer>().flipX == true)
        {
            bulletDirection = new Vector2(1, 0);
            bulletclone = Instantiate(Bullet, Muzzle.transform.position, Quaternion.identity);
            bulletclone.GetComponent<bulletBehaviour>().MakeBullet(bulletDirection, bulletSpeed, 100f);
            CanShoot = false;
            ShootTimer = ShootTime;
            Muzzle.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
