using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_Secret_Door : MonoBehaviour
{
    enum door_state { Open, Closed, Opening, Closing }

    door_state currently = door_state.Closed;

    Vector3 target = new Vector3(0, 0, 1.92f);
    private float door_speed = 1f;
    private Transform left_Door;
    private Transform right_Door;


    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "Door_Left")
            {
                left_Door = child;
            }

            if (child.name == "Door_Right")
            {
                right_Door = child;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        switch (currently)
        {
            case door_state.Opening:
                right_Door.localPosition -= door_speed * Vector3.forward * Time.deltaTime;

                left_Door.localPosition += door_speed * Vector3.forward * Time.deltaTime;

                if (left_Door.localPosition.z > 1.92f)
                {
                    currently = door_state.Open;

                }
                break;
        }



    }

    public void open_Door()
    {
        if(currently != door_state.Open)
        {
            currently = door_state.Opening;
        }
        
    }
}

