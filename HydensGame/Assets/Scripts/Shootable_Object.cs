using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shootable_Object : MonoBehaviour, I_Shootable
{

    int numberCounter = 0;

    TextMeshPro number;
    public void Ive_Been_Shot()
    {
        numberCounter++;

        
    }

    // Start is called before the first frame update
    void Start()
    {
        number = GetComponentInChildren<TextMeshPro>();
        number.text = numberCounter.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(numberCounter > 9)
        {
            numberCounter = 0;
        }

        number.text = numberCounter.ToString();
    }
}
