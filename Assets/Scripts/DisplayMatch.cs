using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DisplayMatch : MonoBehaviour
{
    //should have used a [RequireComponent]
    TextMeshProUGUI TextField;

    // Start is called before the first frame update
    void Start()
    {
        TextField = GetComponent<TextMeshProUGUI>();
        TextField.text = "Uninitialized!";
    }

    // Update is called once per frame
    void Update()
    {
        TextField.text = "Match: " + CompareAll._matchedPercentage.ToString() + "%";
    }
}
