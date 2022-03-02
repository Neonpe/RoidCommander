using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapperProjectileBehaviour : MonoBehaviour
{
    private Rigidbody2D projectileRb;
    private Transform projectileTf;

    [SerializeField] private float projectileSpeed = 3f;
    [SerializeField] private float projectileLifetime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        projectileRb = GetComponent<Rigidbody2D>();
        projectileTf = GetComponent<Transform>();
        Destroy(gameObject, projectileLifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        projectileRb.AddForce(projectileTf.up * projectileSpeed);
    }
}
