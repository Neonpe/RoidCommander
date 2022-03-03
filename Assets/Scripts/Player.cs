using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tf;
    public GameObject GameManager;

    // Controls
    [SerializeField] private string up = "w";
    [SerializeField] private string down = "s";
    [SerializeField] private string left = "a";
    [SerializeField] private string right = "d";
    [SerializeField] private string special1 = "e";
    [SerializeField] private string special2 = "q";
    [SerializeField] private string fire = "space";

    // Movement
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float rotateAccelTime = 0f;
    [SerializeField] private float rotateAccelOffset = 0.4f;
    
    // Shooting
    public GameObject bullet;
    [SerializeField] private float shotCooldown = 1f;
    private float currentTime = 0f;
    [SerializeField] private int spreadShotLevel = 1;
    [SerializeField] private int straightShotLevel = 1;

    // Health
    public GameObject healthBar;
    public float health = 100f;
    private float damageCooldown = 0.5f;
    private float deadTimer = 2f;


    // Checks
    private bool activatethrust = false;
    private bool activateBoost = false;
    private bool rotateLeft = false;
    private bool rotateRight = false;
    private string lastPressed;
    private bool shoot = false;
    private bool canShoot = true;
    private bool canTakeDamage = true;
    private bool canMove = true;
    private bool isDead = false;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        health = 100f;
        canMove = true;

        GameObject initHealthBar = Instantiate(healthBar, new Vector3(tf.position.x, (tf.position.y - 0.5f), tf.position.z), Quaternion.identity);
        PlayerHealthBar initPlayerHealthBar = initHealthBar.GetComponent<PlayerHealthBar>();
        if(initPlayerHealthBar != null)
        {
            initPlayerHealthBar.parentPlayer = gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove == true)
        {
            if(Input.GetKey(up))
            {
                activatethrust = true;
                //Debug.Log("w Pressed");
            }
            if(!Input.GetKey(up))
            {
                activatethrust = false;
                //Debug.Log("w Released");
            }

            if(Input.GetKey(down))
            {
                activateBoost = true;
            }
            if(!Input.GetKey(down))
            {
                activateBoost = false;
            }

            if(Input.GetKey(left))
            {
                rotateLeft = true;
            }
            if(Input.GetKeyDown(left))
            {
                lastPressed = "left";
                rotateAccelTime = 0;
            }
            if(!Input.GetKey(left))
            {
                rotateLeft = false;
            }

            if(Input.GetKey(right))
            {
                rotateRight = true;
            }
            if(Input.GetKeyDown(right))
            {
                lastPressed = "right";
                rotateAccelTime = 0;
            }
            if(!Input.GetKey(right))
            {
                rotateRight = false;
            }

            if(Input.GetKey(fire))
            {
                shoot = true;
            }
            if(!Input.GetKey(fire))
            {
                shoot = false;
            }
        }
    }

    void FixedUpdate()
    {
        if(health <= 0)
        {
            //Debug.Log(health);
            isDead = true;
            canMove = false;
            activatethrust = false;
            activateBoost = false;
            rotateLeft = false;
            rotateRight = false;
            shoot = false;
            canShoot = false;
        }

        if(isDead == true)
        {
            if(deadTimer <= 0)
            {
                //Debug.Log("Reset");
                tf.position = Vector3.zero;

                health = 100f;
                activatethrust = false;
                activateBoost = false;
                rotateLeft = false;
                rotateRight = false;
                shoot = false;
                canShoot = true;
                canTakeDamage = true;
                canMove = true;
                isDead = false;
                deadTimer = 2f;
            }
            if(deadTimer > 0)
            {
                //Debug.Log(deadTimer);
                deadTimer -= Time.deltaTime;
            }
        }


        if(currentTime <= 0)
        {
            canShoot = true;
        }
        if(currentTime > 0)
        {
            currentTime -= Time.fixedDeltaTime;
        }

        if(damageCooldown <= 0)
        {
            canTakeDamage = true;
        }
        if(damageCooldown > 0)
        {
            damageCooldown -= Time.fixedDeltaTime;
        }

        if(shoot == true && canShoot == true)
        {
            canShoot = false;
            Shoot(bullet,straightShotLevel,spreadShotLevel);

            currentTime = shotCooldown;
        }

        //Debug.Log(lastPressed);
        if(activatethrust == true && activateBoost == false)
        {
            rb.AddForce(tf.up * moveSpeed);
            //Debug.Log("AddForce");
        }
        if(activateBoost == true)
        {
            rb.AddForce(tf.up * (moveSpeed * 2));
        }

        if(rotateRight == true && rotateLeft == false)
        {
            //tf.Rotate(-Vector3.forward * rotateSpeed);
            tf.Rotate(-Vector3.forward * rotateSpeed * rotateAccelTime / (rotateAccelTime + rotateAccelOffset));
            rotateAccelTime++;
            //Debug.Log("RotateRight");
        }
        if(rotateLeft == true && rotateRight == false)
        {
            //tf.Rotate(Vector3.forward * rotateSpeed);
            tf.Rotate(Vector3.forward * rotateSpeed * rotateAccelTime / (rotateAccelTime + rotateAccelOffset));
            rotateAccelTime++;
            //Debug.Log("RotateLeft");
        }
        if(rotateRight == true && rotateLeft == true)
        {
            if(lastPressed == "left")
            {
                //tf.Rotate(Vector3.forward * rotateSpeed);
                tf.Rotate(Vector3.forward * rotateSpeed * rotateAccelTime / (rotateAccelTime + rotateAccelOffset));
                //Debug.Log("Both: RotateLeft");
            }
            if(lastPressed == "right")
            {
                //tf.Rotate(-Vector3.forward * rotateSpeed);
                tf.Rotate(-Vector3.forward * rotateSpeed * rotateAccelTime / (rotateAccelTime + rotateAccelOffset));
                //Debug.Log("Both: RotateRight");
            }
            rotateAccelTime++;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if(canTakeDamage == true && health > 0)
        {
            canTakeDamage = false;
            damageCooldown = 0.5f;
            //Debug.Log("Collision: " + col);
            if(col.gameObject.tag == "Mauler" || col.gameObject.tag == "Mangler" || col.gameObject.tag == "Infector" || col.gameObject.tag == "Zapper")
            {
                //Debug.Log("Current Health: " + health);
                health -= col.gameObject.GetComponent<EnemyBehaviour>().getMeleeDamage();
                //Debug.Log("After Col Health: " + health);
            }
            if(col.gameObject.tag == "MaulerSpawner" || col.gameObject.tag == "ManglerSpawner" || col.gameObject.tag == "InfectorSpawner" || col.gameObject.tag == "ZapperSpawner")
            {
                //Debug.Log("Current Health: " + health);
                health -= col.gameObject.GetComponent<EnemySpawnerController>().getMeleeDamage();
                //Debug.Log("After Col Health: " + health);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "ZapperProjectile")
        {
            if(canTakeDamage == true && health > 0)
            {
                canTakeDamage = false;
                damageCooldown = 0.5f;
                health -= 10f;
                Destroy(col.gameObject);
            }
            else if(canTakeDamage == false)
            {
                Destroy(col.gameObject);
            }
        }
        if(col.gameObject.tag == "RawMineral")
        {
            GameManager.GetComponent<GameManager>().rawMineralCount += 1;
            Destroy(col.gameObject);
            Debug.Log(GameManager.GetComponent<GameManager>().rawMineralCount);

            if(GameManager.GetComponent<GameManager>().rawMineralCount == 10 || GameManager.GetComponent<GameManager>().rawMineralCount == 30)
            {
                if(straightShotLevel < 3)
                {
                    straightShotLevel += 1;
                }
            }
            else if(GameManager.GetComponent<GameManager>().rawMineralCount == 20 || GameManager.GetComponent<GameManager>().rawMineralCount == 40)
            {
                if(spreadShotLevel < 3)
                {
                    spreadShotLevel += 1;
                }
            }
        }
    }

    void Shoot(GameObject projectile, int straightLevel, int spreadLevel)
    {
        if(straightLevel == 1)
        {
            Instantiate(projectile, tf.TransformPoint(Vector3.up * 0.75f), tf.rotation);

            if(spreadLevel == 2)
            {
                Quaternion newRotation1 = Quaternion.Euler(new Vector3(0,0, (tf.localEulerAngles.z - 10)));
                Quaternion newRotation2 = Quaternion.Euler(new Vector3(0,0, (tf.localEulerAngles.z + 10)));
                Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * 0.5f)), newRotation1);
                Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * -0.5f)), newRotation2);
            }
            else if(spreadLevel == 3)
            {
                Quaternion newRotation1 = Quaternion.Euler(new Vector3(0,0, (tf.localEulerAngles.z - 10)));
                Quaternion newRotation2 = Quaternion.Euler(new Vector3(0,0, (tf.localEulerAngles.z + 10)));
                Quaternion newRotation3 = Quaternion.Euler(new Vector3(0,0, (tf.localEulerAngles.z - 20)));
                Quaternion newRotation4 = Quaternion.Euler(new Vector3(0,0, (tf.localEulerAngles.z + 20)));
                Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * 0.25f)), newRotation1);
                Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * -0.25f)), newRotation2);
                Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * 0.5f)), newRotation3);
                Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * -0.5f)), newRotation4);
            }

        }
        if(straightLevel == 2)
        {
            Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * 0.15f)), tf.rotation);
            Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * -0.15f)), tf.rotation);

            if(spreadLevel == 2)
            {
                Quaternion newRotation1 = Quaternion.Euler(new Vector3(0,0, (tf.localEulerAngles.z - 10)));
                Quaternion newRotation2 = Quaternion.Euler(new Vector3(0,0, (tf.localEulerAngles.z + 10)));
                Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * 0.5f)), newRotation1);
                Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * -0.5f)), newRotation2);
            }
            else if(spreadLevel == 3)
            {
                Quaternion newRotation1 = Quaternion.Euler(new Vector3(0,0, (tf.localEulerAngles.z - 10)));
                Quaternion newRotation2 = Quaternion.Euler(new Vector3(0,0, (tf.localEulerAngles.z + 10)));
                Quaternion newRotation3 = Quaternion.Euler(new Vector3(0,0, (tf.localEulerAngles.z - 20)));
                Quaternion newRotation4 = Quaternion.Euler(new Vector3(0,0, (tf.localEulerAngles.z + 20)));
                Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * 0.5f)), newRotation1);
                Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * -0.5f)), newRotation2);
                Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * 0.75f)), newRotation3);
                Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * -0.75f)), newRotation4);
            }
        }
        if (straightLevel == 3)
        {
            Instantiate(projectile, tf.TransformPoint(Vector3.up * 0.75f), tf.rotation);
            Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * 0.25f)), tf.rotation);
            Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * -0.25f)), tf.rotation);

            if(spreadLevel == 2)
            {
                Quaternion newRotation1 = Quaternion.Euler(new Vector3(0,0, (tf.localEulerAngles.z - 10)));
                Quaternion newRotation2 = Quaternion.Euler(new Vector3(0,0, (tf.localEulerAngles.z + 10)));
                Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * 0.5f)), newRotation1);
                Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * -0.5f)), newRotation2);
            }
            else if(spreadLevel == 3)
            {
                Quaternion newRotation1 = Quaternion.Euler(new Vector3(0,0, (tf.localEulerAngles.z - 10)));
                Quaternion newRotation2 = Quaternion.Euler(new Vector3(0,0, (tf.localEulerAngles.z + 10)));
                Quaternion newRotation3 = Quaternion.Euler(new Vector3(0,0, (tf.localEulerAngles.z - 20)));
                Quaternion newRotation4 = Quaternion.Euler(new Vector3(0,0, (tf.localEulerAngles.z + 20)));
                Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * 0.5f)), newRotation1);
                Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * -0.5f)), newRotation2);
                Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * 0.75f)), newRotation3);
                Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * -0.75f)), newRotation4);
            }
        }
        /*
        if (straightLevel == 4)
        {
            Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * 0.25f)), tf.rotation);
            Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * 0.75f)), tf.rotation);
            Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * -0.25f)), tf.rotation);
            Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * -0.75f)), tf.rotation);
        }
        if (straightLevel == 5)
        {
            Instantiate(projectile, tf.TransformPoint(Vector3.up * 0.75f), tf.rotation);
            Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * 0.5f)), tf.rotation);
            Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * 1f)), tf.rotation);
            Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * -0.5f)), tf.rotation);
            Instantiate(projectile, tf.TransformPoint((Vector3.up  * 0.75f) + (Vector3.right * -1f)), tf.rotation);
        }
        */
    }

    void upgradeFireRate()
    {
        shotCooldown = shotCooldown * 0.90f;
    }

    void upgradeStraightShot()
    {
        straightShotLevel += 1;
    }

    void upgradeSpreadShot()
    {
        spreadShotLevel += 1;
    }
 
}
