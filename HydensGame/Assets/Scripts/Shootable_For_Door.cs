using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable_For_Door : MonoBehaviour, I_Shootable
{
    public GameObject door_GO;
    private I_Actionable door;
    public void Ive_Been_Shot()
    {
        door.open_Door();
    }

    // Start is called before the first frame update
    void Start()
    {
        door = door_GO.GetComponent<Open_Double_Door>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
