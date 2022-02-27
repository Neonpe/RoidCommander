using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody2D bulletRb;
    private Transform bulletTf;

    [SerializeField] private float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        bulletTf = GetComponent<Transform>();
        bulletRb.AddForce(bulletTf.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(gameObject, 3f);
    }

    void FixedUpdate()
    {
        
    }
}
