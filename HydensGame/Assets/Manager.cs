using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject main_Player_Template;
    private Camera main_Fps_Camera;

    // Start is called before the first frame update
    void Start()
    {
       main_Fps_Camera = main_Player_Template.GetComponentInChildren<Camera>();

        if(main_Player_Template.GetComponentsInChildren<Camera>() != null)
        {
            print("I found my camera");

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
