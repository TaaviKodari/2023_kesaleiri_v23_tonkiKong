using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    private Rigidbody2D fireball;
    public float moveSpeed = 5f;
    private Vector2 movement;
    float horizontalDir;

    public bool isGoingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        fireball = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isGoingRight){
            horizontalDir = 1;
        }else{
            horizontalDir = -1;
        }

        movement.x = horizontalDir * moveSpeed;
    }

    void FixedUpdate(){
        fireball.position += movement * Time.fixedDeltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Bound")){
            isGoingRight = !isGoingRight;
        }
        if(collision.gameObject.CompareTag("Fire")){
            Destroy(gameObject);
        }
    }
}
