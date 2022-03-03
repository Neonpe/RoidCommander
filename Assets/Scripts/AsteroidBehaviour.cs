using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tf;
    public SpriteRenderer spriteRenderer;
    public Sprite smallv1;
    public Sprite smallv2;
    public Sprite largev1;
    public Sprite largev2;

    [SerializeField] private string asteroidSize;

    public GameObject smallAsteroid;
    public GameObject resourceDrop;

    [SerializeField] private int minDrops = 1;
    [SerializeField] private int maxDrops = 5;

    [SerializeField] private int minBreak = 2;
    [SerializeField] private int maxBreak = 4;

    private bool canBreak = false;
    private float initBreakTimer = 0.25f;

    private bool broken = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();

        tf.rotation = Quaternion.Euler(new Vector3(0,0, Random.Range(0f, 360f)));
        rb.AddForce(tf.up * (Random.Range(2f, 5f)), ForceMode2D.Impulse);

        int spritePick = Random.Range(1,3);
        //Debug.Log(spritePick);
        if(asteroidSize == "small")
        {
            if(spritePick == 1)
            {
                //Debug.Log("smallv1");
                spriteRenderer.sprite = smallv1;
            }
            else if(spritePick == 2)
            {
                //Debug.Log("smallv2");
                spriteRenderer.sprite = smallv2;
            }
        }
        if(asteroidSize == "large")
        {
            if(spritePick == 1)
            {
                //Debug.Log("largev1");
                spriteRenderer.sprite = largev1;
            }
            else if(spritePick == 2)
            {
                //Debug.Log("largev2");
                spriteRenderer.sprite = largev2;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(initBreakTimer <= 0)
        {
            canBreak = true;
        }
        if(initBreakTimer > 0)
        {
            initBreakTimer -= Time.fixedDeltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "ShipBullet" || col.gameObject.tag == "ZapperProjectile")
        {
            if(canBreak == true && broken == false)
            {
                broken = true;
                if(asteroidSize == "small")
                {
                    int dropAmount = Random.Range(minDrops, (maxDrops+1));

                    for(int i=0;i<dropAmount;i++)
                    {
                        Instantiate(resourceDrop, tf.position, tf.rotation);
                    }
                    Destroy(gameObject);
                }
                else if(asteroidSize == "large")
                {
                    int breakAmount = Random.Range(minBreak, (maxBreak+1));

                    for(int i=0;i<breakAmount;i++)
                    {
                        Instantiate(smallAsteroid, tf.position, tf.rotation);
                    }
                    Destroy(gameObject);
                }
            }
        }
    }
}
