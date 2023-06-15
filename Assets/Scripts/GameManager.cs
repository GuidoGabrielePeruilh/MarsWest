using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayerMove player;
    public ShooterEnemy enemy;

    public Slider playerLifeSlider;
    public TextMeshProUGUI playerLifeText;
    private string _initTextPlayer;

    public Slider enemyLifeSlider;
    public TextMeshProUGUI enemyLifeText;
    private string _initTextEnemy;
    public GameObject UIEnemyStats;

    public TextMeshProUGUI enemiesRemainingText;
    private string _initEnemyRemaining;
    public bool checkEnemysRemainginBool;

    public TextMeshProUGUI getMoneyText;

    public SceneChanger sceneChanger;


    void Start()
    {
        _initTextPlayer = playerLifeText.text;
        _initTextEnemy = enemyLifeText.text;
        if(checkEnemysRemainginBool)
            _initEnemyRemaining = enemiesRemainingText.text;
    }

    void Update()
    {
        if (player != null)
        {
            CheckPlayerLife();
            if(checkEnemysRemainginBool)
                CheckEnemiesRemaining();
            CheckMoneyGain();
            CheckWinningCondition();
        }
        if (enemy != null)
            CheckEnemyLife();
    }

    void CheckPlayerLife()
    {
        float playerLife = player.currentLifePlayer;
        playerLifeSlider.value = playerLife;
        playerLifeText.text = _initTextPlayer + player.currentLifePlayer.ToString("f0");

        if (playerLife <= 0)
            sceneChanger.newScene("Defeat");
    }

    void CheckEnemyLife()
    {
        float enemylife = enemy.currentEnemyLife;
        enemyLifeSlider.value = enemylife;
        enemyLifeText.text = _initTextEnemy + enemy.currentEnemyLife.ToString("f0");

        if (enemy.distanceFromPlayer <= enemy.minDistance && enemy.currentEnemyLife != 0)
        {
            UIEnemyStats.SetActive(true);
        }
        else
            UIEnemyStats.SetActive(false);
    }

    void CheckEnemiesRemaining()
    {
        enemiesRemainingText.text = _initEnemyRemaining + player.enemiesRemaining.ToString("f0");
        if(player.enemiesRemaining <= 0)
        {
            sceneChanger.newScene("Level2");
        }

    }

    void CheckMoneyGain()
    {
        if (player.MoneyGain)
            getMoneyText.text = "Scape with the Money";
    }

    void CheckWinningCondition()
    {
        if (player.PJScaped)
        {
            sceneChanger.newScene("Victory");
        }
    }
}
