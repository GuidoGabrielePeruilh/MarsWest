using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasButtons : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject CreditsMenu;
    public GameObject InstructionsMenu;

    private void Start()
    {
        MainMenuButton();
    }
    public void closeGame()
    {
        Application.Quit();
    }

    public void MainMenuButton()
    {
        mainMenu.SetActive(true);
        CreditsMenu.SetActive(false);
        InstructionsMenu.SetActive(false);
    }

    public void CreditsMenuButton()
    {
        mainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
        InstructionsMenu.SetActive(false);
    }

    public void InstructionsMenuButton()
    {
        mainMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        InstructionsMenu.SetActive(true);
    }
}
