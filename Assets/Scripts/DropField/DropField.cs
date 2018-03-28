using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropField : MonoBehaviour
{
    // Hint where to drop the card.
    [SerializeField] private SpriteRenderer cardDropfield;

    // IEnumerators.
    private IEnumerator animateDropField;
    private IEnumerator fadeOutDropField;

    //Audio
    [SerializeField] private AudioSource CardSounds;
    
    // Use this for initialization
    void Start()
    {
        Card.CardPressed += ShowDropField;
        Card.CardDropped += HideDropField;
        Card.CardCheck += CheckInField;
        TurnManager.TimeUp += HideDropField;
    }
    /// <summary>
    /// Checks if we are within the dropfield area.
    /// </summary>
    /// <returns></returns>
    private bool CheckInField()
    {
        BoxCollider2D col = transform.GetComponent<BoxCollider2D>();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (mousePos.y > col.transform.position.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// Call the IEnumerator to display the drop field.
    /// </summary>
    void ShowDropField()
    {
        animateDropField = AnimateDropField();
        StartCoroutine(animateDropField);
    }
    /// <summary>
    /// Call the IEnumerator to hide the drop field.
    /// </summary>
    void HideDropField()
    {
        if (animateDropField != null)
        {
            StopCoroutine(animateDropField);
        }

        fadeOutDropField = FadeOutDropField();
        StartCoroutine(fadeOutDropField);
    }
    /// <summary>
    /// Pulse the alpha of the drop field.
    /// </summary>
    /// <returns></returns>
    IEnumerator AnimateDropField()
    {
        Color color;
        bool fadeIn = true;
        float alpha = 0;
        float fadeSpeed = 0.03f;

        while (true)
        {
            if (fadeIn)
            {
                alpha += fadeSpeed;
                if (alpha >= 0.75)
                {
                    fadeIn = false;
                }
            }
            else
            {
                alpha -= fadeSpeed;
                if (alpha <= 0)
                {
                    fadeIn = true;
                }
            }

            color = new Color(1, 1, 1, alpha);
            cardDropfield.color = color;
            yield return new WaitForSeconds(0.0175f);
        }
    }
    /// <summary>
    /// Fade the drop field out.
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeOutDropField()
    {
        Color color;
        CardSounds.Play();
        float alpha = cardDropfield.color.a;
        float fadeSpeed = 0.05f;

        while (true)
        {
            alpha -= fadeSpeed;
            color = new Color(1, 1, 1, alpha);
            cardDropfield.color = color;

            if (alpha <= 0)
            {
                StopCoroutine(fadeOutDropField);
            }

            yield return new WaitForSeconds(0.0175f);
        }
    }

    /// <summary>
    /// Unsubscribe from every delegate.
    /// </summary>
    void OnDestroy()
    {
        Card.CardPressed -= ShowDropField;
        Card.CardDropped -= HideDropField;
        Card.CardCheck -= CheckInField;
        TurnManager.TimeUp -= HideDropField;
    }
}
