using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class CompareAll : MonoBehaviour
{
    //index and the intensity value
    PriorityQueue<int, float> pq1 = new PriorityQueue<int, float>();

    //public float to view values
    public float MaximumSignal;

    //Top N values
    static int N = 64;

    //64 values should suffice, we can always go higher if we need more accuracy
    public int[] PickedIndex = new int[N];

    //temporary float to reverse
    float reversedVal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*since priority queue is always ascending, but we intend on picking the top N bands
         * so we can instead find the highest value and then enqueue by subtracting that highest 
         * with the rest of the bands so that the previously highest intensities will now be the 
         * lowest and so on
         */

        //IF THE SIGNAL IS DEMANDING VERY PRECISE FREQUENCES THEN REDUCE THE SAMPLE SIZE.

        //does this maximum technique work?
        float maxVal = Mathf.Max(Audio._samples);
        MaximumSignal= maxVal; //unneccesary variable tho

        for (int i = 0; i < N; i++)
        {
            //achieve priority reversal and enqueue in the same loop
            reversedVal = maxVal - Audio._samples[i];

            //no need to worry about negative values because the lowest it can go is 0
            pq1.Enqueue(i, reversedVal);
        }
        Compare();
    }
    void Compare()
    {
        for (int i = 0; i < N-1; i++)
        {
            //might be error in this segment
            pq1.TryDequeue(out int a, out float x);
            //PickedIndex[i] = a;
            if (PickedIndex != null)
            {
                PickedIndex[i] = a;

            }
            //Debug
            //UnityEngine.Debug.Log(i.ToString() + " " + a.ToString());
        }
    }
}
