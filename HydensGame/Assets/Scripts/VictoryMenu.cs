using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class VictoryMenu : MonoBehaviour
{

    public AudioClip preEncounterClip;
    private AudioSource preEncounterSource;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        preEncounterSource = gameObject.AddComponent<AudioSource>();
        preEncounterSource.playOnAwake = false;
        preEncounterSource.clip = preEncounterClip;
        preEncounterSource.volume = 0.5f;
        preEncounterSource.loop = true;
        preEncounterSource.Play();

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
