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

    // Sequence.
    [SerializeField] private SetSequence setSequence;

    // Turnmanager.
    [SerializeField] private TurnManager turnManager;

    // Use this for initialization
    void Start ()
    {
        Card.CardDropped += AllignCards;
        Card.CardPlaced += CardSet;
    }

    public void GetCards()
    {
        // Check if we have cards in our list.
        if (cards.Count == 0)
        {
            Debug.LogError("Error! Please fill the list with cards");
            return;
        }

        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].symbolSprite.ToString() == setSequence.symbolSprite[j].ToString() &&
                    cards[i].symbolColor.ToString() == setSequence.symbolColor[j].ToString())
                {
                    Card card = Instantiate(cards[i]) as Card;
                    handCards.Add(card);
                    card.transform.parent = transform;
                }
            }
        }

        // Shuffle our hand with cards.
        for (int i = 0; i < cardAmount-2; i++)
        {
            Card card = Instantiate(cards[Random.Range(0, cards.Count)]) as Card;
            handCards.Add(card);
            card.transform.parent = transform;
        }

        // Allign all cards when received.
        AllignCards();
    }

    public void HideCards()
    {
        for (int i = 0; i < handCards.Count; i++)
        {
            Destroy(handCards[i].gameObject);
        }
        handCards.Clear();
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

        // Set rendering order.
        for (int i = 0; i < cardAmount; i++)
        {
            handCards[i].BackgroundRenderer.sortingOrder = i;
            handCards[i].SymbolRenderer.sortingOrder = i;
        }
    }

    void CardSet(Card card)
    {
        // Compare the placed card with the current sequence set.
        if (card.symbolSprite.ToString() == setSequence.symbolSprite[setSequence.currentSequenceNum].ToString() &&
           card.symbolColor.ToString() == setSequence.symbolColor[setSequence.currentSequenceNum].ToString())
        {
            // Correct card.
            print("Correct!");
            turnManager.turnDamage += 10;
        }
        else
        {
            // Wrong card.
            print("Wrong!");
        }

        //Destroy(card);
        setSequence.currentSequenceNum++;
    }

    void OnDestroy()
    {
        Card.CardDropped -= AllignCards;
        Card.CardPlaced -= CardSet;
    }
}
