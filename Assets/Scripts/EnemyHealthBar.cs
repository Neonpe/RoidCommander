using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    Vector3 localScale;
    public GameObject parentEnemy;
    float currentHealth;
    float targetHealth;
    float previousTargetHealth;
    [SerializeField] float changeRate;
    int timer;

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        if(parentEnemy != null && (parentEnemy.tag == "Mauler" || parentEnemy.tag == "Mangler" ||parentEnemy.tag == "Infector" ||parentEnemy.tag == "Zapper"))
        {
            currentHealth = parentEnemy.GetComponent<EnemyBehaviour>().health;
        }
        else if(parentEnemy != null && (parentEnemy.tag == "MaulerSpawner" || parentEnemy.tag == "ManglerSpawner" ||parentEnemy.tag == "InfectorSpawner" ||parentEnemy.tag == "ZapperSpawner"))
        {
            currentHealth = parentEnemy.GetComponent<EnemySpawnerController>().health;
        }
        
        targetHealth = currentHealth;
        timer = 0;
        changeRate = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(parentEnemy.GetComponent<EnemyBehaviour>().health / 200f);
        if(parentEnemy == null)
        {
            Destroy(gameObject);
        }
        else if (parentEnemy != null)
        {
            //Debug.Log(parentEnemy.GetComponent<EnemyBehaviour>().health);
            if(parentEnemy.tag == "Mauler" || parentEnemy.tag == "Mangler" || parentEnemy.tag == "Infector" || parentEnemy.tag == "Zapper")
            {
                if(targetHealth != parentEnemy.GetComponent<EnemyBehaviour>().health)
                {
                    targetHealth = parentEnemy.GetComponent<EnemyBehaviour>().health;
                    timer = (int)(Mathf.Abs(currentHealth - targetHealth) * changeRate);
                }
            }
            else if(parentEnemy.tag == "MaulerSpawner" || parentEnemy.tag == "ManglerSpawner" || parentEnemy.tag == "InfectorSpawner" || parentEnemy.tag == "ZapperSpawner")
            {
                if(targetHealth != parentEnemy.GetComponent<EnemySpawnerController>().health)
                {
                    targetHealth = parentEnemy.GetComponent<EnemySpawnerController>().health;
                    timer = (int)(Mathf.Abs(currentHealth - targetHealth) * changeRate);
                }
            }
            if(timer > 0)
            {
                currentHealth -= (currentHealth - targetHealth) / timer;
                timer--;
            }
            if((parentEnemy.tag == "Mangler" || parentEnemy.tag == "Infector" || parentEnemy.tag == "Zapper"))
            {
                localScale.x = (currentHealth / 100f);
                transform.localScale = localScale;
                transform.position = new Vector3(parentEnemy.transform.position.x, (parentEnemy.transform.position.y - 0.375f), parentEnemy.transform.position.z);
            }
            if(parentEnemy.tag == "Mauler")
            {
                localScale.x = (currentHealth / 200f);
                transform.localScale = localScale;
                transform.position = new Vector3(parentEnemy.transform.position.x, (parentEnemy.transform.position.y - 0.5f), parentEnemy.transform.position.z);
            }
            if((parentEnemy.tag == "MaulerSpawner" || parentEnemy.tag == "ManglerSpawner" || parentEnemy.tag == "InfectorSpawner" || parentEnemy.tag == "ZapperSpawner"))
            {
                localScale.x = (currentHealth / 300f);
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
        if(parentEnemy != null && (parentEnemy.tag == "Mauler" || parentEnemy.tag == "Mangler" || parentEnemy.tag == "Infector" || parentEnemy.tag == "Zapper"))
        {
            if(parentEnemy.GetComponent<EnemyBehaviour>().health <= 0)
            {
                Destroy(gameObject);
            }
        }
        if(parentEnemy != null && (parentEnemy.tag == "MaulerSpawner" || parentEnemy.tag == "ManglerSpawner" || parentEnemy.tag == "InfectorSpawner" || parentEnemy.tag == "ZapperSpawner"))
        {
            if(parentEnemy.GetComponent<EnemySpawnerController>().health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
