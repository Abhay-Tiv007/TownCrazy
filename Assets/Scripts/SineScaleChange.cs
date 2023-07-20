using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SineScaleChange : MonoBehaviour
{
    [SerializeField] private float baseScale;
    [SerializeField] private float amplitude;
    [SerializeField] private float omega;

    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.fontSize = baseScale + Mathf.Sin(omega * Time.timeSinceLevelLoad) * amplitude;
        //transform.localScale = baseScale + Mathf.Sin(omega * Time.timeSinceLevelLoad) * amplitude;
    }
}
