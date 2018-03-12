using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private enum Symbol { square, triangle, circle, hexagon, pentagon };
    private enum Color { red, green, blue };

    [SerializeField] private Symbol symbol;
    [SerializeField] private Color color;

    [SerializeField] private Sprite cardSprite;
    public Sprite CardSprite
    {
        get { return cardSprite; }
        set { cardSprite = value; }
    }

    void Awake()
    {
        cardSprite = GetComponent<SpriteRenderer>().sprite;
    }

    void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().sortingLayerName = "HighlightCard";
    }

    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().sortingLayerName = "Card";
    }
}
