﻿using System.Collections;
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

    [SerializeField] private SetSequence sequence;
    [SerializeField] private CardHolder cardHolder;

    [SerializeField] private Player player;
    [SerializeField] private Enemy enemy;

    public int turnDamage = 0;

	void Start ()
    {
        attackTurn = true;
        currentTime = defaultTurnTime;
        StartCoroutine(TurnTimer());
    }

    void Update()
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
            player.AttackAnim();
            enemy.DefendAnim();

            Debug.Log("attackturn is over");
            StartCoroutine(TurnTimer());

        }
        else
        {
            Debug.Log("defenturn started");
            currentTurn = true;
            defendTurn = false;
            attackTurn = true;
            player.DefendAnim();
            enemy.AttackAnim();

            Debug.Log("defendturn is over");
            StartCoroutine(TurnTimer());
        }
    }

    IEnumerator TurnTimer()
    {
        Debug.Log("select cards");
        sequence.Generate();
        cardHolder.GetCards();

        StartCoroutine(CountDown(defaultTurnTime));
        yield return new WaitForSecondsRealtime(defaultTurnTime);
        cardHolder.HideCards();
        Debug.Log("end card select");

        if (attackTurn)
        {
            enemy.DoDamage(turnDamage);
        }
        else if (defendTurn)
        {
            player.DoDamage(30 - turnDamage);
        }

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
                //Debug.Log(currentTime);
            }
        }       
    }
}