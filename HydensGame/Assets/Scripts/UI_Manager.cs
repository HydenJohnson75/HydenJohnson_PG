using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{

    Canvas my_canvas;
    Text ammo_Counter;
    GameObject my_Text;
    Text interact_Text;
    public GameObject player;  
    Player my_Player;
    Image buff;
    Text operator_Text;
    bool hasBuff;
    public AudioClip reloadingClip;
    private AudioSource reloadingSource;
    internal Manager my_Main_Manager;
    TextMeshProUGUI damageText;
    CanvasGroup dmgCG;
    public GameObject operatorGroup_GO;
    Text boostText;
    public GameObject pistolImg;
    public GameObject arImg;
    private CanvasGroup pistolCG;
    private CanvasGroup arCG;
    private Gun_Script activeGun;
    public Slider hpSlider;

    // Start is called before the first frame update
    void Start()
    {
        my_canvas = GetComponentInChildren<Canvas>();
        ammo_Counter = my_canvas.GetComponentInChildren<Text>();
        my_Text = GameObject.FindGameObjectWithTag("Interact");
        my_Player = player.GetComponent<Player>();
        interact_Text = my_Text.GetComponent<Text>();
        buff = operatorGroup_GO.GetComponentInChildren<Image>();     
        operator_Text = operatorGroup_GO.gameObject.transform.Find("Operator_Text").GetComponentInChildren<Text>();
        reloadingSource = gameObject.AddComponent<AudioSource>();
        reloadingSource.playOnAwake = false;
        reloadingSource.clip = reloadingClip;
        reloadingSource.Stop();
        my_Main_Manager = GameObject.Find("Manager").GetComponent<Manager>();
        boostText = my_canvas.gameObject.transform.Find("SpeedBuff").GetComponentInChildren<Text>(); 
        damageText = my_canvas.gameObject.transform.Find("DamageNotifier").GetComponentInChildren<TextMeshProUGUI>();
        damageText.enabled = false;
        dmgCG = damageText.GetComponent<CanvasGroup>();
        arImg.SetActive(false);
        pistolCG = pistolImg.GetComponent<CanvasGroup>();
        arCG = arImg.GetComponent<CanvasGroup>();
        activeGun = my_Player.giveActiveGun();
        hpSlider.value = my_Player.giveMaxHp();
    }

    // Update is called once per frame
    void Update()
    {
        interact_Text.enabled = false;
        boostText.enabled = false;
        activeGun = my_Player.giveActiveGun();

        hpSlider.value = my_Player.giveCurrentHP();

        if (my_Player.hasGun2)
        {
            arImg.SetActive(true);
        }

        if(my_Player.giveActiveGun() == my_Player.my_Guns[0])
        {
            arCG.alpha = 0.22f;
            pistolCG.alpha = 1f;
        }
        if(my_Player.giveActiveGun() == my_Player.my_Guns[1])
        {
            pistolCG.alpha = 0.22f;
            arCG.alpha = 1f;
        }

        ammo_Counter.text = activeGun.currentAmmo.ToString();


        if(hasBuff == false)
        {
            buff.enabled = false;
            operator_Text.enabled = false;
        }
        else if(hasBuff == true)
        {
            buff.enabled = true;
            operator_Text.enabled = true;
        }

        if (my_Player.gotBuff())
        {
            hasBuff = true;
        }

        if(my_Main_Manager.gameOn == true)
        {
            damageText.enabled = true;
        }
        else
        {
            damageText.enabled = false;
        }


        if (my_Player.gotSpeedBuff())
        {
            boostText.enabled = true;
        }

        
        

    }


}
