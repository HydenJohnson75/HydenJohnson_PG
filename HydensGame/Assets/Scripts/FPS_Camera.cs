using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Camera : MonoBehaviour
{
    Transform owning_Character_Transform;
    private Player owning_Character;
    private Transform focal_Point;

    // Start is called before the first frame update
    void Start()
    {
        focal_Point = FindObjectOfType<focal_Point>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(-0.0059998245f, 0.693000019f, 0.331f);
        transform.LookAt(focal_Point);
    }


    internal void you_Belong_To_Me(Player player)
    {
        owning_Character_Transform = player.transform;
        owning_Character = player;
    }
}
