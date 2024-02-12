using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private bool _isGameOver = false;

    private float lastTaapTime  = 0;
    private float doubleTapThreshold = 0.3f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1);
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (Time.time - lastTaapTime <= doubleTapThreshold && _isGameOver == true)
                {
                    lastTaapTime = 0;
                    SceneManager.LoadScene(1);
                } else
                {
                    lastTaapTime = Time.time;
                }
            }
        }

    }
    public void GameOver()
    {
        _isGameOver = true;
    }

}
