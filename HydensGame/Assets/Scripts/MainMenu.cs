using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.SceneManagement;

/*  Code to navigate main menu taken from brackeys video on 02/03/2022
    URL:https://www.youtube.com/watch?v=zc8ac_qUXQY&list=PLPV2KyIb3jR4JsOygkHOd2q0CFoslwZOZ&index=1
 */

public class MainMenu : MonoBehaviour
{
    public AudioClip preEncounterClip;
    private AudioSource preEncounterSource;
    public Canvas startCanvas;
    public Canvas lastCanvas;
    public void Start()
    {
        Time.timeScale = 1f;
        preEncounterSource = gameObject.AddComponent<AudioSource>();
        preEncounterSource.playOnAwake = false;
        preEncounterSource.clip = preEncounterClip;
        preEncounterSource.volume = 0.5f;
        preEncounterSource.loop = true;
        preEncounterSource.Play();
        lastCanvas.gameObject.SetActive(false);
    }

    public void PlayGame()
    {
        startCanvas.gameObject.SetActive(false);
        lastCanvas.gameObject.SetActive(true);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
