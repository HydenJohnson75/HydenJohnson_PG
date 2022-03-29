using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Script : MonoBehaviour
{

    private Vector3 targetADS = new Vector3(0, -0.035f, 0.179f);
    private Vector3 originalPosition;
    public ParticleSystem muzzleFlash;
    public AudioClip gunAudio;
    private AudioSource gunShot;
    private float fireRate;
    private float fireTime;
    private int dmg;
    internal int maxAmmo;
    internal int currentAmmo;
    private bool isEmpty = false;
    private bool isFiring = false;


    private void Awake()
    {
        muzzleFlash.enableEmission = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        gunShot = gameObject.AddComponent<AudioSource>();
        gunShot.playOnAwake = false;
        gunShot.clip = gunAudio;
        gunShot.Stop();
        currentAmmo = maxAmmo;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (isFiring)
        {
            fireTime -= Time.deltaTime;
            muzzleFlash.enableEmission = true;
            if (fireTime <= 0)
            {
                
                Ray gun_Ray = new Ray(this.transform.position + (Vector3.up * 0.02f), Camera.main.transform.forward);

                gunShot.Play();

                muzzleFlash.Play();

                RaycastHit info;

                if (Physics.Raycast(gun_Ray, out info))
                {
                    I_Shootable target = info.transform.GetComponent<I_Shootable>();
                    if (target != null)
                    {
                        target.Ive_Been_Shot();
                    }
                }

                Debug.DrawRay(this.transform.position + (Vector3.up * 0.03f), Camera.main.transform.forward * 10f, Color.green);

                currentAmmo--;

                fireTime = fireRate;
            }
            else
            {
                muzzleFlash.enableEmission = false;
            }
        }

        isFiring = false;


        if (currentAmmo <= 0)
        {
            isEmpty = true;
        }

        if(isEmpty && Input.GetKeyDown(KeyCode.R)) 
        {
            currentAmmo = maxAmmo;
            isEmpty = false;
        }


    }

    internal void Shoot()
    {
        isFiring = true;
    }

    internal void setup(Vector3 default_position,float fire_Time,float fire_Rate, int gun_Dmg,int max_Ammo,int current_Ammo)
    {
        originalPosition = default_position;
        fireTime = fire_Time;
        fireRate = fire_Rate;
        dmg = gun_Dmg;
        maxAmmo = max_Ammo;
        currentAmmo = current_Ammo;
    }

    internal void ADS()
    {
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, targetADS, (12 * Time.deltaTime));
    }

    internal void noADS()
    {
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, originalPosition, (12 * Time.deltaTime));
    }

    internal int giveGunDmg()
    {
        return dmg;
    }

    internal bool giveIsEmpty()
    {
        return isEmpty;
    }

    internal int giveCurrentAmmo()
    {
        return currentAmmo;
    }
}
