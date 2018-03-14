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
    [SerializeField]private Color[] red;

    private Color currentColor1;
    private Color currentColor2;
    private Color currentColor3;
    private Sprite currentSprite1;
    private Sprite currentSprite2;
    private Sprite currentSprite3;
    private Sprite temp;
	
	void Update ()
	{
        Generate();
	}

    public void Generate()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            for (int i = 0; i < shapes.Length; i++)
            {
                currentSequence.Add(shapes[i]);
            }
            GetRandomSprite();
            one.sprite = temp;
            currentSprite1 = temp;
            currentColor1 = red[Random.Range(0, red.Length)];
            one.color = currentColor1;

            GetRandomSprite();
            two.sprite = temp;
            currentSprite2 = temp;
            currentColor2 = red[Random.Range(0, red.Length)];
            two.color = currentColor2;

            GetRandomSprite();
            three.sprite = temp;
            currentSprite3 = temp;
            currentColor3 = red[Random.Range(0, red.Length)];
            three.color = currentColor3;

            currentSequence.Clear(); // save these values
        }
    }
    private void GetRandomSprite()
    {
        temp = currentSequence[Random.Range(0, currentSequence.Count)];
        currentSequence.Remove(temp);
    }
}