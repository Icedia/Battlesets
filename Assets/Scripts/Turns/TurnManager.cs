using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour {
    private enum turn { TurnTimer };
   // private enum animturn { AnimTimer};
    private bool attackTurn = false;
    private bool defendTurn = false;
    private bool currentTurn = false;
    [SerializeField]private float defaultTurnTime = 5;
    private float currentTime;
    [SerializeField] private Text countDown;
    private SetSequence sequence;

	void Start ()
    {
        sequence = GetComponent<SetSequence>();
        attackTurn = true;
        currentTime = defaultTurnTime;
        StartCoroutine(TurnTimer());
        
    }
    private void Update()
    {
        countDown.text = "Time left:" + " " + currentTime;
    }


    void CheckTurn()
    {
        if (attackTurn == true)
        {
            Debug.Log("attackturn started");
            currentTurn = true;


            attackTurn = false;
            defendTurn = true;
            Debug.Log("attackturn is over");
            StartCoroutine(TurnTimer());

        }
        else
        {
            Debug.Log("defenturn started");
            currentTurn = true;
 
            defendTurn = false;
            attackTurn = true;
            Debug.Log("defendturn is over");
            StartCoroutine(TurnTimer());
        }
    }

    IEnumerator TurnTimer()
    {
        Debug.Log("select cards");
        //sequence.Generate();
        StartCoroutine(CountDown(defaultTurnTime));
        yield return new WaitForSecondsRealtime(defaultTurnTime);
        Debug.Log("end card select");
        yield return new WaitForSecondsRealtime(5);
        Debug.Log("end animation");
        CheckTurn();
    }

    IEnumerator CountDown(float time)
    {
        for (int i = 0; i < defaultTurnTime; i++)
        {
            if (time > 0)
            {
                yield return new WaitForSeconds(1);
                time--;
                currentTime = time;
                Debug.Log(currentTime);
            }
        }       
    }
}
