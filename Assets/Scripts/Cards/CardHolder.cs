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

    /// <summary>
    /// Get playable cards in your hand.
    /// </summary>
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
                if (cards[i].symbolSprite.ToString().Equals(setSequence.CurrentSequence[j].SymbolSprite.ToString()) &&
                    cards[i].symbolColor.ToString().Equals(setSequence.CurrentSequence[j].SymbolClr.ToString()))
                {
                    Card card = Instantiate(cards[i]) as Card;
                    handCards.Add(card);
                    card.transform.parent = transform;
                }
            }
        }
        
        // Instantiate all hand cards.
        for (int i = 0; i < cardAmount-2; i++)
        {
            Card card = Instantiate(cards[Random.Range(0, cards.Count)]) as Card;
            handCards.Add(card);
            card.transform.parent = transform;
        }

        // Shuffle our hand with cards.
        ShuffleHand();

        // Allign all cards when received.
        AllignCards();
    }

    /// <summary>
    /// Randomize the order of the hand cards.
    /// </summary>
    private void ShuffleHand()
    {
        List<Card> randomizedList = new List<Card>();
        while (handCards.Count > 0)
        {
            int index = Random.Range(0, handCards.Count); //pick a random item from the master list
            randomizedList.Add(handCards[index]); //place it at the end of the randomized list
            handCards.RemoveAt(index);
        }

        handCards = randomizedList;
    }

    /// <summary>
    /// Destroy all cards in the hand and clear the list. 
    /// </summary>
    public void HideCards()
    {
        for (int i = 0; i < handCards.Count; i++)
        {
            Destroy(handCards[i].gameObject);
        }
        handCards.Clear();
    }

    /// <summary>
    /// Makes all cards in the hand alligned propperly.
    /// </summary>
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
        for (int i = 0; i < handCards.Count; i++)
        {
            handCards[i].BackgroundRenderer.sortingOrder = i;
            handCards[i].SymbolRenderer.sortingOrder = i+1;
        }
    }

    /// <summary>
    /// Gets called when the player sets a card.
    /// </summary>
    /// <param name="card">The played card.</param>
    void CardSet(Card card)
    {
        // Compare the placed card with the current sequence set.
        if (card.symbolSprite.ToString().Equals(setSequence.CurrentSequence[setSequence.CurrentSequenceNum].SymbolSprite.ToString()) &&
           card.symbolColor.ToString().Equals(setSequence.CurrentSequence[setSequence.CurrentSequenceNum].SymbolClr.ToString()))
        {
            // Correct card.
            print("Correct!");

            // Destroy and remove the card.
            handCards.Remove(card);
            Destroy(card.gameObject);
            AllignCards();
            
            turnManager.turnDamage += 10;
        }
        else
        {
            // Wrong card.
            print("Wrong!");
        }

        setSequence.CurrentSequence[setSequence.CurrentSequenceNum].gameObject.SetActive(false);
        setSequence.CurrentSequenceNum++;
    }

    /// <summary>
    /// Unsubscribe from every delegate.
    /// </summary>
    void OnDestroy()
    {
        Card.CardDropped -= AllignCards;
        Card.CardPlaced -= CardSet;
    }
}
