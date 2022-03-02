using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HealthBar : MonoBehaviour
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
        changeRate = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(parentPlayer != null)
        {
            if(targetHealth != parentPlayer.GetComponent<Player>().health)
            {
                targetHealth = parentPlayer.GetComponent<Player>().health;
                timer = (int)(Mathf.Abs(currentHealth - targetHealth) / changeRate);
            }
            if(timer > 0)
            {
                currentHealth -= (currentHealth - targetHealth) / timer;
                timer--;
            }
            localScale.x = (currentHealth / 100f) * 4;
            transform.localScale = localScale;
        }
    }
}
