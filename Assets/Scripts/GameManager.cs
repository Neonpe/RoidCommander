using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Transform tf;
    private Transform playerTf;

    public GameObject player;
    public GameObject[] planetList;
    
    public int rawMineralCount;

    // Asteroid Spawning
    public GameObject largeAsteroid;
    public GameObject smallAsteroid;

    private int largeSpawnAmount;
    private int smallSpawnAmount;

    private Vector3 spawnPosition;
    private Vector3 planetSpawnPosition;

    private int planetSpawnCount;

    [SerializeField] private float asteroidSpawnCooldown = 0f;

    private bool canSpawnAsteroid = true;

    // Time
    public float gameTimer;



    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        playerTf = player.GetComponent<Transform>();
        rawMineralCount = 0;
        planetSpawnCount = 1;
        gameTimer = 0f;

        if(planetList != null && planetList.Length > 0)
        {
            foreach(GameObject obj in planetList)
            {
                float spawnX = 0f;
                float spawnY = 0f;
                if(planetSpawnCount == 1)
                {
                    spawnX = Random.Range(10f,30f);
                    spawnY = Random.Range(10f,30f);
                }
                else if(planetSpawnCount == 2)
                {
                    spawnX = Random.Range(-30f,-10f);
                    spawnY = Random.Range(-30f,-10f);
                }
                else if(planetSpawnCount == 3)
                {
                    spawnX = Random.Range(-30f,-10f);
                    spawnY = Random.Range(10f,30f);
                }
                else if(planetSpawnCount == 4)
                {
                    spawnX = Random.Range(10f,30f);
                    spawnY = Random.Range(-30f,-10f);
                }
                else if(planetSpawnCount == 5)
                {
                    spawnX = Random.Range(50f,70f);
                    spawnY = Random.Range(10f,30f);
                }
                else if(planetSpawnCount == 6)
                {
                    spawnX = Random.Range(-70f,-50f);
                    spawnY = Random.Range(-30f,-10f);
                }
                else if(planetSpawnCount == 7)
                {
                    spawnX = Random.Range(-30f,-10f);
                    spawnY = Random.Range(50f,70f);
                }
                else if(planetSpawnCount == 8)
                {
                    spawnX = Random.Range(10f,30f);
                    spawnY = Random.Range(-50f,-70f);
                }

                spawnPosition = new Vector3(spawnX, spawnY, 0);
                Instantiate(obj, playerTf.TransformPoint(spawnPosition), tf.rotation);
                planetSpawnCount += 1;
            }
        }
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        gameTimer += Time.fixedDeltaTime;
        
        if(asteroidSpawnCooldown <= 0)
        {
            canSpawnAsteroid = true;
        }
        if(asteroidSpawnCooldown > 0)
        {
            asteroidSpawnCooldown -= Time.fixedDeltaTime;
        }

        if(canSpawnAsteroid == true)
        {
            canSpawnAsteroid = false;
            
            largeSpawnAmount = Random.Range(0,3);
            smallSpawnAmount = Random.Range(0,3);

            for(int i=0;i<largeSpawnAmount;i++)
            {
                int negate = Random.Range(1,5);
                float spawnX = Random.Range(5f,10f);
                float spawnY = Random.Range(5f,10f);;

                if(negate == 2)
                {  
                    spawnX = spawnX * -1;
                }
                else if(negate == 3)
                {  
                    spawnY = spawnY * -1;
                }
                else if(negate == 4)
                {  
                    spawnX = spawnX * -1;
                    spawnY = spawnY * -1;
                }

                spawnPosition = new Vector3(spawnX, spawnY, 0);
                Instantiate(largeAsteroid, playerTf.TransformPoint(spawnPosition), tf.rotation);
            }
            for(int i=0;i<smallSpawnAmount;i++)
            {
                int negate = Random.Range(1,5);
                float spawnX = Random.Range(5f,10f);
                float spawnY = Random.Range(5f,10f);;

                if(negate == 2)
                {  
                    spawnX = spawnX * -1;
                }
                else if(negate == 3)
                {  
                    spawnY = spawnY * -1;
                }
                else if(negate == 4)
                {  
                    spawnX = spawnX * -1;
                    spawnY = spawnY * -1;
                }

                spawnPosition = new Vector3(spawnX, spawnY, 0);
                Instantiate(smallAsteroid, playerTf.TransformPoint(spawnPosition), tf.rotation);
            }

            asteroidSpawnCooldown = Random.Range(3f,10f);
        }
    }
}
