using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    float lifetime;
    public float speed;
    public float lifeTimeBullet;
    public float damage;
    PlayerMove _player;

    public AudioClip bulletShootFromPlayer;
    public AudioSource bulletsAudioSourceShoot;

    public AudioClip playerGettingHit;
    public AudioClip enemyShoot;
    public AudioClip impactBullet;

    SpriteRenderer bulletSpriteRenderer;
    CapsuleCollider2D bulletCollider;


    private void Awake()
    {
        _player = FindObjectOfType<PlayerMove>();
        bulletsAudioSourceShoot = gameObject.GetComponent<AudioSource>();
        bulletSpriteRenderer = GetComponent<SpriteRenderer>();
        bulletCollider = GetComponent<CapsuleCollider2D>();
    }
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;

        lifetime += Time.deltaTime; //que se destruya despues de cierto tiempo, para que no quede ocupando lugar en el juego
        if (lifetime >= lifeTimeBullet)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)// walls
        {
            bulletsAudioSourceShoot.PlayOneShot(impactBullet);
            bulletSpriteRenderer.enabled = false;
            bulletCollider.enabled = false;
        }

        if (collision.gameObject.layer == 8) //Player
        {
            
            _player.currentLifePlayer -= damage;
            bulletsAudioSourceShoot.PlayOneShot(playerGettingHit);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            bulletSpriteRenderer.enabled = false;
            bulletCollider.enabled = false;
        }
    }


    public void playAudio(AudioClip newClip)
    {
        bulletsAudioSourceShoot.Stop();
        bulletsAudioSourceShoot.clip = newClip;
        bulletsAudioSourceShoot.Play();
    }
}
