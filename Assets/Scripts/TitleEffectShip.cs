using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleEffectShip : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tf;
    private SpriteRenderer spriteRenderer;

    public Sprite ship1;
    public Sprite ship2;
    public Sprite ship3;
    public Sprite ship4;
    public Sprite ship5;
    public Sprite ship6;
    public Sprite asteroid1;
    public Sprite asteroid2;
    public Sprite asteroid3;
    public Sprite asteroid4;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        int spritePick = Random.Range(1,11);
        switch (spritePick)
        {
            case 1:
                spriteRenderer.sprite = ship1;
                break;
            case 2:
                spriteRenderer.sprite = ship2;
                break;
            case 3:
                spriteRenderer.sprite = ship3;
                break;
            case 4:
                spriteRenderer.sprite = ship4;
                break;
            case 5:
                spriteRenderer.sprite = ship5;
                break;
            case 6:
                spriteRenderer.sprite = ship6;
                break;
            case 7:
                spriteRenderer.sprite = asteroid1;
                break;
            case 8:
                spriteRenderer.sprite = asteroid2;
                break;
            case 9:
                spriteRenderer.sprite = asteroid3;
                break;
            case 10:
                spriteRenderer.sprite = asteroid4;
                break;

            default:
                spriteRenderer.sprite = ship1;
                break;
        }

        float targetx = Random.Range(-5f, 5f) - tf.position.x;
        float targety = Random.Range(-5f, 5f) - tf.position.y;

        float angle = -90f + Mathf.Atan2(targety, targetx) * Mathf.Rad2Deg;
        tf.rotation = Quaternion.Euler(new Vector3(0,0,angle));

        rb.AddForce(tf.up * (Random.Range(2f, 5f)), ForceMode2D.Impulse);

        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
