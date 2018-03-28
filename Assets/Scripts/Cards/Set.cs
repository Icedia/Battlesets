using UnityEngine;

public class Set : MonoBehaviour
{
    public enum Symbol { air, earth, fire, water, soul };
    public enum SymbolColor { red, green, blue };

    // Assign in the inspector which symbol/color this set should have.
    [SerializeField] private Symbol symbolSprite;
    [SerializeField] private SymbolColor symbolClr;

    [SerializeField] private SpriteRenderer resultSpriteRnd;
    [SerializeField] private Sprite correct;
    [SerializeField] private Sprite inCorrect;

    public Symbol SymbolSprite
    {
        get { return symbolSprite; }
    }
    public SymbolColor SymbolClr
    {
        get { return symbolClr; }
    }

    /// <summary>
    /// Displays when the made set correct or incorrect is.
    /// </summary>
    /// <param name="isCorrect">Which sprite to display</param>
    public void SetResult(bool isCorrect)
    {
        resultSpriteRnd.gameObject.SetActive(true);

        if (isCorrect)
        {
            resultSpriteRnd.sprite = correct;
        }
        else
        {
            resultSpriteRnd.sprite = inCorrect;
        }
    }
}
