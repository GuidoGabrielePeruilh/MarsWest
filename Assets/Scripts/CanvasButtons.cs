using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasButtons : MonoBehaviour
{
    string currentScene;

    public GameObject mainMenu;
    public GameObject CreditsMenu;
    public GameObject InstructionsMenu;
    public GameObject upgradesOptions;
    public GameObject victoryScreen;
    public GameObject defeatScreen;

    public PlayerMove _player;
    public Saloon _saloonStats;
    public PlayerShoot _playerShootStats;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "MainMenu")
            MainMenuButton();

        if (currentScene != "MainMenu")
            _player = FindObjectOfType<PlayerMove>();
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

    public void VictoryMenu()
    {
        mainMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        InstructionsMenu.SetActive(false);

    }
    public void DefeatMenu()
    {
        mainMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        InstructionsMenu.SetActive(false);


    }

    public void enterSaloonUpgrades()
    {
        upgradesOptions.SetActive(true);
    }

    public void getOutSaloonUpgrades()
    {
        upgradesOptions.SetActive(false);
        _player.gameObject.SetActive(true);
    }

    public void lifeUpgradeButton()
    {
        _player._initLife = _saloonStats.lifeRecoveryUpgrade;
        _player.currentLifePlayer = _saloonStats.lifeRecoveryUpgrade;
        getOutSaloonUpgrades();
    }

    public void moreDamageUpgrade()
    {
        _playerShootStats.damage = _saloonStats.damageUpgrade;
        getOutSaloonUpgrades();
    }
}
