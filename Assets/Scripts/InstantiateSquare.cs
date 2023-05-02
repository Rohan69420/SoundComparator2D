using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class InstantiateSquare : MonoBehaviour
{
    public GameObject _sampleSquarePrefab;
    GameObject[] _sampleSquare = new GameObject[512];
    public float _maxScale;

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
            _instanceSquare.transform.position = new Vector3(.4f * i, 0, 0);
            //this.transform.position = transform.right  * (i+1);
            _sampleSquare[i] = _instanceSquare;

        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i=0; i<512;i++)
        {
            //hardcoded scales
            _sampleSquare[i].transform.localScale = new Vector3(0.36f,Audio._samples[i] * _maxScale,0.36f);
        }
        
    }
}
