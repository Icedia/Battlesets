using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    // All available cards.
    [SerializeField] private List<Card> cards = new List<Card>();

    // The cards in the players hand.
    [SerializeField] private List<Card> handCards = new List<Card>();

    // Amount of cards we will instantiate.
    [SerializeField] private int cardAmount = 0;
    
    // Use this for initialization
    void Start ()
    {
        GetCards();

        Card.CardDropped += AllignCards;
    }

    void GetCards()
    {
        // Check if we have cards in our list.
        if (cards.Count == 0)
        {
            Debug.LogError("Error! Please fill the list with cards");
            return;
        }

        // Shuffle our hand with cards.
        for (int i = 0; i < cardAmount; i++)
        {
            Card card = Instantiate(cards[i]) as Card;
            handCards.Add(card);
            card.transform.parent = transform;
            card.BackgroundRenderer.sortingOrder = i;
            card.SymbolRenderer.sortingOrder = i + 1;
        }

        // Allign all cards when received.
        AllignCards();
    }

    void AllignCards()
    {
        float cardHalfWidth = cards[0].BackgroundRenderer.bounds.size.x / 2;
        float startPos = (cardHalfWidth * (handCards.Count - 1) / 2);


        float totalTwist = -45f;
        float startTwist = -1f * (totalTwist / 2f);
        float twistPerCard = totalTwist / handCards.Count;


        // Center the cards in the hand and put offset between each card.
        for (int i = 0; i < handCards.Count; i++)
        {
            float twistForThisCard = startTwist + (i * twistPerCard);
            handCards[i].transform.eulerAngles = new Vector3(0, 0, twistForThisCard);
            handCards[i].transform.position = new Vector2(-startPos + i * cardHalfWidth, transform.position.y);
        }
    }

    void OnDestroy()
    {
        Card.CardDropped -= AllignCards;
    }
}
