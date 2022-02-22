using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_Double_Door : MonoBehaviour, I_Actionable
{
    enum door_state { Open, Closed, Opening, Closing }

    door_state currently = door_state.Closed;

    Vector3 target = new Vector3(-1.957317f, -2.7f, -0f);
    private float door_speed = 1f;
    private Transform top_Door;
    private Transform bottom_Door;

    // Start is called before the first frame update
    void Start()
    {
       foreach(Transform child in transform)
        {
            if(child.name == "door_2_right")
            {
                top_Door = child;
            }

            if(child.name == "door_2_left")
            {
                bottom_Door = child;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currently)
        {
            case door_state.Opening:
                bottom_Door.localPosition += door_speed * Vector3.down * Time.deltaTime;

                top_Door.localPosition -= door_speed * Vector3.down * Time.deltaTime;

                if (bottom_Door.localPosition.y < -2.7f)
                {
                    currently = door_state.Open;
                  
                }
                break;
        }



    }

    public void open_Door()
    {
        currently = door_state.Opening;
    }
}
