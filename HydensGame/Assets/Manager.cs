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

       for (int i = 0; i<100; i++)
       {
            GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cube);

            g.transform.position = new Vector3(Random.Range(-50f, 50f), Random.Range(-50f, 50f), Random.Range(-50f, 50f));
       }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
