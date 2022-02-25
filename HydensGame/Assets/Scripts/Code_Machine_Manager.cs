using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Code_Machine_Manager : MonoBehaviour
{
    TextMeshPro[] code_Numbers;
    Shootable_Object[] terminals;

    // Start is called before the first frame update
    void Start()
    {
        code_Numbers = gameObject.GetComponentsInChildren<TextMeshPro>();
        terminals = gameObject.GetComponentsInChildren<Shootable_Object>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal int currentNumberCheck()
    {
        return (characterValue(code_Numbers[0].text) * 1000 + characterValue(code_Numbers[1].text) *100 + characterValue(code_Numbers[2].text) * 10 + characterValue(code_Numbers[3].text));
    }


    internal int characterValue(string codeNumber)
    {

        char c = codeNumber[0];

        return c - '0';
    }
}
