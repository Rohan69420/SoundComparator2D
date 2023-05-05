using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Utils;

public class CompareAll : MonoBehaviour
{
    //index and the intensity value
    PriorityQueue<int, float> pq1 = new PriorityQueue<int, float>();
    PriorityQueue<int, float> pq2 = new PriorityQueue<int, float>();

    //public float to view values
    public float MaximumSignalOne, MaximumSignalTwo;
    public static float _matchedPercentage;

    //comparison parameter: the indices upto which we intend to compare
    public int CompareParameter;
    public int count;

    //float 
    float maxVal;

    //Top N values
    static int N = 64;

    //64 values should suffice, we can always go higher if we need more accuracy
    public int[] FirstSortedIndex = new int[N];
    public int[] SecondSortedIndex = new int[N];

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

        if (!Audio.paused)
        {
            //does this maximum technique work?
            maxVal = Mathf.Max(Audio._samples);
            MaximumSignalOne = maxVal; //unneccesary variable tho

            //first loop
            for (int i = 0; i < N; i++)
            {
                //achieve priority reversal and enqueue in the same loop
                reversedVal = maxVal - Audio._samples[i];

                //no need to worry about negative values because the lowest it can go is 0
                pq1.Enqueue(i, reversedVal);
            }

        }
        if (!SecondAudio.paused)
        {
            maxVal = Mathf.Max(SecondAudio._secondSamples);
            MaximumSignalTwo = maxVal;
            //second loop
            for (int i = 0; i < N; i++)
            {
                //achieve priority reversal and enqueue in the same loop
                reversedVal = maxVal - SecondAudio._secondSamples[i];

                //no need to worry about negative values because the lowest it can go is 0
                pq2.Enqueue(i, reversedVal);
            }
        }
        if (!Audio.paused || !SecondAudio.paused) //F.X = F 
        {
            ShowOrder();
        }
        
        CalculateSimilarity();
    }
    void ShowOrder()
    {
        //N-1 to fix array out of bound error, still unresolved.
        for (int i = 0; i < N-1; i++)
        {
            //might be error in this segment
            
            
            //FirstSortedIndex[i] = a;
            if (FirstSortedIndex != null && !Audio.paused)
            {
                pq1.TryDequeue(out int a, out float x);
                FirstSortedIndex[i] = a;

            }
            if (SecondSortedIndex != null && !SecondAudio.paused)
            {
                pq2.TryDequeue(out int u, out float v);
                SecondSortedIndex[i] = u;
            }
            //Debug
            //UnityEngine.Debug.Log(i.ToString() + " " + a.ToString());
        }
    }
    void CalculateSimilarity()
    {
        //int count = 0;
        count = 0;
        for (int i=0; i<CompareParameter; i++)
        {
            for (int j=0; j<CompareParameter; j++)
            {
                if (FirstSortedIndex[i] == SecondSortedIndex[j])
                {
                    count=count + 1 ;
                }
            }
        }
        if (CompareParameter == 0)
        {
            UnityEngine.Debug.Log("Attemped Divide by Zero!!!");
        }
        else
        {
            _matchedPercentage = count * 100 / CompareParameter;
        }
    }
}
