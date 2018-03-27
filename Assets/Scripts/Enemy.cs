using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class Enemy : MonoBehaviour, IHealth<float>
{
    [SerializeField] private float enemyHealth;
    [SerializeField] private Slider enemyHealthBar;

    // Spriterenderer 
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Sprites
    [SerializeField] private Sprite enemyIdle;
    [SerializeField] private Sprite enemyHurt;
    [SerializeField] private Sprite enemyAttack;

    void Start ()
    {
        Health(100); // set health for the player
	}

    void Update()
    {
        Victory();
    }

    public void IdleAnim()
    {
        spriteRenderer.sprite = enemyIdle;
    }

    public void HurtAnim()
    {
        spriteRenderer.sprite = enemyHurt;
    }

    public void AttackAnim()
    {
        spriteRenderer.sprite = enemyAttack;
    }

    public void Health(float health)
    {
        enemyHealth = health;
    }

    private void Victory()
    {
        if(enemyHealth <= 0)
        {
            SceneManager.LoadScene("Victory");
        }
    }

    public void DoDamage(int damage)
    {
        enemyHealth -= damage;
        enemyHealthBar.value = enemyHealth;
        transform.DOShakePosition(0.75f, 5, 50, 90);
    }
}
