using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSequence : MonoBehaviour
{
    // All available cards.
    [SerializeField] private List<Set> sets = new List<Set>();

    // How many sets are made in a sequence. (Default to 3)
    [SerializeField] private int setAmount = 3;
    public int SetAmount
    {
        get { return setAmount; }
    }

    // The current sequence.
    [SerializeField] private List<Set> currentSequence = new List<Set>();
    public List<Set> CurrentSequence
    {
        get { return currentSequence; }
    }

    // The sequence we are currently at.
    private int currentSequenceNum = 0;
    public int CurrentSequenceNum
    {
        get { return currentSequenceNum; }
        set { currentSequenceNum = value; }
    }

    // Copy of all possible sets to prevent duplicate sets in the sequence.
    [SerializeField] private List<Set> setCopy;

    /// <summary>
    /// Create the sequence of sets to play.
    /// </summary>
    public void GenerateSequence()
    {
        // Reset the sequence.
        ClearSequence();

        // Make the sequence.
        setCopy = new List<Set>(sets);
        for (int i = 0; i < setAmount; i++)
        {
            // Get the random set from the copy list.
            int rndSet = Random.Range(0, setCopy.Count);
            // Spawn the set.
            Set sequenceSet = Instantiate(setCopy[rndSet]) as Set;
            sequenceSet.transform.parent = transform;

            // Add it to the list of all the sequences.
            currentSequence.Add(sequenceSet);
            // Remove it from the copylist of all possible sets so we wont spawn a duplicate set.
            setCopy.RemoveAt(rndSet);
        }

        AllignSequence();
    }
    
    /// <summary>
    /// Puts all sets in a row by order.
    /// </summary>
    private void AllignSequence()
    {
        for (int i = currentSequence.Count; i --> 0;)
        {
            currentSequence[i].transform.position = new Vector2(100 + (i * 175), 250);
            currentSequence[i].transform.localScale = new Vector2(0.6f, 0.6f);
        }
    }

    /// <summary>
    /// Clear the sequence list and remove all the sprites.
    /// </summary>
    private void ClearSequence()
    {
        for (int i = 0; i < currentSequence.Count; i++)
        {
            Destroy(currentSequence[i].gameObject);
        }

        currentSequence.Clear();
        currentSequenceNum = 0;
    }
}