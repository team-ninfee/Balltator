using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class BallScript : MonoBehaviour
{
    public float speedForce = 1.0f;
    //public float maxVelocity = 8.0f;
    private Vector2 direction;
    private Rigidbody2D rigi_bdy;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.zero;
        direction.y = 1.0f;
        direction.x = 1.0f;
        rigi_bdy = GetComponent<Rigidbody2D>();
        //rigi_bdy.AddForce(direction * speedForce);
        rigi_bdy.velocity = direction * speedForce;
    }

    // Update is called once per frame
    void Update()
    {
        
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
            new_direction.x += player_rgd_bdy.velocity.x / 2 + player_offset / 5;
            //minimal y speed when hitting paddle
            if(new_direction.y < 0.4)
                new_direction.y = 0.4f; 
            rigi_bdy.velocity = new_direction;
        }

        if(collid_g_obj.tag == "Wall")
        {
            //prevent wall horizontal lock
            if(Mathf.Abs(rigi_bdy.velocity.x) < 0.1)
            {
                var new_direction = rigi_bdy.velocity;
                new_direction.y += Mathf.Sign(rigi_bdy.velocity.y) * 0.1f;
                rigi_bdy.velocity = new_direction;
            }
        }

        //correct velocity... to 45* degrees
        //var _direction = new Vector2(
        //    Mathf.Sign(rigi_bdy.velocity.x),
        //    Mathf.Sign(rigi_bdy.velocity.y)
        //);
        //rigi_bdy.velocity = _direction * speedForce;
         
        //todo bug fix paddle collission alskñjdfañslkjfdñlkasjdf
    }

}
