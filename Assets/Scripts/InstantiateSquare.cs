using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class InstantiateSquare : MonoBehaviour
{
    public GameObject _sampleSquarePrefab;
    GameObject[] _sampleSquare = new GameObject[512];
    public float _maxScale;
    public float _offset;

    /*normalization scale to normalize the visualization rects 
     * so that it stays relatives and never goes out of frame */

    //Y offset to spawn the sample visualizers
    public float Y_offset;

    // Start is called before the first frame update
    void Start()
    {
        //instanitate here with a given distance along the X-axis
        for (int i=0; i<512; i++)
        {
            GameObject _instanceSquare = (GameObject)Instantiate(_sampleSquarePrefab);
            _instanceSquare.transform.position = this.transform.position;   //it is starting at same position?
            _instanceSquare.transform.parent = this.transform;
            _instanceSquare.name = "Sample" + i;
            //_instanceSquare.transform.position = transform.right * 100 * (i+1);
            _instanceSquare.transform.position = new Vector3((_sampleSquarePrefab.transform.localScale.x + _offset) * i, Y_offset, 0); //maintaining gap w/ resize
            //this.transform.position = transform.right  * (i+1);
            _sampleSquare[i] = _instanceSquare;

        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i=0; i<512;i++)
        {
            //hardcoded scales transformed into editor changable scaled depending on the prefab
            _sampleSquare[i].transform.localScale = new Vector3(_sampleSquarePrefab.transform.localScale.x ,Audio._samples[i] * _maxScale, _sampleSquarePrefab.transform.localScale.z);
        }
        
    }
}
