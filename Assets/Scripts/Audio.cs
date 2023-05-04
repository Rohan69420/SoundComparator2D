using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (AudioSource))]
public class Audio : MonoBehaviour
{
    AudioSource _audioSource;
    public static float[] _samples= new float[512]; //number of frequency samples obtained by FFT; 256, 512, 1024
    public static float[] _requiredBands = new float[32]; //10 bands for now but we can alter it for future accuracies
    public static bool paused=false;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            //freezing the FFT values if audio is paused; almost like screenshot action
            GetSpectrumAudioSource();
        }
        MakeFrequencyBands();
    }
    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }
    void MakeFrequencyBands()
    {
        /* from my python test we used 128 chunks and RFFT it to 64 chunks so here, we'll 
         * halve it's accuracy and sample upto the lower 128 bands only and reduce it into 
         * 32 bands for comparison of the sounds 
         * 
         * The division will be from 0 to 4000hz range which happens to be almost 4 times the guitar's
         * playable range (acoustic guitar)
         * 
         * so this FFT also reduces 44100hz to 22050hz
         * then
         * 22050/512 = 43.066hz per chunk 
         * then 0 hz is equivalent to chunk 0th
         * and 4000hz is equivalent to chunk 4000/43.066 = 92.88 ~ 93rd chunk
         * 
         * we want to take it into 32 bands so each band has range equal to 93/32 = 2.906 chunks = 
         * 2.906 * 43.066 = 125.160 hz (very niceeee)
         * 
         * 3*32 = 96 so in the last iteration we need to take bands outside of our desired range
         * 
         * So in each increment the index should be 
         */

        int count = 1;
        for (int i=0; i<32; i++)
        {
            float average =0;

            for (int j=0;j<3;j++) {
                //run the loop 3 times

                average += _samples[count];
                count++;
            }
            //not normalized
            average /= 3;
            _requiredBands[i] = average;
          
        }

    }
    public void SetPause()
    {
        //no safety checks if the audio is present or if it is already playing
        paused = true;
    }

    public void SetPlay()
    {
        if (_audioSource.isPlaying)
        {
            paused = false;
        }
        else
        {
            _audioSource.Play();
            paused = false;
        }
    }
}
