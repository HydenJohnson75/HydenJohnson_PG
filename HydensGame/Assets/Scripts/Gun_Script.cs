using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Script : MonoBehaviour
{

    private Vector3 targetADS = new Vector3(0, -0.041f, 0.1623f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Shoot()
    {
        Ray gun_Ray = new Ray(this.transform.position + (Vector3.up * 0.02f), Camera.main.transform.forward);

        RaycastHit info;

        if (Physics.Raycast(gun_Ray, out info))
        {
            I_Shootable target = info.transform.GetComponent<I_Shootable>();
            if(target != null)
            {
                target.Ive_Been_Shot();
            }
        }

        Debug.DrawRay(this.transform.position + (Vector3.up * 0.1f), Camera.main.transform.forward * 10f, Color.green);
    }


    internal void ADS()
    {
        this.transform.position = Vector3.Lerp(this.transform.localPosition, targetADS, Time.deltaTime);
    }
}
