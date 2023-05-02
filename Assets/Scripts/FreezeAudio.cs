using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class FreezeAudio : MonoBehaviour
{
    public AudioSource _audiosource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PauseAudio()
    {
        if (_audiosource != null)
        {
            _audiosource.Pause();
        }
    }

    void PlayAudio()
    {
        if(_audiosource!= null)
        {
            _audiosource.Play();
        }
    }
}
