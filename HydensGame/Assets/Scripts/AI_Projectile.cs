using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Projectile : MonoBehaviour
{
    private int damage = 10;
    Ray my_Ray;
    float startDestroyTime = 3f;
    float waitDestroyTime;


    // Start is called before the first frame update
    void Start()
    {
        waitDestroyTime = startDestroyTime;
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += my_Ray.direction* (10*Time.deltaTime);
        if (waitDestroyTime <= 0)
        {
            waitDestroyTime = startDestroyTime;
            Destroy(this.gameObject);
        }
        else
        {
            waitDestroyTime -= Time.deltaTime;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            player.takeDmg(damage);
            player.didITakeDmg(true);
            Destroy(this.gameObject);
        }
        
    }

    internal void getRayLocation(Ray AI_Ray)
    {
        my_Ray = AI_Ray;
    }

}
