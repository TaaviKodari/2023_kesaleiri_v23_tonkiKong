using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Transform gameOver;
    public Transform winner;
    public float moveSpeed = 5f;
    public Rigidbody2D player;
    public float jumpForce = 5f;
    private Vector2 movement;
    private float horizontalInput;
    private float verticalInput;
    private bool grounded;
    private bool canClimb;
    private bool isClimbing;
    private Transform ladder;
    private float playerHeight;
    // Start is called before the first frame update
    void Start()
    {
        playerHeight = GetComponent<SpriteRenderer>().size.y;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        //Debug.Log(horizontalInput);
        verticalInput = Input.GetAxisRaw("Vertical");
        
        Jump();
        
        movement.y = 0;
        
        CheckClimbing();

        Climb();

        if(horizontalInput > 0 ){
            transform.localScale = new Vector3(1,1,1);
        }
        else if(horizontalInput < 0 ){
            transform.localScale = new Vector3(-1,1,1);
        }        

        movement.x = horizontalInput * moveSpeed; 
    }

    void FixedUpdate(){
        player.position += movement * Time.fixedDeltaTime;
    }

    public void Jump(){
        if(Input.GetButtonDown("Jump") && grounded == true){
            player.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
        }
    }

    public void CheckClimbing(){
        if(canClimb){
        
            if(verticalInput != 0 && grounded && horizontalInput == 0){
                isClimbing = true;
            }
        }
        else{
            isClimbing = false;
        }
    }

    public void Climb(){
        if(isClimbing){
            if(player.position.y <= ladder.transform.GetChild(0).transform.position.y+playerHeight/2 &&
             player.position.y >= ladder.transform.GetChild(1).transform.position.y-0.01f){
                
                player.velocity = Vector2.zero;
                player.isKinematic = true;
                movement.y = verticalInput * moveSpeed;
                player.position = new Vector2(ladder.transform.position.x, player.position.y);
            }else{
                isClimbing = false;
            }
        }else{
            player.isKinematic = false;
        }
        
    }

    void OnTriggerStay2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Platform")){
            grounded = true;
        }

        if(collision.gameObject.CompareTag("Ladder")){
            canClimb = true;
            ladder = collision.transform;
        }
    }

    void OnTriggerExit2D(Collider2D collision){
         if(collision.gameObject.CompareTag("Platform")){
            grounded = false;
        }

        if(collision.gameObject.CompareTag("Ladder")){
            canClimb = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Fireball")){
           StartCoroutine(GameOverDelay());
        }
          if(collision.gameObject.CompareTag("Princess")){
           StartCoroutine(WinnerDelay());
        }
    }

    IEnumerator WinnerDelay(){
        winner.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

     IEnumerator GameOverDelay(){
        gameOver.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
     }
}
