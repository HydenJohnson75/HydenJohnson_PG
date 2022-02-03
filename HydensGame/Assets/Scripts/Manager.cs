using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject main_Player_Template;
    private Camera main_Fps_Camera;
    public GameObject bullet;
    private Transform gun;
    public Player player_Script;


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

        player_Script = main_Player_Template.GetComponent<Player>();

        gun = main_Fps_Camera.transform.Find("Gun");
    }

    // Update is called once per frame
    void Update()
    {
        if (player_Script.Shoot())
        {
            bullet = Instantiate(bullet);
            bullet.transform.position = main_Fps_Camera.transform.position + main_Fps_Camera.transform.forward;
        }
    }
}
