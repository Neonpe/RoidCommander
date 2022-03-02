using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tf;

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

    // Health
    public GameObject healthBar;
    public float health = 100f;
    private float damageCooldown = 0.5f;


    // Checks
    private bool activatethrust = false;
    private bool activateBoost = false;
    private bool rotateLeft = false;
    private bool rotateRight = false;
    private string lastPressed;
    private bool shoot = false;
    private bool canShoot = true;
    private bool canTakeDamage = true;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        health = 100f;

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

    void FixedUpdate()
    {
        if(health <= 0)
        {
            tf.position = Vector3.zero;

            health = 100f;
            activatethrust = false;
            activateBoost = false;
            rotateLeft = false;
            rotateRight = false;
            shoot = false;
            canShoot = true;
            canTakeDamage = true;
        }

        if(currentTime <= 0)
        {
            canShoot = true;
        }
        if(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }

        if(damageCooldown <= 0)
        {
            canTakeDamage = true;
        }
        if(damageCooldown > 0)
        {
            damageCooldown -= Time.deltaTime;
        }

        if(shoot == true && canShoot == true)
        {
            canShoot = false;
            Instantiate(bullet, tf.TransformPoint(Vector3.up * 0.75f), tf.rotation);
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
        if(canTakeDamage == true)
        {
            canTakeDamage = false;
            damageCooldown = 0.5f;
            //Debug.Log("Collision: " + col);
            if(col.gameObject.tag == "Enemy16" || col.gameObject.tag == "Enemy32")
            {
                //Debug.Log("Current Health: " + health);
                health -= col.gameObject.GetComponent<EnemyBehaviour>().getMeleeDamage();
                //Debug.Log("After Col Health: " + health);
            }
            if(col.gameObject.tag == "EnemySpawner")
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
            if(canTakeDamage == true)
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
    }


}
