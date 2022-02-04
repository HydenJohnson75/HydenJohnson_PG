using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_Door : MonoBehaviour, I_Shootable
{
    enum door_state { Open, Closed, Opening, Closing}

    door_state currently = door_state.Closed;

    Vector3 target = new Vector3(0.001f, -2.7f,-0.001f);
    private float door_speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currently)
        {
            case door_state.Opening:
                transform.localPosition += door_speed * Vector3.down * Time.deltaTime;

                if (transform.localPosition.y < -2.7f)
                {
                    currently = door_state.Open;
                    transform.localPosition = target;
                }
                break;
        }



    }

    public void open_Door()
    {
        currently = door_state.Opening;
        //Vector3 current_Position = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z);

        //transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, (1.0f * Time.deltaTime));

    }

    public void Ive_Been_Shot()
    {
        print("I have been shot");
    }
}
