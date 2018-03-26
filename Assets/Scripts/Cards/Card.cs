using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    // Event when a card is pressed down.
    public delegate void OnCardPressed();
    public static event OnCardPressed CardPressed;
    // Event when a card is let go.
    public delegate void OnCardReset();
    public static event OnCardReset CardReset;
    // Event when a card is let go.
    public delegate void OnCardDropped();
    public static event OnCardDropped CardDropped;
    // Event to check if we can place the card.
    public delegate bool OnCardCheck();
    public static event OnCardCheck CardCheck;
    // Event when a card is played.
    public delegate void OnCardPlaced(Card card);
    public static event OnCardPlaced CardPlaced;

    // Static boolean to keep track of the card that is being dragged.
    static bool isDragging = false;

    // Amount of distance between the mousepointer and the card.
    private float dragDist;

    public enum Symbol { air, earth, fire, water, soul };
    public enum SymbolColor { red, green, blue };

    [SerializeField] public Symbol symbolSprite;
    [SerializeField] public SymbolColor symbolColor;

    // Renderer of our Card Background.
    [SerializeField] private SpriteRenderer backgroundRenderer;
    public SpriteRenderer BackgroundRenderer
    {
        get { return backgroundRenderer; }
        set { backgroundRenderer = value; }
    }

    // Renderer of our Card Symbol.
    [SerializeField] private SpriteRenderer symbolRenderer;
    public SpriteRenderer SymbolRenderer
    {
        get { return symbolRenderer; }
        set { symbolRenderer = value; }
    }
    
    // Highlighting the card when moving over it.
    void OnMouseEnter()
    {
        if (!isDragging)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 65);
            backgroundRenderer.sortingLayerName = "HighlightCard";
            symbolRenderer.sortingLayerName = "HighlightCard";
        }
    }

    // Remove the highlight from the card.
    void OnMouseExit()
    {
        if (!isDragging)
        {
            if (CardReset != null)
            {
                CardReset();
            }
            backgroundRenderer.sortingLayerName = "Card";
            symbolRenderer.sortingLayerName = "Card";
        }
    }

    // Moving the card.
    void OnMouseDrag()
    {
        isDragging = true;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePos, 0.2f);

        dragDist = Vector2.Distance(transform.position, mousePos);
    }

    // Highlighting the card clicking it.
    void OnMouseDown()
    {
        transform.eulerAngles = Vector3.zero;
        if (CardPressed != null)
        {
            CardPressed();
        }
        backgroundRenderer.sortingLayerName = "HighlightCard";
        symbolRenderer.sortingLayerName = "HighlightCard";
    }

    // Stop dragging and reallign our cards.
    void OnMouseUp()
    {
        // Check if we are within the dorpfield.
        if (CardCheck != null)
        {
            if (CardCheck())
            {
                if (CardPlaced != null)
                {
                    CardPlaced(this);
                }
            }
            else
            {
                if (CardReset != null)
                {
                    CardReset();
                }
            }
        }

        // Call allignment
        isDragging = false;
        if (CardDropped != null)
        {
            CardDropped();
        }
    }
}
