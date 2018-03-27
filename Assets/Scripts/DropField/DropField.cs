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

    // Checks if we are within the dropfield area.
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

    // Call the IEnumerator to display the drop field.
    void ShowDropField()
    {
        animateDropField = AnimateDropField();
        StartCoroutine(animateDropField);
    }
    
    // Call the IEnumerator to hide the drop field.
    void HideDropField()
    {
        if (animateDropField != null)
        {
            StopCoroutine(animateDropField);
        }

        fadeOutDropField = FadeOutDropField();
        StartCoroutine(fadeOutDropField);
    }

    // Pulse the alpha of the drop field.
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

    // Fade the drop field out.
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
