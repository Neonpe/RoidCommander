using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMineralBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tf;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();

        tf.rotation = Quaternion.Euler(new Vector3(0,0, Random.Range(0f, 360f)));
        rb.AddForce(tf.up * (Random.Range(0.25f, 1f)), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
