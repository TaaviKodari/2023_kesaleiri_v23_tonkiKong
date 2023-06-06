using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D player;
    private Vector2 movement;
    private float horizontalInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        //Debug.Log(horizontalInput);
        movement.x = horizontalInput * moveSpeed; 
    }

    void FixedUpdate(){
        player.position += movement * Time.fixedDeltaTime;
    }

}
