using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip Clip;

    public void StartGame()
    {
        SceneManager.LoadScene("GamePlayScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayBtnSound()
    {
        Audio.PlayOneShot(Clip);
    }
}
