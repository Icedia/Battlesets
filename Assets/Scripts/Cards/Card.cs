using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    // Event when a card is let go.
    public delegate void OnCardDropped();
    public static event OnCardDropped CardDropped;
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
            backgroundRenderer.sortingLayerName = "HighlightCard";
            symbolRenderer.sortingLayerName = "HighlightCard";
        }
    }

    // Remove the highlight from the card.
    void OnMouseExit()
    {
        if (!isDragging)
        {
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
        backgroundRenderer.sortingLayerName = "HighlightCard";
        symbolRenderer.sortingLayerName = "HighlightCard";
    }

    // Stop dragging and reallign our cards.
    void OnMouseUp()
    {
        // Call allignment
        isDragging = false;
        if (CardDropped != null)
        {
            CardDropped();
        }

        if (dragDist >= 75f)
        {
            dragDist = 0;
            if (CardPlaced != null)
            {
                CardPlaced(this);
            }
            print("Card placed!");
        }
    }
}
