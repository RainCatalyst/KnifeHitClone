using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class KnifeIcon : MonoBehaviour
{
    [SerializeField] Color enabledColor;
    [SerializeField] Color disabledColor;

    Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetEnabled(bool enable) => image.color = enable ? enabledColor : disabledColor; 
}
