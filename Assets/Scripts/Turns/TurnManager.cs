using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TurnManager : MonoBehaviour
{
    // Player Turn
    [SerializeField] private bool attackTurn = false;
    [SerializeField] private bool defendTurn = false;

    // Check if we can play cards.
    private bool cardPhase = false;

    // Turn indicator.
    [SerializeField] private Image turnIndicator;
    [SerializeField] private Sprite attIndicatorSprite;
    [SerializeField] private Sprite defIndicatorSprite;
    // Attack indicator.
    [SerializeField] private Image attackIndicator;
    [SerializeField] private Sprite playerIndicatorSprite;
    [SerializeField] private Sprite enemyIndicatorSprite;
    //Timer
    [SerializeField] private RectTransform timerArrow;
    [SerializeField] private RectTransform clock;

    // Event when a card is pressed down.
    public delegate void OnTimeUp();
    public static event OnTimeUp TimeUp;
    
    // Time each turn.
    [SerializeField] private float defaultTurnTime = 5;
    // Time that is being count down.
    [SerializeField] private float currentTime;

    // IEnumerators.
    private IEnumerator countDown;
    private IEnumerator animationTime;

    // Scripts.
    [SerializeField] private SetSequence sequence;
    [SerializeField] private CardHolder cardHolder;
    [SerializeField] private Player player;
    [SerializeField] private Enemy enemy;

    //Audio
    [SerializeField] private AudioSource EnemySound;
    [SerializeField] private AudioSource PlayerSound;
    public int turnDamage = 0;

    Sequence tweenClockSequence;

    void Start ()
    {
        // Assign IEnumerators.
        countDown = CountDown();
        animationTime = AnimationTime();

        // Set default values.
        currentTime = defaultTurnTime;

        tweenClockSequence = DOTween.Sequence();

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
        turnDamage = 0;
        StopCoroutine(animationTime);

        if (attackTurn == true)
        {
            turnIndicator.sprite = defIndicatorSprite;
            Sequence tweenSequence = DOTween.Sequence();
            tweenSequence.Append(turnIndicator.transform.DOLocalRotate(new Vector3(0, 0, 720), 0.5f, RotateMode.FastBeyond360).SetEase(Ease.OutQuad));

            cardPhase = true;
            attackTurn = false;
            defendTurn = true;

            StartCardPhase();
        }
        else
        {
            turnIndicator.sprite = attIndicatorSprite;
            Sequence tweenSequence = DOTween.Sequence();
            tweenSequence.Append(turnIndicator.transform.DOLocalRotate(new Vector3(0, 0, 720), 0.5f, RotateMode.FastBeyond360).SetEase(Ease.OutQuad));

            cardPhase = true;
            defendTurn = false;
            attackTurn = true;

            StartCardPhase();
        }

        // Default animations.
        player.IdleAnim();
        enemy.IdleAnim();
    }

    /// <summary>
    /// Stop the cardphase and begin the attack animation.
    /// </summary>
    void EndTurn()
    {
        tweenClockSequence.Kill();
        timerArrow.transform.rotation = new Quaternion(0, 0, 0, 0);
        
        Debug.Log("end card select");

        cardPhase = false;
        cardHolder.HideCards();

        animationTime = AnimationTime();
        StartCoroutine(animationTime);
    }

    /// <summary>
    /// Generates a new sequence, displays the cards en starts the countdown.
    /// </summary>
    void StartCardPhase()
    {
        Debug.Log("Start CardPhase");
        sequence.GenerateSequence();
        cardHolder.GetCards();

        // Start the countdown.
        countDown = CountDown();
        StartCoroutine(countDown);
    }

    // Checks time for clock.
    IEnumerator CountDown()
    {
        currentTime = defaultTurnTime;

        tweenClockSequence.Append(timerArrow.DOLocalRotate(new Vector3(0, 0, (timerArrow.rotation.z - 360)), defaultTurnTime, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        //tweenClockSequence.Append(clock.DOLocalRotate(new Vector3(0, 0, (timerArrow.rotation.z - 10)), 0.04f, RotateMode.Fast).SetLoops(20, LoopType.Yoyo).SetSpeedBased(true));
        
        while (true)
        {
            yield return new WaitForSeconds(1);
            currentTime--;

            // Check if the time is up.
            if (currentTime == 0)
            {
                if (TimeUp != null)
                {
                    TimeUp();
                }
                StopCoroutine(countDown);
                EndTurn();
            }
        }
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
    /// <summary>
    /// Regulates the time for the animation
    /// </summary>
    /// <returns></returns>
    IEnumerator AnimationTime()
    {
        print("Animating");
		
		DisplayAttackScreen();
        yield return new WaitForSeconds(4.5f);
        Debug.Log("end animation");
        CheckTurn();
    }
    /// <summary>
    /// Animates the attack animation of the player and enemy.
    /// </summary>
    void DisplayAttackScreen()
    {
        attackIndicator.gameObject.SetActive(true);
        float endPos = 0;

        if (attackTurn)
        {
            attackIndicator.sprite = playerIndicatorSprite;
            attackIndicator.rectTransform.localPosition = new Vector2(-1080, 17.5f);
            endPos = 1080;
        }
        else
        {
            attackIndicator.sprite = enemyIndicatorSprite;
            attackIndicator.rectTransform.localPosition = new Vector2(1080, 17.5f);
            endPos = -1080;
        }

        Sequence indicatorSequence = DOTween.Sequence();
        indicatorSequence.SetDelay(0.5f);
        indicatorSequence.Append(attackIndicator.rectTransform.DOLocalMoveX(0, 1.5f).SetEase(Ease.OutBounce));
        indicatorSequence.PrependInterval(0.5f);
        indicatorSequence.Append(attackIndicator.rectTransform.DOLocalMoveX(endPos, 1.5f).SetEase(Ease.InOutQuart));

        Sequence damageSequence = DOTween.Sequence();
        damageSequence.SetDelay(1.5f);
        damageSequence.AppendCallback(SetTurnDamage);
    }

    /// <summary>
    /// Sets the amount of damage done or blocked.
    /// </summary>
    void SetTurnDamage()
    {
        if (attackTurn)
        {
            enemy.DoDamage(turnDamage);
            player.AttackAnim();
            enemy.HurtAnim();
			
            PlayerSound.Play();
            //play magic sound
        }
        else if (defendTurn)
        {
            player.DoDamage((sequence.SetAmount * 10) - turnDamage);
            enemy.AttackAnim();
            player.HurtAnim();
			
			
            EnemySound.Play();
            //player hit sound
        }
    }
}
