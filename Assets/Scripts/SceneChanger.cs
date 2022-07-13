using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    GameObject audio1;
    AudioSource audioSourceMainMenu;
    SpriteRenderer audioSpriteRenderer;

    GameObject audioGameLevels;
    AudioSource audioSourceaudioGameLevels;
    SpriteRenderer audioSpriteRendereraudioGameLevels;
    private void Awake()
    {

        audio1 = GameObject.Find("Audio");
        audioSourceMainMenu = audio1.GetComponent<AudioSource>();
        audioSpriteRenderer = audio1.GetComponent<SpriteRenderer>();

    }
    private void Start()
    {
        if (!audioSourceMainMenu.isPlaying && audioSpriteRenderer.enabled)
        {
            audioSourceMainMenu.Play();
        }
    }

    public void newScene(string sceneName)
    {

        DontDestroyOnLoad(audioSourceMainMenu);
        if (sceneName == "Level1")
        {
            audioSourceMainMenu.Stop();
            audioSpriteRenderer.enabled = false;

        }
        else if (sceneName == "Level2")
        {
            audioGameLevels = GameObject.Find("AmbientMusic");
            audioSourceaudioGameLevels = audioGameLevels.GetComponent<AudioSource>();
            audioSpriteRendereraudioGameLevels = audioGameLevels.GetComponent<SpriteRenderer>();
            DontDestroyOnLoad(audioGameLevels);
        }
        else
            audioSpriteRenderer.enabled = true;
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }

    public void closeGame()
    {
        Application.Quit();
    }

}
