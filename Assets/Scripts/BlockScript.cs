using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Some logic to add score to global state
    void OnCollisionEnter2D()
    {
         var wall = GameObject.Find("Walls");
         wall.SendMessage("AddPoints");
    }
}