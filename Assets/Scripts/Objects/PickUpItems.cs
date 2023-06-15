using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : MonoBehaviour
{
    public float lifeRecovery;
    public AudioClip lifePickUpAudioClip;
    public AudioSource pickUpAudioSource;
    public SpriteRenderer pickUpSpriteRenderer;
    public float objectLife;
    public float currentObjetLife;

    private void Awake()
    {
        
        pickUpAudioSource = GetComponent<AudioSource>();
        pickUpSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }

    private void Update()
    {
        currentObjetLife += Time.deltaTime;
        if (currentObjetLife >= objectLife)
        {
            Destroy(gameObject);
            currentObjetLife = 0;
        }
    }

    public void playAudio(AudioClip newClip)
    {
        pickUpAudioSource.Stop();
        pickUpAudioSource.clip = newClip;
        pickUpAudioSource.Play();
    }

}
