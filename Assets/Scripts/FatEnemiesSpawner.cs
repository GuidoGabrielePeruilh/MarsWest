using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatEnemiesSpawner : MonoBehaviour
{
    public float timeToSpawnFatEnemy;
    public float currentTime;
    PlayerMove _player;

    public GameObject FatEnemy;
    private void Start()
    {
        _player = FindObjectOfType<PlayerMove>();
    }
    void Update()
    {
        currentTime += Time.deltaTime;
        if((currentTime >= timeToSpawnFatEnemy) && (_player.enemiesRemaining > 0)) //para que no0 crees enemigos constantemente y que pare cuando ya no haya mas enemigos para ganar. Es condicion de victoria
        {
            Instantiate(FatEnemy, transform.position, transform.rotation);
            currentTime = 0;
        }
    }
}
