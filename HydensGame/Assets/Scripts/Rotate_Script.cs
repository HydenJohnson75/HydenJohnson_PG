using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Script : MonoBehaviour
{
    private float degree = 60f;
    private float speed = 0.3f;
    private bool movement = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,degree,0) * Time.deltaTime); 

        if(movement == true)
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);

            if(transform.position.y >= 2)
            {
                movement = false;
            }

        }

        if(movement == false)
        {
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);

            if(transform.position.y <= 1.6)
            {
                movement = true;
            }
        }
    }
}
