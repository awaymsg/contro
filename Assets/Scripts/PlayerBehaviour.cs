using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour {
    public GameObject Gun;
    public GameObject Bullet;
    public GameObject Muzzle;
    public GameObject TheCamera;
    public GameObject HealthBar;
    public GameObject GroundCollide;
    public Rigidbody2D PlayerBody;
    public Sprite RifleSprite;
    public Sprite ShotgunSprite;
    public bool OnLadder;
    public bool grounded;

    GameObject bulletclone;
    Vector2 bulletDirection;
    float speed;
    float maxSpeed;
    float moveDirection;
    float ShootTime;
    float WeaponSwitchTime;
    float HealthPoints;
    float MaxHealth;
    float BulletSpeed;

    string GunName;
    string Weapon1;
    string Weapon2;

    bool facingRight = true;
    bool CanShoot;
    bool OnWeapon1 = true;
    public Vector2 currentSpeed;
    Vector2 Movement;
    Vector2 JumpSpeed;

    private static PlayerBehaviour instance;
    public static PlayerBehaviour Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerBehaviour>();
            }
            return instance;
        }
    }

	// Use this for initialization
	void Start ()
    {
        MaxHealth = 1000f;
        HealthPoints = MaxHealth;
        JumpSpeed = new Vector2(0, 350f * Time.deltaTime);
        maxSpeed = 1f;
        ShootTime = 0.2f;
        CanShoot = true;
        grounded = true;
        OnLadder = false;
        WeaponSwitchTime = 0.1f;
        OnWeapon1 = true;
        Weapon1 = "rifle";
        Weapon2 = "shotgun";
        GunName = Weapon1;
        BulletSpeed = 200;
        Muzzle.GetComponent<SpriteRenderer>().enabled = false;
        HealthBar.GetComponent<Slider>().maxValue = MaxHealth;
        HealthBar.GetComponent<Slider>().value = HealthPoints;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        //if (collision.gameObject.tag == "Floor")
        //{
        //    grounded = true;
        //}
        if (collision.gameObject.tag == "Gun")
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(OnLadder);
        currentSpeed = PlayerBody.velocity;
        WeaponSwitchTime -= Time.deltaTime;
        ShootTime -= Time.deltaTime;
        //Debug.Log(grounded);
        if (ShootTime < 0)
        {
            CanShoot = true;
        }
        speed = 400f * Time.deltaTime;
        moveDirection = Input.GetAxisRaw("Horizontal");
        //if (Input.GetKeyDown("x")) { Debug.Log(grounded); }
        if (OnLadder == false)
        {
            if (moveDirection != 0)
            {
                PlayerMove(moveDirection);
            }
            else if (moveDirection == 0)
            {
                //PlayerBody.velocity = new Vector2(0, PlayerBody.velocity.y);
                PlayerBody.velocity = new Vector2(PlayerBody.velocity.x * 0.8f, PlayerBody.velocity.y);
            }
            if (Input.GetKeyDown("x") && grounded == true)
            {
                PlayerBody.velocity = JumpSpeed + currentSpeed;
                grounded = false;
            }
            if (Input.GetAxisRaw("Vertical") == 1)
            {
                if (facingRight)
                {
                    Gun.transform.eulerAngles = new Vector3(0, 0, 45);
                    bulletDirection = new Vector2(1f, 1f);
                }
                else if (facingRight == false)
                {
                    Gun.transform.eulerAngles = new Vector3(0, 0, -45);
                    bulletDirection = new Vector2(-1f, 1f);
                }
            }
            else if (Input.GetAxisRaw("Vertical") == -1)
            {
                if (facingRight)
                {
                    Gun.transform.eulerAngles = new Vector3(0, 0, -45);
                    bulletDirection = new Vector2(1f, -1f);
                }
                else if (facingRight == false)
                {
                    Gun.transform.eulerAngles = new Vector3(0, 0, 45);
                    bulletDirection = new Vector2(-1f, -1f);
                }
            }
            else if (Input.GetAxisRaw("Vertical") == 0)
            {
                Gun.transform.eulerAngles = Vector2.zero;
                if (facingRight)
                {
                    bulletDirection = new Vector2(1, 0);
                }
                else if (facingRight == false)
                {
                    bulletDirection = new Vector2(-1, 0);
                }
            }
            if (Input.GetAxisRaw("Fire1") == 1 && CanShoot == true)
            {
                ShootGun();
            }
        }
        if (OnLadder)
        {
            CanShoot = false;
            if (Input.GetKeyDown("x"))
            {
                OnLadder = false;
                //grounded = false;
                PlayerBody.gravityScale = 2;
            }
            if (Input.GetAxisRaw("Vertical") == 1)
            {
                PlayerBody.velocity = new Vector3(0, 80 * Time.deltaTime, 0);
            }
            if (Input.GetAxisRaw("Vertical") == -1)
            {
                PlayerBody.velocity = new Vector3(0, -80 * Time.deltaTime, 0);
            }
        }
        PlayerBody.velocity = PlayerBody.velocity * 0.95f;
        if (WeaponSwitchTime < 0)
        {
            if (Input.GetAxis("Switch") == 1)
            {
                SwitchWeapon();
            }
        }
    }

    void PlayerMove(float xinput)
    {
        Movement = new Vector2(speed, 0);
        if (xinput == 1)
        {
            //if (PlayerBody.velocity.x < maxSpeed && PlayerBody.velocity.x > -maxSpeed)
            //{
            //if (PlayerBody.velocity.x > 3.5)
            //{
            //    CameraFollow.Instance.offsetPosition = new Vector3(3f, 0, -10f);
            //    CameraFollow.Instance.CameraLock = false;
            //}
            facingRight = true;
            PlayerBody.AddForce(Movement);
            GetComponent<SpriteRenderer>().flipX = false;
            if (Gun.GetComponent<SpriteRenderer>().flipX == true)
            {
                Gun.GetComponent<SpriteRenderer>().flipX = false;
                Gun.transform.localPosition = new Vector2(Gun.transform.localPosition.x + 0.2f, Gun.transform.localPosition.y);
                Muzzle.transform.localPosition = new Vector2(Muzzle.transform.localPosition.x + 0.61f, Muzzle.transform.localPosition.y);
            }
            //}
        }
        if (xinput == -1)
        {
            //if (PlayerBody.velocity.x < maxSpeed && PlayerBody.velocity.x > -maxSpeed)
            //{
            //if (PlayerBody.velocity.x < -3.5)
            //{
            //    CameraFollow.Instance.offsetPosition = new Vector3(-3f, 0, -10f);
            //    CameraFollow.Instance.CameraLock = false;
            //}
            facingRight = false;
            PlayerBody.AddForce(-Movement);
            GetComponent<SpriteRenderer>().flipX = true;
            if (Gun.GetComponent<SpriteRenderer>().flipX == false)
            {
                Gun.GetComponent<SpriteRenderer>().flipX = true;
                Gun.transform.localPosition = new Vector2(Gun.transform.localPosition.x - 0.2f, Gun.transform.localPosition.y);
                Muzzle.transform.localPosition = new Vector2(Muzzle.transform.localPosition.x - 0.61f, Muzzle.transform.localPosition.y);
            }
            //}
        }
    }

    private void FixedUpdate()
    {
        Muzzle.GetComponent<SpriteRenderer>().enabled = false;
    }

    void SwitchWeapon()
    {
        if (OnWeapon1)
        {
            GunName = Weapon2;
            OnWeapon1 = false;
        }
        else if (OnWeapon1 == false)
        {
            GunName = Weapon1;
            OnWeapon1 = true;
        }
        if (GunName == "rifle")
        {
            Gun.GetComponent<SpriteRenderer>().sprite = RifleSprite;
            ShootTime = 0.2f;
            CanShoot = false;
        }
        if (GunName == "shotgun")
        {
            Gun.GetComponent<SpriteRenderer>().sprite = ShotgunSprite;
            ShootTime = 1f;
            CanShoot = false;
        }
        WeaponSwitchTime = 0.5f;
        Debug.Log(GunName);
    }

    void ShootGun()
    {
        float randomness;
        float randomplus;
        float randomspeed;
        Vector2 offsetbulletdirection = Vector2.zero;
        if (GunName == "rifle")
        {
            bulletclone = Instantiate(Bullet, Muzzle.transform.position, Quaternion.identity);
            bulletclone.GetComponent<bulletBehaviour>().MakeBullet(bulletDirection, 400f, 100f);
            ShootTime = 0.2f;
        }
        if (GunName == "shotgun")
        {
            for (int i = 0; i < 24; i++)
            {
                bulletclone = Instantiate(Bullet, Muzzle.transform.position, Quaternion.identity);
                randomness = Random.value / 10;
                randomplus = Random.value;
                randomspeed = Random.value;
                ShootTime = 1f;
                if (randomplus <= 0.25)
                {
                    offsetbulletdirection = new Vector2(bulletDirection.x + randomness, bulletDirection.y + randomness);
                }
                if (randomplus > 0.25 && randomplus <= 0.5)
                {
                    offsetbulletdirection = new Vector2(bulletDirection.x - randomness, bulletDirection.y + randomness);
                }
                if (randomplus > 0.5 && randomplus <= 0.75)
                {
                    offsetbulletdirection = new Vector2(bulletDirection.x + randomness, bulletDirection.y - randomness);
                }
                if (randomplus > 0.75 && randomplus <= 1)
                {
                    offsetbulletdirection = new Vector2(bulletDirection.x - randomness, bulletDirection.y - randomness);
                }
                if (randomspeed <= 0.5)
                {
                    bulletclone.GetComponent<bulletBehaviour>().MakeBullet(offsetbulletdirection, BulletSpeed + Random.value * 20, 20f);
                }
                else if (randomspeed <= 1)
                {
                    bulletclone.GetComponent<bulletBehaviour>().MakeBullet(offsetbulletdirection, BulletSpeed - Random.value * 20, 20f);
                }
            }
        }
        CanShoot = false;
        Muzzle.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void ChangeHealth(float amount)
    {
        HealthPoints += amount;
        HealthBar.GetComponent<Slider>().value = HealthPoints;
        //HealthBar.transform.localPosition = new Vector3(HealthBar.transform.localPosition.x - healthratio * (1/5), HealthBar.transform.localPosition.y, HealthBar.transform.localPosition.z);
    }

    public IEnumerator ChangePositionX(float positionx)
    {
        if (transform.position.x < positionx)
        {
            while (transform.position.x < positionx)
            {
                transform.position = new Vector2(transform.position.x + 0.02f, transform.position.y);
                yield return new WaitForSeconds(.005f);
            }
            transform.position = new Vector2(positionx, transform.position.y);
        } else if (transform.position.x > positionx)
        {
            while (transform.position.x > positionx)
            {
                transform.position = new Vector2(transform.position.x - 0.02f, transform.position.y);
                yield return new WaitForSeconds(.005f);
            }
            transform.position = new Vector2(positionx, transform.position.y);
        }
    }

    public IEnumerator ChangePositionY(float positiony)
    {
        if (transform.position.y < positiony)
        {
            while (transform.position.y < positiony)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.02f);
                yield return new WaitForSeconds(.005f);
            }
            transform.position = new Vector2(transform.position.x, positiony);
        }
        else if (transform.position.y > positiony)
        {
            while (transform.position.y > positiony)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 0.02f);
                yield return new WaitForSeconds(.005f);
            }
            transform.position = new Vector2(transform.position.x, positiony);
        }
    }
}
