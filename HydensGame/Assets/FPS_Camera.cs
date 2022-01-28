using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Camera : MonoBehaviour
{
    float angle = 0f;
    float distance = 0f;
    Transform owning_Character_Transform;
    private Player owning_Character;
    private float focal_Height = 2f;
    private Transform focal_Point;
    private float vertical_Sensitivity = 0.05f;
    private Vector3 desired_camera_position;

    // Start is called before the first frame update
    void Start()
    {
        focal_Point = FindObjectOfType<focal_Point>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0, 1, 0);
        transform.LookAt(focal_Point);
    }

    internal void adjust_Vertical_Angle(float vertical_Adjustment)
    {
        angle += vertical_Adjustment * vertical_Sensitivity;
        angle = Mathf.Clamp(angle, -1, 0);
        print(vertical_Adjustment);

        desired_camera_position = new Vector3(0, distance * Mathf.Cos(angle), distance * Mathf.Sin(angle));
    }

    internal void you_Belong_To_Me(Player player)
    {
        owning_Character_Transform = player.transform;
        owning_Character = player;

    }
}
