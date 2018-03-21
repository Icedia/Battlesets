using UnityEngine;

public class Set : MonoBehaviour
{
    public enum Symbol { air, earth, fire, water, soul };
    public enum SymbolColor { red, green, blue };

    // Assign in the inspector which symbol/color this set should have.
    [SerializeField] private Symbol symbolSprite;
    [SerializeField] private SymbolColor symbolClr;

    public Symbol SymbolSprite
    {
        get { return symbolSprite; }
    }
    public SymbolColor SymbolClr
    {
        get { return symbolClr; }
    }
}
