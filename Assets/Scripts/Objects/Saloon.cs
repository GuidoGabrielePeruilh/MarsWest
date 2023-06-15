using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saloon : MonoBehaviour
{
    public GameObject upgradesOptions;
    public float lifeRecoveryUpgrade;
    public float damageUpgrade;

    public BoxCollider2D colliderSaloon;

    public bool itWasUsed = false;

    private void Awake()
    {
        colliderSaloon = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (itWasUsed)
            colliderSaloon.isTrigger = false;
    }
}
