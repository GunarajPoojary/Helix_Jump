using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceInstantiator : MonoBehaviour
{
    List<int> stickySlices; // List to keep track of which slices are sticky

    [Header("Shaft Prefab")]
    [SerializeField] Transform shaft; // The parent object where slices will be instantiated

    [SerializeField] GameObject scoreSensor; // The sensor object for scoring

    [Space(20), Header("Slice Prefabs")]
    [SerializeField] GameObject normalSlice; // Normal slice prefab
    [SerializeField] GameObject stickySlice; // Sticky slice prefab

    // Start is called before the first frame update
    void Start()
    {
        stickySlices = new List<int>(); // Initialize the list to keep track of sticky slices

        // Loop through the rows where slices will be instantiated
        for (int i = 10; i >= -10; i--)
        {
            int skipSlot = Random.Range(1, 8); // Randomly choose a slot to skip

            // Add two random slots to the list of sticky slices
            stickySlices.Add(Random.Range(1, 8));
            stickySlices.Add(Random.Range(1, 8));

            // Ensure the sticky slices and skip slot are not the same
            while (stickySlices[0] == skipSlot || stickySlices[1] == skipSlot || stickySlices[0] == stickySlices[1])
            {
                stickySlices[0] = Random.Range(1, 8);
                stickySlices[1] = Random.Range(1, 8);
                skipSlot = Random.Range(1, 8);
            }

            // Instantiate the score sensor at the current row
            Instantiate(scoreSensor, new Vector3(0, i, 0), Quaternion.identity);

            // Loop through the slots to instantiate slices
            for (int k = 1; k <= 8; k++)
            {
                // Skip the slot if it's the skip slot
                if (k == skipSlot) continue;

                // Instantiate a sticky slice if the slot matches one in the sticky slices list
                if (k == stickySlices[0])
                    Instantiate(stickySlice, new Vector3(0, i, 0), Quaternion.Euler(0, k * 45, 90)).transform.SetParent(shaft);
                else if (k == stickySlices[1])
                    Instantiate(stickySlice, new Vector3(0, i, 0), Quaternion.Euler(0, k * 45, 90)).transform.SetParent(shaft);
                else // Otherwise, instantiate a normal slice
                    Instantiate(normalSlice, new Vector3(0, i, 0), Quaternion.Euler(0, k * 45, 90)).transform.SetParent(shaft);
            }
            stickySlices.Clear(); // Clear the list of sticky slices for the next row
        }
    }
}
