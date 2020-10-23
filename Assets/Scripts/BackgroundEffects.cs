using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundEffects : MonoBehaviour
{
    private RectTransform bgTransform;
    private Image bgImage;
    private float bgImageColorFactor;
    // Start is called before the first frame update
    void Start()
    {
        bgTransform = GetComponent<RectTransform>();
        bgTransform.localScale *= 2;
        bgImage = GetComponent<Image>();
        bgImageColorFactor = Random.Range(0, 360);
        bgImage.color *= bgImageColorFactor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
