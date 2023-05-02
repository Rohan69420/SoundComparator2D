using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandVisualizers : MonoBehaviour
{
    public float _maxScale,_scaleMultiplier;
    public int _band;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, Audio._requiredBands[_band] * _maxScale * _scaleMultiplier , transform.localScale.z);
    }
}
