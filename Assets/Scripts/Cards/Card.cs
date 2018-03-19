using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public delegate void OnCardDropped();
    public static event OnCardDropped CardDropped;

    public delegate void OnCardPlaced(Card card);
    public static event OnCardPlaced CardPlaced;

    static bool isDragging = false;

    [SerializeField] private float dragDist;

    public enum Symbol { air, earth, fire, water, soul };
    public enum SymbolColor { red, green, blue };

    [SerializeField] public Symbol symbolSprite;
    [SerializeField] public SymbolColor symbolColor;

    [SerializeField] private SpriteRenderer backgroundRenderer;
    public SpriteRenderer BackgroundRenderer
    {
        get { return backgroundRenderer; }
        set { backgroundRenderer = value; }
    }
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

        if (dragDist >= 100f)
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
