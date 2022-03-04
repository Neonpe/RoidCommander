using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private Transform tf;

    public GameObject effectShip;

    private float spawnCoolDown;
    private bool canSpawn;
    private int spawnAmount;

    private Vector3 spawnPosition;


    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();

        spawnCoolDown = Random.Range(0f,1f);
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(spawnCoolDown <= 0)
        {
            canSpawn = true;
        }
        if(spawnCoolDown > 0)
        {
            spawnCoolDown -= Time.fixedDeltaTime;
        }

        if(canSpawn == true)
        {
            canSpawn = false;
            
            spawnAmount = Random.Range(1,4);

            for(int i=0;i<spawnAmount;i++)
            {
                int negate = Random.Range(1,5);
                float spawnX = Random.Range(7f,8f);
                float spawnY = Random.Range(7f,8f);;

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
                Instantiate(effectShip, tf.TransformPoint(spawnPosition), tf.rotation);
            }

            spawnCoolDown = Random.Range(2f, 7.5f);
        }
    }
}
