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

    // Health
    public GameObject healthBar;
    public float health = 100f;

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
        }
        if(enemyType == "mangler")
        {
            health = 25f;
            meleeDamage = 10f;
            rangedDamage = 0f;
        }
        if(enemyType == "infector")
        {
            health = 10f;
            meleeDamage = 5f;
            rangedDamage = 0f;
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
            Destroy(gameObject, 0.25f);
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
}
