using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class focal_Point : MonoBehaviour
{
    public float angle;
    private Player character;
    private float vertical_Sensitivity = 0.05f;
    private float distance = 0f;
    private Vector3 desired_camera_position;


    // Start is called before the first frame update
    void Start()
    {
        character = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, desired_camera_position, 0.01f);
    }

    internal void adjust_Vertical_Angle(float vertical_Adjustment)
    {
        angle += vertical_Adjustment * vertical_Sensitivity;
        angle = Mathf.Clamp(angle, -1, 0);
        print(vertical_Adjustment);

        desired_camera_position = new Vector3(0, distance * Mathf.Cos(angle), distance * Mathf.Sin(angle));
    }
}
