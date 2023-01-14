using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class BallScript : MonoBehaviour
{
    public float speedForce = 1.0f;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.zero;
        direction.y = 1.0f;
        direction.x = 1.0f;
        var rigi_bdy = GetComponent<Rigidbody2D>();
        rigi_bdy.AddForce(direction * speedForce);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Colisión detectadah");
        //then with blocks
        if(coll.collider.gameObject.tag == "Wall" || 
        coll.collider.gameObject.tag == "Player")
        {
            var _normal = coll.contacts[0].normal;
            var rigi_bdy = GetComponent<Rigidbody2D>();
            //nasa physics
            rigi_bdy.AddForce(direction * _normal * speedForce);
        }
        //todo bug fix paddle collission alskñjdfañslkjfdñlkasjdf
    }

}
