using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fill : MonoBehaviour
{
    public int value;
    [SerializeField] Text valueDisplay;
    [SerializeField] private float speed;
    private bool hasCombined;

    private Image image;

    public void FillValueUpdate(int value)
    {
        this.value = value;
        valueDisplay.text = this.value.ToString();

        int colorIndex = GetColorIndex(value);
        image = GetComponent<Image>();
        image.color = GameController.instance.fillColors[colorIndex % 5];
    }

    int GetColorIndex(int value)
    {
        int index = 0;
        while (value != 1)
        {
            index++;
            value /= 2;
        }
        index--;
        return index; 
    }
    private void Update()
    {
        if (transform.localPosition != Vector3.zero)
        {
            hasCombined = false;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, speed * Time.deltaTime);
        }   
        else if (!hasCombined)
        {
            if (transform.parent.GetChild(0) != this.transform)
            {
                Destroy(transform.parent.GetChild(0).gameObject);
            }

            hasCombined = true;
        }
    }
    public void Double()
    {
        value *= 2;
        GameController.instance.ScoreUpdate(value);
        valueDisplay.text = value.ToString();
        
        int colorIndex = GetColorIndex(value);
        image.color = GameController.instance.fillColors[colorIndex];
        
        GameController.instance.WinningCheck(value);
        
    }
}
