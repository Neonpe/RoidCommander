using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    Vector3 localScale;
    public GameObject parentPlayer;
    float currentHealth;
    float targetHealth;
    float previousTargetHealth;
    [SerializeField] float changeRate;
    int timer;

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        currentHealth = parentPlayer.GetComponent<Player>().health;
        targetHealth = currentHealth;
        timer = 0;
        changeRate = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(parentPlayer == null)
        {
            Destroy(gameObject);
        }
        else
        {
            if(targetHealth != parentPlayer.GetComponent<Player>().health)
            {
                targetHealth = parentPlayer.GetComponent<Player>().health;
                timer = (int)(Mathf.Abs(currentHealth - targetHealth) * changeRate);
            }
            if(timer > 0)
            {
                currentHealth -= (currentHealth - targetHealth) / timer;
                timer--;
            }
            localScale.x = (currentHealth / 200f);
            transform.localScale = localScale;
            transform.position = new Vector3(parentPlayer.transform.position.x, (parentPlayer.transform.position.y - 0.5f), parentPlayer.transform.position.z);
            /* Chris' previous code that I am rewriting to add smooth health transitions
            localScale.x = (parentPlayer.GetComponent<Player>().health / 200f);
            transform.localScale = localScale;
            transform.position = new Vector3(parentPlayer.transform.position.x, (parentPlayer.transform.position.y - 0.5f), parentPlayer.transform.position.z);
            */
        }
    }
    
    void FixedUpdate()
    {
        if(parentPlayer == null)
        {
            Destroy(gameObject);
        }
        /*
        if(parentPlayer != null)
        {
            if(parentPlayer.GetComponent<Player>().health <= 0)
            {
                Destroy(gameObject);
            }
        }
        */
    }
}
