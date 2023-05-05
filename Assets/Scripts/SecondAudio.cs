using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class SecondAudio : MonoBehaviour
{
    //Adding microphone functionality
    public AudioClip _audioClip;
    public bool _useMicrophone;


    AudioSource _secondAudioSource;
    public static float[] _secondSamples = new float[512]; //number of frequency samples obtained by FFT; 256, 512, 1024
    public static float[] _secondRequiredBands = new float[32]; //10 bands for now but we can alter it for future accuracies
    public static bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        _secondAudioSource = GetComponent<AudioSource>();

        //microphone input

        if (_useMicrophone)
        {
            if (Microphone.devices.Length > 0)
            {
                //can use the selected device by converting it into string
                //set to 10 seconds
                /*here the microphone is constantly looping and the input audio being clipped
                 * but I want it to start on button click of play therefore I'll shift this 
                 * line of code to the button click
                 */

                
                /* These functions mean that as soon as microphone is started, it starts to buffer
                 but it doesnt start playing back until we use the Play() function of the clip
                it retake another 10 seconds of buffer and makes into the clip and plays it and so on
                */

                //set looping mode of audio enabled for microphone for continuous feed
                _secondAudioSource.loop = true;

                _secondAudioSource.clip = Microphone.Start(null, true, 10, AudioSettings.outputSampleRate);
                
                
                //record for the first 10 seconds and then we need to keep on recording until we need to stop?
                //now play the 10 second chunk
               
                _secondAudioSource.Play();
                
                //also need to set the loop to enable incase of microphone to allow continuous stream
                //and disable it on pause?
            }
            else
            {
                _useMicrophone = false;
            }
        }
        else if(_audioClip!= null) 
        {
            //set loop disabled so that play pause can work as intended
            _secondAudioSource.loop = false;

            UnityEngine.Debug.Log("Clip should be assigned!");
            _secondAudioSource.clip = _audioClip;
        }
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
        _secondAudioSource.GetSpectrumData(_secondSamples, 0, FFTWindow.Blackman);
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
        for (int i = 0; i < 32; i++)
        {
            float average = 0;

            for (int j = 0; j < 3; j++)
            {
                //run the loop 3 times

                average += _secondSamples[count];
                count++;
            }
            //not normalized
            average /= 3;
            _secondRequiredBands[i] = average;

        }

    }
    public void SetPause()
    {
        //no safety checks if the audio is present or if it is already playing
        //pause is done using Unity Pause()
        paused = true;

        //end microphone if mic is being used to avoid annoying loop
       /* if (_useMicrophone && Microphone.IsRecording(null))
        {
            //null since we have selected default mic only
            Microphone.End(null);
            //the pausing is done by the editor based function
        } */    
    }

    public void SetPlay()
    {
        /*
        if (_secondAudioSource.isPlaying && !_useMicrophone)
        {
            //no need to replay and use Unity Unpause(); just set the flag if it is paused
            paused = false;
        }
        else
        {
            if (_useMicrophone)
            {
                //query if the microphone is already recording
                if (!Microphone.IsRecording(null))
                {
                    //here is the handling for the microphone
                    _secondAudioSource.clip = Microphone.Start(null, true, 10, AudioSettings.outputSampleRate);

                }
                /* we not only have to start the microphone but play it after starting so that the feedback
                starts and we can graph the live audio */
         /*       _secondAudioSource.Play();
            }
            else
            {
                //if it hasn't played yet then play it
                _secondAudioSource.Play();
                paused = false;
            }
        }
        */
        if (_secondAudioSource.isPlaying)
        {
            paused = false;
        }
        else
        {
            //if it hasn't played yet then play it
            _secondAudioSource.Play();
            paused = false;
        }

    }
}
