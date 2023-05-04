using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class CompareSamples : MonoBehaviour
{
    //index and the intensity value
    PriorityQueue<int,float>  pq1 = new PriorityQueue<int,float>();

    //public float to view values
    public float ValueWindow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*since priority queue is always ascending, but we intend on picking the top 10 bands
         * so we can instead find the highest value and then enqueue by subtracting that highest 
         * with the rest of the bands so that the previously highest intensities will now be the 
         * lowest and so on
         */

        //does this maximum technique work?
        float maxVal = Mathf.Max(Audio._requiredBands);
        ValueWindow = maxVal; //unneccesary variable tho

        for  (int i = 0; i < 32; i++)
        {
            pq1.Enqueue(i, Audio._requiredBands[i]);
        }
        Compare();
    }
    void Compare()
    {
        for (int i = 0; i < 32; i++)
        {
            pq1.Dequeue();
        }
    }
}
