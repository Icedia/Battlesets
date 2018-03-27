﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour {

    //Regulates Pause state
    public void SwitchPause()
    {
        if (PauseGame.pause == false)
        {
            PauseGame.pause = true;
        }
        else
        {
            PauseGame.pause = false;
        }
       
    }
}