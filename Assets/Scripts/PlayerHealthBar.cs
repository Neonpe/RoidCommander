using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    Vector3 localScale;
    public GameObject parentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
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
            localScale.x = (parentPlayer.GetComponent<Player>().health / 200f);
            transform.localScale = localScale;
            transform.position = new Vector3(parentPlayer.transform.position.x, (parentPlayer.transform.position.y - 0.5f), parentPlayer.transform.position.z);
        }
    }
    
    void FixedUpdate()
    {
        if(parentPlayer == null)
        {
            Destroy(gameObject);
        }
        if(parentPlayer != null)
        {
            if(parentPlayer.GetComponent<Player>().health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
