using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {
    private enum turn { TurnTimer };
   // private enum animturn { AnimTimer};
    private bool attackTurn = false;
    private bool defendTurn = false;
    private bool currentTurn = false;

	// Use this for initialization
	void Start ()
    {
        /*
        attackTurn = true;
        if (attackTurn == true)
        {
            StartCoroutine(TurnTimer());
            attackTurn = false;
            defendTurn = true;
            Debug.Log("attackturnEnd");
        }
        //StartCoroutine(TurnTimer());
        if (defendTurn == true)
        {
            StartCoroutine(TurnTimer());
            defendTurn = false;
            attackTurn = true;
            Debug.Log("defendturnEnd");
        }*/
        attackTurn = true;
        CheckTurn();

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void CheckTurn()
    {
        if (attackTurn == true)
        {
            Debug.Log("attackturn started");
            currentTurn = true;
            StartCoroutine(TurnTimer());
           // Debug.Log("routine over");
           // Debug.Log(currentTurn);
            if (currentTurn == false)
            {
                attackTurn = false;
                defendTurn = true;
                Debug.Log("attackturn is over");
            }
        }
        if (defendTurn == true)
        {
            Debug.Log("defenturn started");
            currentTurn = true;
            StartCoroutine(TurnTimer());
            if (currentTurn == false)
            {
                defendTurn = false;
                attackTurn = true;
                Debug.Log("defendturn is over");
            }
        }
    }

    IEnumerator TurnTimer()
    {

        Debug.Log("select cards");
        yield return new WaitForSecondsRealtime(5);
        Debug.Log("end card select");
        yield return new WaitForSecondsRealtime(5);
        Debug.Log("end animation");
        currentTurn = false;
        
    }


}
