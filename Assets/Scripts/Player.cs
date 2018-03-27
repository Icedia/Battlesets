using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class Player : MonoBehaviour, IHealth<float>
{
    [SerializeField] private float playerHealth;
    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private GameObject sequence;
    [SerializeField] private GameObject PlayAgain;

    // Spriterenderer 
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Sprites
    [SerializeField] private Sprite playerIdle;
    [SerializeField] private Sprite playerHurt;
    [SerializeField] private Sprite playerAttack;
    [SerializeField] private Transform defeatScreen;

	void Start ()
    {
        Health(10);
	}

    void Update()
    {
        Defeat();
    }

    public void IdleAnim()
    {
        spriteRenderer.sprite = playerIdle;
    }

    public void HurtAnim()
    {
        spriteRenderer.sprite = playerHurt;
    }

    public void AttackAnim()
    {
        spriteRenderer.sprite = playerAttack;
    }

    public void Health(float health)
    {
        playerHealth = health;
    }

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
        //if player health <=0 change scene
    }

    public void DoDamage(int damage)
    {
        playerHealth -= damage;
        playerHealthBar.value = playerHealth;
        transform.DOShakePosition(0.75f, 5, 50, 90);
    }
}
