using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    Text interact_Text;

    // Start is called before the first frame update
    void Start()
    {
        interact_Text = GetComponent<Text>();

        interact_Text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (interact_Text = GetComponent<Text>()){
            interact_Text.text = "Press E to interact";
        }
    }
}
