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
    //value used to end the game
    [SerializeField] private GameObject sequence;
    [SerializeField] private GameObject PlayAgain;

    // Spriterenderer 
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Sprites
    [SerializeField] private Sprite enemyIdle;
    [SerializeField] private Sprite enemyHurt;
    [SerializeField] private Sprite enemyAttack;
    [SerializeField] private Transform victoryScreen;

    //sets enemy health too 100
    void Start ()
    {
        Health(400); 
	}

    //checkes if the game is won
    void Update()
    {
        Victory();
    }

    //assignes the idle animation
    public void IdleAnim()
    {
        spriteRenderer.sprite = enemyIdle;
    }

    //assignes the hurt animation
    public void HurtAnim()
    {
        spriteRenderer.sprite = enemyHurt;
    }

    //assignes the attack sprite
    public void AttackAnim()
    {
        spriteRenderer.sprite = enemyAttack;
    }

    //assignes the enemy health
    public void Health(float health)
    {
        enemyHealth = health;
    }

    //checks if the game is won
    private void Victory()
    {
        if(enemyHealth <= 0)
        {
            //stop game and show screen
            Sequence victoryTween = DOTween.Sequence();
            victoryTween.SetDelay(3f);
            victoryTween.Append(victoryScreen.DOMoveY(440, 1f));
            sequence.SetActive(false);
            PlayAgain.SetActive(true);

        }
    }

    //calculates how much damage will be done
    public void DoDamage(int damage)
    {
        enemyHealth -= damage;
        enemyHealthBar.value = enemyHealth;
        transform.DOShakePosition(0.75f, 5, 50, 90);
    }
}
