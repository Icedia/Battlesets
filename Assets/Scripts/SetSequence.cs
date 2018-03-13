using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SetSequence : MonoBehaviour {

	private List<Sprite> currentSequence = new List<Sprite>();
	private int sequenceAmount = 3;
    [SerializeField] private Sprite[] colors;
    [SerializeField] private SpriteRenderer one;
    [SerializeField] private SpriteRenderer two;
    [SerializeField] private SpriteRenderer three;
    private Sprite temp;
	
	void Update () 
	{
        Generate();
	}

    private void Generate()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            for (int i = 0; i < colors.Length; i++)
            {
                currentSequence.Add(colors[i]);
            }
            GetRandom();
            one.sprite = temp;

            GetRandom();
            two.sprite = temp;

            GetRandom();
            three.sprite = temp;

            currentSequence.Clear();
        }
    }
    private void GetRandom()
    {
        temp = currentSequence[Random.Range(0, currentSequence.Count)];
        currentSequence.Remove(temp);
    }
}