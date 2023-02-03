using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class BallScript : MonoBehaviour
{
    public float speedForce = 1.0f;
    //public float maxVelocity = 8.0f;
    private Vector2 direction;
    private Rigidbody2D rigi_bdy;

    private bool ballIsActive = false;
    private Vector2 ballPosition;
    private Vector2 ballInitialForce;
    private float originY;
    //referencia para que pelota siga al palo
    public GameObject playerObj;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.zero;
        direction.y = 1.0f;
        direction.x = 1.0f;
        rigi_bdy = GetComponent<Rigidbody2D>();
        //rigi_bdy.AddForce(direction * speedForce);
        //fuerza
        ballInitialForce = direction * speedForce;
        ballIsActive = false;
        ballPosition = transform.position;
        //para saber en que 'y' inició la pelota
        originY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //eyectar bola
        if(Input.GetButtonDown("Jump") == true)
        {
            if(!ballIsActive)
            {
                rigi_bdy.velocity = ballInitialForce;
                ballIsActive = true;
            }
        }

        //mover pelota junto a palo
        if(!ballIsActive && playerObj != null)
        {
            ballPosition.x = playerObj.transform.position.x;
            transform.position = ballPosition;
        }

        //todo: loose live 
        if(ballIsActive == true && transform.position.y < -6)
        {
            ballIsActive = false;
            ballPosition.x = playerObj.transform.position.x;
            ballPosition.y = originY;
            transform.position = ballPosition;

            //perder vida y luego revisar si es game over
            var wall = GameObject.Find("Walls");
            wall.SendMessage("TakeLive");
            wall.SendMessage("WinLose");
        }
            
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Colisión detectadah");
        var collid_g_obj = coll.collider.gameObject;

        if(collid_g_obj.tag == "Block")
        {
            //register block destruction in manager
            var wall = GameObject.Find("Walls");
            wall.SendMessage("TakeBlockCount");

            //destroy block
            var block = coll.collider.gameObject;
            block.SendMessage("Kill");
        }

        //gameplay of paddle
        if(collid_g_obj.tag == "Player")
        {
            var player_rgd_bdy = collid_g_obj.GetComponent<Rigidbody2D>();
            var new_direction = rigi_bdy.velocity;
            //game play from arkanoid...
            float player_offset = player_rgd_bdy.transform.position.x - transform.position.x;
            new_direction.x += player_rgd_bdy.velocity.x / 6.0f - player_offset * 5.3f;
            //decrease x speed a little... im lazy about correcting the vector angle
            new_direction.x *= 0.8f;
            //minimal y speed when hitting paddle
            if(new_direction.y < 1.3)
                new_direction.y = 1.3f; 
            
            //tood: preserve a minimal speed***
            //if(new_direction.magnitude 

            rigi_bdy.velocity = new_direction;
        }

        if(collid_g_obj.tag == "Wall")
        {
            //prevent wall horizontal lock
            if(Mathf.Abs(rigi_bdy.velocity.y) < 0.35f)
            {
                Debug.Log("Aquí se atoró la bola");
                var new_direction = rigi_bdy.velocity;
                new_direction.y = Mathf.Sign(rigi_bdy.velocity.y) * 1.24f;
                rigi_bdy.velocity = new_direction;
            }
        }

        //todo: set max speed...

        //correct velocity... to 45* degrees
        //var _direction = new Vector2(
        //    Mathf.Sign(rigi_bdy.velocity.x),
        //    Mathf.Sign(rigi_bdy.velocity.y)
        //);
        //rigi_bdy.velocity = _direction * speedForce;
         
        //todo bug fix paddle collission alskñjdfañslkjfdñlkasjdf
    }

}
