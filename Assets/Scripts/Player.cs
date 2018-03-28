using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class Player : MonoBehaviour, IHealth<float>
{
    // Holds value to end the game
    [SerializeField] private float playerHealth;
    [SerializeField] private Slider playerHealthBar;
    //use too end the game
    [SerializeField] private GameObject sequence;
    [SerializeField] private GameObject PlayAgain;

    // Spriterenderer 
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Sprites
    [SerializeField] private Sprite playerIdle;
    [SerializeField] private Sprite playerHurt;
    [SerializeField] private Sprite playerAttack;
    [SerializeField] private Transform defeatScreen;
    //Sets health of player
	void Start ()
    {
        Health(100);
	}
    //Checks if the game ends
    void Update()
    {
        Defeat();
    }
    //assignes idle animation
    public void IdleAnim()
    {
        spriteRenderer.sprite = playerIdle;
    }
    //assignes hurt animation
    public void HurtAnim()
    {
        spriteRenderer.sprite = playerHurt;
    }
    //assignes attack animation
    public void AttackAnim()
    {
        spriteRenderer.sprite = playerAttack;
    }
    //assignes health
    public void Health(float health)
    {
        playerHealth = health;
    }
    //checks if the game ends in a defeat
    private void Defeat()
    {
        if (playerHealth <= 0)
        {
            Sequence defeatTween = DOTween.Sequence();
            defeatTween.SetDelay(3f);
            defeatTween.Append(defeatScreen.DOMoveY(440, 1.5f));
            sequence.SetActive(false);
            PlayAgain.SetActive(true);
        }
    }
    //checks how much damage will be done
    public void DoDamage(int damage)
    {
        playerHealth -= damage;
        playerHealthBar.value = playerHealth;
        transform.DOShakePosition(0.75f, 5, 50, 90);
    }
}
