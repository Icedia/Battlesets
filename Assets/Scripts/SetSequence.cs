using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SetSequence : MonoBehaviour {

	[SerializeField] private List<Sprite> currentSequence = new List<Sprite>();
    public List<Sprite> CurrentSequence
    {
        get { return currentSequence; }
    }

    [SerializeField] private Sprite[] shapes;
    [SerializeField] private SpriteRenderer[] seq;

    public enum Symbol { air, earth, fire, water, soul };
    public enum SymbolColor { red, green, blue };

    [SerializeField] public Symbol[] symbolSprite;
    [SerializeField] public SymbolColor[] symbolColor;

    public int currentSequenceNum = 0;

    [SerializeField] private Sprite[] currentSpriteArray;
    [SerializeField] private Sprite temp;

    public void Generate()
    {
        currentSequenceNum = 0;

        for (int i = 0; i < shapes.Length; i++)
        {
            currentSequence.Add(shapes[i]);
        }

        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < seq.Length; i++)
            {
                GetRandomSprite();
                seq[i].sprite = temp;
                currentSpriteArray[i] = temp;
                symbolSprite[i] = CheckSymbol(currentSpriteArray[i].name);
                seq[i].color = SetColor(symbolColor[j]);
                symbolColor[j] = (SymbolColor)Random.Range(0, symbolColor.Length);
            }
        }

        currentSequence.Clear();
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
            case ("Rune_Air"):
                return Symbol.air;
            case ("Rune_Earth"):
                return Symbol.earth;
            case ("Rune_Fire"):
                return Symbol.fire;
            case ("Rune_Water"):
                return Symbol.water;
            default:
                return Symbol.soul;
        }
    }

    private void GetRandomSprite()
    {
        temp = currentSequence[Random.Range(0, currentSequence.Count)];
        currentSequence.Remove(temp);
    }
}