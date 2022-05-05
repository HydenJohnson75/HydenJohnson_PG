using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/*Code for changing resolution and fullscreen taken from brackeys video on youtube on 05/05/2022 
  URL: https://www.youtube.com/watch?v=YOaYQrN1oYQ&list=PLPV2KyIb3jR4JsOygkHOd2q0CFoslwZOZ&index=2  
 */

public class PauseMenu_Script : MonoBehaviour
{

    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject optionMenuUI;
    public Slider sensitivitySlider;
    Resolution[] resolutions;
    public Dropdown resolutionDropDown;

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
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

    public void Quit()
    {
        Application.Quit();
    }

    public void setResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void setFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

}
