using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    Vector3 localScale;
    public GameObject parentEnemy;

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(parentEnemy.GetComponent<EnemyBehaviour>().health / 200f);
        if(parentEnemy == null)
        {
            Destroy(gameObject);
        }
        else
        {
            if(parentEnemy != null && parentEnemy.tag == "Enemy16")
            {
                localScale.x = (parentEnemy.GetComponent<EnemyBehaviour>().health / 100f);
                transform.localScale = localScale;
                transform.position = new Vector3(parentEnemy.transform.position.x, (parentEnemy.transform.position.y - 0.375f), parentEnemy.transform.position.z);
            }
            if(parentEnemy != null && parentEnemy.tag == "Enemy32")
            {
                localScale.x = (parentEnemy.GetComponent<EnemyBehaviour>().health / 200f);
                transform.localScale = localScale;
                transform.position = new Vector3(parentEnemy.transform.position.x, (parentEnemy.transform.position.y - 0.5f), parentEnemy.transform.position.z);
            }
            if(parentEnemy != null && parentEnemy.tag == "EnemySpawner")
            {
                localScale.x = (parentEnemy.GetComponent<EnemySpawnerController>().health / 300f);
                transform.localScale = localScale;
                transform.position = new Vector3(parentEnemy.transform.position.x, (parentEnemy.transform.position.y - 1.75f), parentEnemy.transform.position.z);
            }
        }

        
    }

    void FixedUpdate()
    {
        if(parentEnemy == null)
        {
            Destroy(gameObject);
        }
        if(parentEnemy != null && parentEnemy.tag == "Enemy")
        {
            if(parentEnemy.GetComponent<EnemyBehaviour>().health <= 0)
            {
                Destroy(gameObject);
            }
        }
        if(parentEnemy != null && parentEnemy.tag == "EnemySpawner")
        {
            if(parentEnemy.GetComponent<EnemySpawnerController>().health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
