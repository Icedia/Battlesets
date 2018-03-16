﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SetSequence : MonoBehaviour {

	private List<Sprite> currentSequence = new List<Sprite>();
    [SerializeField] private Sprite[] shapes;
    [SerializeField] private SpriteRenderer[] seq;

    public enum Symbol { air, earth, fire, water, soul };
    public enum SymbolColor { red, green, blue };

    [SerializeField] public Symbol[] symbolSprite;
    [SerializeField] public SymbolColor[] symbolColor;

    public int currentSequenceNum = 0;

    [SerializeField] private Sprite currentSprite1;
    [SerializeField] private Sprite currentSprite2;
    [SerializeField] private Sprite currentSprite3;
    [SerializeField] private Sprite[] currentSpriteArray;
    [SerializeField] private Sprite temp;
    private int Y;

    public void Generate()
    {
        currentSequenceNum = 0;

        for (int i = 0; i < shapes.Length; i++)
        {
            currentSequence.Add(shapes[i]);
        }

        for (int i = 0; i < seq.Length; i++)
        {
            Y++;
            GetRandomSprite();
            seq[i].sprite = temp;
            currentSpriteArray[i] = temp;
            symbolSprite[i] = CheckSymbol(currentSpriteArray[i].name);
            seq[i].color = SetColor(symbolColor[Y]);
            symbolColor[Y] = (SymbolColor)Random.Range(0, symbolColor.Length);
            if (Y != 3)
            {
                Y = 0;
            }



        }
        /* GetRandomSprite();
         seq[1].sprite = temp;
         currentSprite2 = temp;
         symbolSprite[1] = CheckSymbol(currentSprite2.name);
         symbolColor[1] = (SymbolColor)Random.Range(0, 3);
         seq[1].color = SetColor(symbolColor[1]);

         GetRandomSprite();
         seq[2].sprite = temp;
         currentSprite3 = temp;
         symbolSprite[2] = CheckSymbol(currentSprite3.name);
         symbolColor[2] = (SymbolColor)Random.Range(0, 3);
         seq[2].color = SetColor(symbolColor[2]);
     */

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