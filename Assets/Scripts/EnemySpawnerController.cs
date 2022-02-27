using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tf;

    [SerializeField] private string spawnerType;
    [SerializeField] private float meleeDamage = 10f;
    [SerializeField] private float rangedDamage = 5f;

    // Health
    public GameObject healthBar;
    public float health = 1000f;

    // Spawn Points
    private Vector3 spawn1;
    private Vector3 spawn2;
    private Vector3 spawn3;
    private Vector3 spawn4;

    public GameObject enemyType;

    // Spawn Timing
    private float spawnCooldown;
    private bool canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();

        if(spawnerType == "mauler")
        {
            health = 1000f;
            meleeDamage = 25f;
            rangedDamage = 0f;
        }
        if(spawnerType == "mangler")
        {
            health = 1000f;
            meleeDamage = 25f;
            rangedDamage = 0f;
        }
        if(spawnerType == "infector")
        {
            health = 1000f;
            meleeDamage = 25f;
            rangedDamage = 0f;
        }

        GameObject initHealthBar = Instantiate(healthBar, new Vector3(tf.position.x, (tf.position.y - 1f), tf.position.z), Quaternion.identity);
        EnemyHealthBar initEnemyHealthBar = initHealthBar.GetComponent<EnemyHealthBar>();
        if(initEnemyHealthBar != null)
        {
            initEnemyHealthBar.parentEnemy = gameObject;
        }

        //spawnTest();
        //spawnEnemy1();
        //spawnEnemy2();
        //spawnEnemy3();
        //spawnEnemy4();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(health <= 0)
        {
            Destroy(gameObject, 0.25f);
        }

        if(spawnCooldown <= 0)
        {
            canSpawn = true;
        }
        if(spawnCooldown > 0)
        {
            spawnCooldown -= Time.deltaTime;
        }

        if(canSpawn == true)
        {
            canSpawn = false;

            float spawnChoice = Random.Range(0f, 4f);

            if(spawnChoice >= 0f && spawnChoice <= 1f)
            {
                spawnEnemy1();
            }
            else if(spawnChoice > 1f && spawnChoice <= 2f)
            {
                spawnEnemy2();
            }
            else if(spawnChoice > 2f && spawnChoice <= 3f)
            {
                spawnEnemy3();
            }
            else if(spawnChoice > 3f && spawnChoice <= 4f)
            {
                spawnEnemy4();
            }
            else
            {
                spawnEnemy1();
            }

            spawnCooldown = Random.Range(1f, 5f);
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
    }

    public float getMeleeDamage()
    {
        return meleeDamage;
    }

    private void spawnTest()
    {
        // Enemy 1: Top Left
        GameObject enemy1 = Instantiate(enemyType, new Vector3((tf.position.x - 0.75f), (tf.position.y + 0.75f), tf.position.z), tf.rotation);
        EnemyBehaviour enemy1Behaviour = enemy1.GetComponent<EnemyBehaviour>();
        if(enemy1Behaviour != null)
        {
            enemy1Behaviour.parentSpawner = gameObject;
        }
        // Enemy 2: Top Right
        GameObject enemy2 = Instantiate(enemyType, new Vector3((tf.position.x + 0.75f), (tf.position.y + 0.75f), tf.position.z), tf.rotation);
        EnemyBehaviour enemy2Behaviour = enemy2.GetComponent<EnemyBehaviour>();
        if(enemy2Behaviour != null)
        {
            enemy2Behaviour.parentSpawner = gameObject;
        }
        // Enemy 3: Bottom Left
        GameObject enemy3 = Instantiate(enemyType, new Vector3((tf.position.x - 0.75f), (tf.position.y - 0.75f), tf.position.z), tf.rotation);
        EnemyBehaviour enemy3Behaviour = enemy3.GetComponent<EnemyBehaviour>();
        if(enemy3Behaviour != null)
        {
            enemy3Behaviour.parentSpawner = gameObject;
        }
        // Enemy 4: Bottom Right
        GameObject enemy4 = Instantiate(enemyType, new Vector3((tf.position.x + 0.75f), (tf.position.y - 0.75f), tf.position.z), tf.rotation);
        EnemyBehaviour enemy4Behaviour = enemy4.GetComponent<EnemyBehaviour>();
        if(enemy4Behaviour != null)
        {
            enemy4Behaviour.parentSpawner = gameObject;
        }
    }

    private void spawnEnemy1()
    {
        // Enemy 1: Top Left
        //GameObject enemy1 = Instantiate(enemyType, new Vector3((tf.position.x - 0.75f), (tf.position.y + 0.75f), tf.position.z), tf.rotation);
        GameObject enemy1 = Instantiate(enemyType, tf.TransformPoint(new Vector3(-1.75f, 1.75f, 0f)), Quaternion.identity);
        EnemyBehaviour enemy1Behaviour = enemy1.GetComponent<EnemyBehaviour>();
        Rigidbody2D enemy1Rb = enemy1.GetComponent<Rigidbody2D>();
        if(enemy1Behaviour != null)
        {
            enemy1Behaviour.parentSpawner = gameObject;
            //enemy1Rb.AddForce((tf.TransformPoint(new Vector3(-1.25f,1.25f,0f))), ForceMode2D.Impulse);
            enemy1Rb.AddForce((tf.up + (tf.right * -1)) * 1.5f, ForceMode2D.Impulse);
        }
    }

    private void spawnEnemy2()
    {
        // Enemy 2: Top Right
        //GameObject enemy2 = Instantiate(enemyType, new Vector3((tf.position.x + 0.75f), (tf.position.y + 0.75f), tf.position.z), tf.rotation);
        GameObject enemy2 = Instantiate(enemyType, tf.TransformPoint(new Vector3(1.75f, 1.75f, 0f)), Quaternion.identity);
        EnemyBehaviour enemy2Behaviour = enemy2.GetComponent<EnemyBehaviour>();
        Rigidbody2D enemy2Rb = enemy2.GetComponent<Rigidbody2D>();
        if(enemy2Behaviour != null)
        {
            enemy2Behaviour.parentSpawner = gameObject;
            //enemy2Rb.AddForce((tf.TransformPoint(new Vector3(1.25f,1.25f,0f))), ForceMode2D.Impulse);
            enemy2Rb.AddForce((tf.up + tf.right) * 1.5f, ForceMode2D.Impulse);
        }
    }

    private void spawnEnemy3()
    {
        // Enemy 3: Bottom Left
        //GameObject enemy3 = Instantiate(enemyType, new Vector3((tf.position.x - 0.75f), (tf.position.y - 0.75f), tf.position.z), tf.rotation);
        GameObject enemy3 = Instantiate(enemyType, tf.TransformPoint(new Vector3(-1.75f, -1.75f, 0f)), Quaternion.identity);
        EnemyBehaviour enemy3Behaviour = enemy3.GetComponent<EnemyBehaviour>();
        Rigidbody2D enemy3Rb = enemy3.GetComponent<Rigidbody2D>();
        if(enemy3Behaviour != null)
        {
            enemy3Behaviour.parentSpawner = gameObject;
            //enemy3Rb.AddForce((tf.TransformPoint(new Vector3(-1.25f,-1.25f,0f))), ForceMode2D.Impulse);
            enemy3Rb.AddForce(((tf.up * -1) + (tf.right * -1)) * 1.5f, ForceMode2D.Impulse);
        }
    }

    private void spawnEnemy4()
    {
        // Enemy 4: Bottom Right
        //GameObject enemy4 = Instantiate(enemyType, new Vector3((tf.position.x + 0.75f), (tf.position.y - 0.75f), tf.position.z), tf.rotation);
        GameObject enemy4 = Instantiate(enemyType, tf.TransformPoint(new Vector3(1.75f, -1.75f, 0f)), Quaternion.identity);
        EnemyBehaviour enemy4Behaviour = enemy4.GetComponent<EnemyBehaviour>();
        Rigidbody2D enemy4Rb = enemy4.GetComponent<Rigidbody2D>();
        if(enemy4Behaviour != null)
        {
            enemy4Behaviour.parentSpawner = gameObject;
            //enemy4Rb.AddForce((tf.TransformPoint(new Vector3(1.25f,-1.25f,0f))), ForceMode2D.Impulse);
            enemy4Rb.AddForce(((tf.up * -1) + tf.right) * 1.5f, ForceMode2D.Impulse);
        }
    }
}
