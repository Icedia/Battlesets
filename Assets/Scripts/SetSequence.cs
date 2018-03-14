using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SetSequence : MonoBehaviour {

	private List<Sprite> currentSequence = new List<Sprite>();
	private int sequenceAmount = 3;
    [SerializeField] private Sprite[] shapes;
    [SerializeField] private SpriteRenderer one;
    [SerializeField] private SpriteRenderer two;
    [SerializeField] private SpriteRenderer three;

    public enum Symbol { square, triangle, circle, hexagon, pentagon };
    public enum SymbolColor { red, green, blue };

    [SerializeField] public Symbol[] symbolSprite;
    [SerializeField] public SymbolColor[] symbolColor;

    public int currentSequenceNum = 0;

    [SerializeField] private Sprite currentSprite1;
    [SerializeField] private Sprite currentSprite2;
    [SerializeField] private Sprite currentSprite3;
    [SerializeField] private Sprite temp;

    public void Generate()
    {
        currentSequenceNum = 0;

        for (int i = 0; i < shapes.Length; i++)
        {
            currentSequence.Add(shapes[i]);
        }

        GetRandomSprite();
        one.sprite = temp;
        currentSprite1 = temp;
        symbolSprite[0] = CheckSymbol(currentSprite1.name);
        symbolColor[0] = (SymbolColor)Random.Range(0, 3);
        one.color = SetColor(symbolColor[0]);

        GetRandomSprite();
        two.sprite = temp;
        currentSprite2 = temp;
        symbolSprite[1] = CheckSymbol(currentSprite2.name);
        symbolColor[1] = (SymbolColor)Random.Range(0, 3);
        two.color = SetColor(symbolColor[1]);

        GetRandomSprite();
        three.sprite = temp;
        currentSprite3 = temp;
        symbolSprite[2] = CheckSymbol(currentSprite3.name);
        symbolColor[2] = (SymbolColor)Random.Range(0, 3);
        three.color = SetColor(symbolColor[2]);

        currentSequence.Clear(); // save these values
    }

    Color SetColor(SymbolColor symbolColor)
    {
        if (symbolColor == SymbolColor.red)
        {
            return Color.red;
        }
        else if (symbolColor == SymbolColor.green)
        {
            return Color.green;
        }
        else
        {
            return Color.blue;
        }
    }

    Symbol CheckSymbol(string spriteName)
    {
        switch (spriteName)
        {
            case ("Circle"):
                return Symbol.circle;
            case ("Triangle"):
                return Symbol.triangle;
            case ("Square"):
                return Symbol.square;
            case ("Hexagon"):
                return Symbol.hexagon;
            default:
                return Symbol.pentagon;
        }
    }

    private void GetRandomSprite()
    {
        temp = currentSequence[Random.Range(0, currentSequence.Count)];
        currentSequence.Remove(temp);
    }
}