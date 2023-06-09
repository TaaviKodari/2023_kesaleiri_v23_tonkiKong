using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    public Transform throwPoint;
    public GameObject fireball;
    private float timer = 0;
    public float timeBetweenThrows = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > timeBetweenThrows){
            timer = 0;
            ThrowBarrel();
        }
    }

    public void ThrowBarrel(){
        Instantiate(fireball, throwPoint.position, throwPoint.rotation);
    }
}
