using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Rigidbody2D rb;
    private Transform tf;

    [SerializeField] private string enemyType;
    [SerializeField] private float meleeDamage = 10f;
    [SerializeField] private float rangedDamage = 5f;

    public GameObject parentSpawner;

    public GameObject target1;

    // Health
    public GameObject healthBar;
    public float health = 100f;

    // Movement
    private bool canMove = true;
    private float moveCooldown;

    // Shooting
    public GameObject projectile;
    private bool canShoot = true;
    private float shootCooldown = 1.5f;

    // Resources
    public GameObject rawMineral;
    private int heldMinerals = 0;

    bool playerInRange;
    bool mineralInRange;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();

        if(enemyType == "mauler")
        {
            health = 100f;
            meleeDamage = 25f;
            rangedDamage = 0f;
            moveCooldown = 1f;
        }
        else if(enemyType == "mangler")
        {
            health = 25f;
            meleeDamage = 10f;
            rangedDamage = 0f;
            moveCooldown = 0.5f;
        }
        else if(enemyType == "infector")
        {
            health = 10f;
            meleeDamage = 5f;
            rangedDamage = 0f;
            moveCooldown = 0.25f;
        }
        else if(enemyType == "zapper")
        {
            health = 50f;
            meleeDamage = 0f;
            rangedDamage = 25f;
            moveCooldown = 2f;
        }
        else
        {
            health = 100f;
            meleeDamage = 10f;
            rangedDamage = 0f;
            moveCooldown = 1f;
        }

        GameObject initHealthBar = Instantiate(healthBar, new Vector3(tf.position.x, (tf.position.y - 0.5f), tf.position.z), tf.rotation);
        EnemyHealthBar initEnemyHealthBar = initHealthBar.GetComponent<EnemyHealthBar>();
        if(initEnemyHealthBar != null)
        {
            initEnemyHealthBar.parentEnemy = gameObject;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(health <= 0)
        {
            for(int i=0;i<heldMinerals;i++)
            {
                Instantiate(rawMineral, tf.position, tf.rotation);
            }
            Destroy(gameObject);
        }

        if(moveCooldown <= 0)
        {
            canMove = true;
        }
        if(moveCooldown > 0)
        {
            moveCooldown -= Time.deltaTime;
        }

        if(shootCooldown <= 0)
        {
            canShoot = true;
        }
        if(shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
        /*
        if(target1 == null)
        {
            GameObject[] inRange = tf.Find("RangeDetect").GetComponent<RangeDetect>().getInRange();
            if(inRange != null && inRange.Length > 0)
            {
                foreach (GameObject obj in inRange)
                {
                    if(obj.tag == "Player")
                    {
                        target1 = obj;
                        break;
                    }
                }
                
                //foreach (GameObject obj in inRange)
                //{
                    //if(obj.tag == "Enemy16" || obj.tag == "Enemy32" || obj.tag == "EnemySpawner")
                    //{
                        //target1 = obj;
                        //break;
                    //}
                //}
                
            }
        }
        */
        GameObject[] inRange = tf.Find("RangeDetect").GetComponent<RangeDetect>().getInRange();
        if(inRange == null || inRange.Length == 0)
        {
            target1 = null;
        }
        if(inRange != null && inRange.Length > 0)
        {
            playerInRange = false;
            mineralInRange = false;

            foreach (GameObject obj in inRange)
            {
                if(obj != null && obj.tag == "Player")
                {
                    target1 = obj;
                    playerInRange = true;
                    break;
                }
            }

            foreach(GameObject obj in inRange)
            {
                if(obj != null && obj.tag == "RawMineral")
                {
                    target1 = obj;
                    mineralInRange = true;
                    break;
                }
            }

            if(playerInRange == false && mineralInRange == false)
            {
                target1 = null;
            }

        }

        if(playerInRange == false && mineralInRange == true)
        {
            if(target1 != null && target1.tag == "RawMineral")
            {
                if(canMove == true)
                {
                    canMove = false;
                    moveCooldown = Random.Range(0.5f, 1.5f);
                    
                    float targetx = target1.transform.position.x - tf.position.x;
                    float targety = target1.transform.position.y - tf.position.y;

                    float angle = -90f + Mathf.Atan2(targety, targetx) * Mathf.Rad2Deg;
                    tf.rotation = Quaternion.Euler(new Vector3(0,0,angle));

                    rb.AddForce(tf.up * Random.Range(1f, 2f), ForceMode2D.Impulse);
                }
            }
        }


        if(enemyType == "mauler")
        {
            if(target1 != null && (target1.tag == "Player" || target1.tag == "Infector" || target1.tag == "Zapper" || target1.tag == "InfectorSpawner" || target1.tag == "ZapperSpawner"))
            {
                if(canMove == true)
                {
                    canMove = false;
                    moveCooldown = Random.Range(1f, 1.5f);
                    
                    float targetx = target1.transform.position.x - tf.position.x;
                    float targety = target1.transform.position.y - tf.position.y;

                    float angle = -90f + Mathf.Atan2(targety, targetx) * Mathf.Rad2Deg;
                    tf.rotation = Quaternion.Euler(new Vector3(0,0,angle));
                    
                    /*
                    Vector3 targetPos = new Vector3(target1.transform.position.x, target1.transform.position.y, 0);
                    tf.LookAt(targetPos);
                    */

                    rb.AddForce(tf.up * Random.Range(2f, 3f), ForceMode2D.Impulse);
                }
            }
            if(target1 == null)
            {
                if(canMove == true)
                {
                    canMove = false;
                    moveCooldown = Random.Range(0.5f, 2f);
                    
                    tf.rotation = Quaternion.Euler(new Vector3(0,0, Random.Range(0f, 360f)));

                    rb.AddForce(tf.up * (Random.Range(1f, 3f)), ForceMode2D.Impulse);
                }
            }
        }
        else if(enemyType == "mangler")
        {
            if(target1 != null && (target1.tag == "Player" || target1.tag == "Infector" || target1.tag == "Zapper" || target1.tag == "InfectorSpawner" || target1.tag == "ZapperSpawner"))
            {
                if(canMove == true)
                {
                    canMove = false;
                    moveCooldown = Random.Range(0.5f, 1f);

                    float targetx = target1.transform.position.x - tf.position.x;
                    float targety = target1.transform.position.y - tf.position.y;

                    float angle = -90f + Mathf.Atan2(targety, targetx) * Mathf.Rad2Deg;
                    tf.rotation = Quaternion.Euler(new Vector3(0,0,angle));

                    /*
                    Vector3 targetPos = new Vector3(target1.transform.position.x, target1.transform.position.y, 0);
                    tf.LookAt(targetPos);
                    */
                    rb.AddForce(tf.up * Random.Range(1f, 1.5f), ForceMode2D.Impulse);
                }
            }
            if(target1 == null)
            {
                if(canMove == true)
                {
                    canMove = false;
                    moveCooldown = Random.Range(0.25f, 1.5f);
                    
                    tf.rotation = Quaternion.Euler(new Vector3(0,0, Random.Range(0f, 360f)));

                    rb.AddForce(tf.up * (Random.Range(0.5f, 2f)), ForceMode2D.Impulse);
                }
            }
        }
        else if(enemyType == "infector")
        {
            if(target1 != null && (target1.tag == "Player" || target1.tag == "Mauler" || target1.tag == "Mangler" || target1.tag == "Zapper" || target1.tag == "MaulerSpawner" || target1.tag == "ManglerSpawner" || target1.tag == "ZapperSpawner"))
            {
                if(canMove == true)
                {
                    canMove = false;
                    moveCooldown = Random.Range(0f, 0.10f);

                    float targetx = target1.transform.position.x - tf.position.x;
                    float targety = target1.transform.position.y - tf.position.y;

                    float angle = -90f + Mathf.Atan2(targety, targetx) * Mathf.Rad2Deg;
                    tf.rotation = Quaternion.Euler(new Vector3(0,0,angle));
                    
                    /*
                    Vector3 targetPos = new Vector3(target1.transform.position.x, target1.transform.position.y, 0);
                    tf.LookAt(targetPos);
                    */
                    rb.AddForce(tf.up * Random.Range(0.25f, 0.5f), ForceMode2D.Impulse);
                }
            }
            if(target1 == null)
            {
                if(canMove == true)
                {
                    canMove = false;
                    moveCooldown = Random.Range(0f, 1f);
                    
                    tf.rotation = Quaternion.Euler(new Vector3(0,0, Random.Range(0f, 360f)));

                    rb.AddForce(tf.up * (Random.Range(0.10f, 1f)), ForceMode2D.Impulse);
                }
            }
        }
        if(enemyType == "zapper")
        {
            if(target1 != null && (target1.tag == "Player" || target1.tag == "Mauler" || target1.tag == "Mangler" || target1.tag == "Infector" || target1.tag == "MaulerSpawner" || target1.tag == "ManglerSpawner" || target1.tag == "InfectorSpawner"))
            {
                if(canShoot == true)
                {
                    canShoot = false;

                    float targetx = target1.transform.position.x - tf.position.x;
                    float targety = target1.transform.position.y - tf.position.y;

                    float angle = -90f + Mathf.Atan2(targety, targetx) * Mathf.Rad2Deg;
                    tf.rotation = Quaternion.Euler(new Vector3(0,0,angle));

                    Instantiate(projectile, tf.TransformPoint(Vector3.up * 0.75f), tf.rotation);

                    shootCooldown = Random.Range(2f, 3f);
                }

                if(canMove == true && canShoot == false)
                {
                    canMove = false;
                    moveCooldown = Random.Range(1.5f, 3f);
                    
                    float targetx = target1.transform.position.x - tf.position.x;
                    float targety = target1.transform.position.y - tf.position.y;

                    float angle = 90f + Mathf.Atan2(targety, targetx) * Mathf.Rad2Deg;
                    tf.rotation = Quaternion.Euler(new Vector3(0,0,angle));
                    
                    /*
                    Vector3 targetPos = new Vector3(target1.transform.position.x, target1.transform.position.y, 0);
                    tf.LookAt(targetPos);
                    */

                    rb.AddForce(tf.up * Random.Range(2f, 3f), ForceMode2D.Impulse);
                }
            }
            if(target1 == null)
            {
                if(canMove == true)
                {
                    canMove = false;
                    moveCooldown = Random.Range(1f, 3f);
                    
                    tf.rotation = Quaternion.Euler(new Vector3(0,0, Random.Range(0f, 360f)));

                    rb.AddForce(tf.up * (Random.Range(1f, 2f)), ForceMode2D.Impulse);
                }
            }
        }
        else
        {
            if(target1 != null && (target1.tag == "Player" || target1.tag == "Mauler" || target1.tag == "Mangler" || target1.tag == "Infector" || target1.tag == "Zapper" || target1.tag == "MaulerSpawner" || target1.tag == "ManglerSpawner" || target1.tag == "InfectorSpawner" || target1.tag == "ZapperSpawner"))
            {
                if(canMove == true)
                {
                    canMove = false;
                    moveCooldown = Random.Range(1f, 1.5f);
                    
                    float targetx = target1.transform.position.x - tf.position.x;
                    float targety = target1.transform.position.y - tf.position.y;

                    float angle = -90f + Mathf.Atan2(targety, targetx) * Mathf.Rad2Deg;
                    tf.rotation = Quaternion.Euler(new Vector3(0,0,angle));
                    
                    /*
                    Vector3 targetPos = new Vector3(target1.transform.position.x, target1.transform.position.y, 0);
                    tf.LookAt(targetPos);
                    */

                    rb.AddForce(tf.up * Random.Range(2f, 3f), ForceMode2D.Impulse);
                }
            }
            if(target1 == null)
            {
                if(canMove == true)
                {
                    canMove = false;
                    moveCooldown = Random.Range(0.5f, 2f);
                    
                    tf.rotation = Quaternion.Euler(new Vector3(0,0, Random.Range(0f, 360f)));

                    rb.AddForce(tf.up * (Random.Range(1f, 3f)), ForceMode2D.Impulse);
                }
            }
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("Collision: " + col);
        if(col.gameObject.tag == "ShipBullet")
        {
            //Debug.Log("Current Health: " + health);
            health -= 25f;
            //Debug.Log("After Col Health: " + health);
        }
        if(col.gameObject.tag == "ZapperProjectile")
        {
            if(gameObject.tag == "Mauler" || gameObject.tag == "Mangler" || gameObject.tag == "Infector" || gameObject.tag == "MaulerSpawner" || gameObject.tag == "ManglerSpawner" || gameObject.tag == "InfectorSpawner")
            {
                health -= 10f;
                Destroy(col.gameObject);
            }
        }
        if(col.gameObject.tag == "RawMineral")
        {
            heldMinerals += 1;
            Destroy(col.gameObject);
        }
    }

    public float getMeleeDamage()
    {
        return meleeDamage;
    }

    public float getRangedDamage()
    {
        return rangedDamage;
    }
}
