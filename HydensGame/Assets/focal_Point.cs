using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class focal_Point : MonoBehaviour
{
    public float angle;
    private Player character;
    private float vertical_Sensitivity = 0.05f;
    private Vector3 desired_camera_position;


    // Start is called before the first frame update
    void Start()
    {
        character = FindObjectOfType<Player>();
        desired_camera_position = new Vector3(0, 1, 10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, desired_camera_position, 0.2f);
    }

    internal void adjust_Vertical_Angle(float vertical_Adjustment)
    {
        desired_camera_position += vertical_Sensitivity*vertical_Adjustment * Vector3.up;

        desired_camera_position = new Vector3(desired_camera_position.x, Mathf.Clamp(desired_camera_position.y, -30f, 50f), desired_camera_position.z);

    }

}
