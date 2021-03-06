﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    // Player Turn
    private bool attackTurn = false;
    private bool defendTurn = false;

    // Check if we can play cards.
    private bool cardPhase = false;

    // Time each turn.
    [SerializeField] private float defaultTurnTime = 5;
    // Time that is being count down.
    [SerializeField] private float currentTime;
    // Countdown UI text.
    [SerializeField] private Text countDownText;

    // IEnumerators.
    private IEnumerator countDown;
    private IEnumerator animationTime;

    // Scripts.
    [SerializeField] private SetSequence sequence;
    [SerializeField] private CardHolder cardHolder;
    [SerializeField] private Player player;
    [SerializeField] private Enemy enemy;

    public int turnDamage = 0;

	void Start ()
    {
        // Assign IEnumerators.
        countDown = CountDown();
        animationTime = AnimationTime();

        // Set default values.
        currentTime = defaultTurnTime;
        UpdateCountDownText();

        attackTurn = true;
        cardPhase = true;
        currentTime = defaultTurnTime;

        StartCardPhase();
    }
    
    void Update()
    {
        CheckSequenceComplete();
    }

    /// <summary>
    /// Switches the turns between attacking and defending.
    /// </summary>
    void CheckTurn()
    {
        StopCoroutine(animationTime);

        if (attackTurn == true)
        {
            cardPhase = true;
            attackTurn = false;
            defendTurn = true;
            player.AttackAnim();
            enemy.DefendAnim();

            StartCardPhase();
        }
        else
        {
            cardPhase = true;
            defendTurn = false;
            attackTurn = true;
            player.DefendAnim();
            enemy.AttackAnim();

            StartCardPhase();
        }
    }

    void EndTurn()
    {
        cardPhase = false;
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

        animationTime = AnimationTime();
        StartCoroutine(animationTime);
    }

    void StartCardPhase()
    {
        Debug.Log("Start CardPhase");
        sequence.GenerateSequence();
        cardHolder.GetCards();

        // Start the countdown.
        countDown = CountDown();
        StartCoroutine(countDown);
    }

    IEnumerator CountDown()
    {
        currentTime = defaultTurnTime;
        UpdateCountDownText();

        while (true)
        {
            yield return new WaitForSeconds(1);
            print("countdown");
            currentTime--;
            UpdateCountDownText();

            // Check if the time is up.
            if (currentTime == 0)
            {
                StopCoroutine(countDown);
                EndTurn();
            }
        }
    }

    /// <summary>
    /// Update the text of our countdown timer.
    /// </summary>
    void UpdateCountDownText()
    {
        countDownText.text = "Time left:" + " " + currentTime;
    }

    /// <summary>
    /// Checks if the player has finished the sequence before the times.
    /// </summary>
    void CheckSequenceComplete()
    {
        if (sequence.CurrentSequenceNum > sequence.CurrentSequence.Count - 1 &&
            cardPhase == true)
        {
            print("Sequence Finished");
            StopCoroutine(countDown);
            EndTurn();
        }
    }

    IEnumerator AnimationTime()
    {
        print("Animating");
        yield return new WaitForSeconds(5);
        Debug.Log("end animation");
        CheckTurn();
    }
}
