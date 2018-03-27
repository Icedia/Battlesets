using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

    static public bool pause = false;
    [SerializeField] private GameObject pauseMenu;

    //pauses game
    void Update()
    {

        if (pause == false)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }

        else
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }

    }
}
