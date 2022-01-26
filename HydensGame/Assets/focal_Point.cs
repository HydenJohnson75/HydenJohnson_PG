using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class focal_Point : MonoBehaviour
{
    public float angle;
    private Player character;


    // Start is called before the first frame update
    void Start()
    {
        character = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.transform.position +  Mathf.Sin(angle) * Vector3.up + Mathf.Cos(angle) * character.transform.forward; 
    }
}
