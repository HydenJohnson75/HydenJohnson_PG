using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_Door : MonoBehaviour
{
    
    Vector3 target = new Vector3(0.001f, -2.7f,-0.001f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void open_Door()
    {

        Vector3 current_Position = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z);

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, (1.0f * Time.deltaTime));

    }



}
