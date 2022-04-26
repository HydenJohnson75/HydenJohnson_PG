using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu_Script : MonoBehaviour
{

    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject optionMenuUI;
    public Slider sensitivitySlider;

    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Options()
    {
        pauseMenuUI.SetActive(false);
        optionMenuUI.SetActive(true);
        sensitivitySlider.value = Player.mouse_Sensitivity_X;
    }

    public void OptionBack()
    {
        optionMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
}
