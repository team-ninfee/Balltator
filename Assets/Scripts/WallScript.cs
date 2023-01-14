using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WallScript : MonoBehaviour
{
    //global state...
    private int playerLives;
    private int playerPoints;

    // Start is called before the first frame update
    void Start()
    {
        playerLives = 3;
        playerPoints = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddPoints(int points)
    {
        playerPoints += points;
    }

    void TakeLike()
    {
        playerLives--;
    }

    void WinLose()
    {
        if(playerLives == 0)
        {
            SceneManager.LoadScene("Level001");
        }
    }
}
