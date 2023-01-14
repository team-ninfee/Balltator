using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    private Rigidbody2D rigi_bdy;
    // Start is called before the first frame update
    void Start()
    {
        rigi_bdy = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //variables
        float _horSpeed = 0.0f;

        //simple game logic
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            _horSpeed = -2.0f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _horSpeed = 2.0f;
        }
        rigi_bdy.AddForce(new Vector2(_horSpeed * 18.0f, 0.0f));
    }
}
