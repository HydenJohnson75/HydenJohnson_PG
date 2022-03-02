using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip preEncounterClip;
    private AudioSource preEncounterSource;

    public void Start()
    {
        preEncounterSource = gameObject.AddComponent<AudioSource>();
        preEncounterSource.playOnAwake = false;
        preEncounterSource.clip = preEncounterClip;
        preEncounterSource.volume = 0.5f;
        preEncounterSource.loop = true;
        preEncounterSource.Play();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
