using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Script : MonoBehaviour
{

    private Vector3 targetADS = new Vector3(0, -0.035f, 0.179f);
    private Vector3 originalPosition = new Vector3(0.0829f, -0.067f, 0.238f);
    public ParticleSystem muzzleFlash;
    public AudioClip gunAudio;
    private AudioSource gunShot;

    // Start is called before the first frame update
    void Start()
    {
        gunShot = gameObject.AddComponent<AudioSource>();
        gunShot.playOnAwake = false;
        gunShot.clip = gunAudio;
        gunShot.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Shoot()
    {
        Ray gun_Ray = new Ray(this.transform.position + (Vector3.up * 0.02f), Camera.main.transform.forward);

        
        gunShot.Play();
        
        muzzleFlash.Play();

        RaycastHit info;

        if (Physics.Raycast(gun_Ray, out info))
        {
            I_Shootable target = info.transform.GetComponent<I_Shootable>();
            if(target != null)
            {
                target.Ive_Been_Shot();
            }
        }

        Debug.DrawRay(this.transform.position + (Vector3.up * 0.03f), Camera.main.transform.forward * 10f, Color.green);
    }


    internal void ADS()
    {
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, targetADS, (12 * Time.deltaTime));
    }

    internal void noADS()
    {
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, originalPosition, (12 * Time.deltaTime));
    }

}
