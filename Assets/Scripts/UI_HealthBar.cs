using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HealthBar : MonoBehaviour
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
        if(parentPlayer != null)
        {
            localScale.x = (parentPlayer.GetComponent<Player>().health / 100f) * 4;
            transform.localScale = localScale;
        }
    }
}
