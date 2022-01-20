using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private int player_Hp = 100;
    private float speed = 10f;
    internal enum player_States {moving, idle, takingDmg, healing, dead};
    internal player_States my_State = player_States.idle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            my_State = player_States.moving;
            transform.position += Vector3.forward * (speed) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            my_State = player_States.moving;
            transform.position += Vector3.left * (speed) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            my_State = player_States.moving;
            transform.position += Vector3.back * (speed) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            my_State = player_States.moving;
            transform.position += Vector3.right * (speed) * Time.deltaTime;
        }
    }
}
