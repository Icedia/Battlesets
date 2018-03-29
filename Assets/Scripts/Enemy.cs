using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Enemy : MonoBehaviour, IHealth<float>
{
    //Holds value's of enemies health
    [SerializeField] private float enemyHealth;
    [SerializeField] private Slider enemyHealthBar;

    //Value used to end the game
    [SerializeField] private GameObject sequence;
    [SerializeField] private GameObject menuButton;
    [SerializeField] private GameObject pauseButton;

    // Spriterenderer 
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Sprites
    [SerializeField] private Sprite enemyIdle;
    [SerializeField] private Sprite enemyHurt;
    [SerializeField] private Sprite enemyAttack;
    [SerializeField] private Transform victoryScreen;

    /// <summary>
    /// Sets enemy health to 100.
    /// </summary>
    void Start ()
    {
        Health(400); 
	}

    /// <summary>
    /// checkes if the game is won
    /// </summary>
    void Update()
    {
        Victory();
    }

    /// <summary>
    /// assignes the idle animation
    /// </summary>
    public void IdleAnim()
    {
        spriteRenderer.sprite = enemyIdle;
    }

    /// <summary>
    /// assignes the hurt animation
    /// </summary>
    public void HurtAnim()
    {
        spriteRenderer.sprite = enemyHurt;
    }

    /// <summary>
    /// assignes the attack sprite
    /// </summary>
    public void AttackAnim()
    {
        spriteRenderer.sprite = enemyAttack;
    }

    /// <summary>
    /// assignes the enemy health
    /// </summary>
    /// <param name="health"></param>
    public void Health(float health)
    {
        enemyHealth = health;
    }

    /// <summary>
    ///   checks if the game is won
    /// </summary>
    private void Victory()
    {
        if (enemyHealth <= 0)
        {
            //stop game and show screen
            Sequence victoryTween = DOTween.Sequence();
            victoryTween.SetDelay(3f);
            
            victoryTween.Append(victoryScreen.DOMoveY(Screen.height * 0.55f, 1f).OnComplete(() => menuButton.SetActive(true)));
            sequence.SetActive(false);
            pauseButton.SetActive(false);
        }
    }

    /// <summary>
    /// calculates how much damage will be done
    /// </summary>
    /// <param name="damage"></param>
    public void DoDamage(int damage)
    {
        enemyHealth -= damage;
        enemyHealthBar.value = enemyHealth;
        transform.DOShakePosition(0.75f, 5, 50, 90);
    }
}
