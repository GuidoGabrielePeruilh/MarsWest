using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    PlayerMove _player;
    PlayerShoot _playerShoot;
    public float distanceFromPlayer;
    public float minDistance;

    public float _speed;
    bool _changeDir;
    float _ChangePosTimer;

    float _Range;
    Vector3 lookAt;

    bool lookAtPj;

    Transform positionChild;

    public Bullets _enemyBullet;
    float prepareToShootTime;
    public float fireRate;

    public float _initLife;
    public float currentEnemyLife;
    public GameObject lifePowerUp;
    public float damage = 1;

    public bool moveVertical = true;

    Animator animatorEnemy;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerMove>();
        _playerShoot = FindObjectOfType<PlayerShoot>();
        positionChild = transform.GetChild(0).GetComponent<Transform>().transform;
        animatorEnemy = GetComponent<Animator>();


    }

    // Start is called before the first frame update
    void Start()
    {
        currentEnemyLife = _initLife;

        _Range = 2.14f; // valor que se asigno para que cambie de direccion
    }

    void Update()
    {
        if (_player.isActiveAndEnabled)
        {

            LookAtPlayer();
            ShootToPlayer();


            if (_ChangePosTimer >= _Range)
            {
                changeDirection();
            }

            if (lookAtPj == false)
                Move();

            if (currentEnemyLife <= 0)
            {
                Destroy(gameObject);
                Instantiate(lifePowerUp, transform.position, transform.rotation); //cuando se destruya, crea un life power up. Le da vida al personaje
                _player.enemiesRemaining -= 1;
            }
        }
    }

    void CheckDistanceFromPlayer()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, _player.transform.position);
    }

    void ShootToPlayer()
    {

        CheckDistanceFromPlayer();
        if (distanceFromPlayer <= minDistance)
        {
            prepareToShootTime += Time.deltaTime;
            if (prepareToShootTime >= fireRate)
            {
                //_enemyBullet.playAudio(_enemyBullet.enemyShoot);
                _enemyBullet.bulletsAudioSourceShoot.PlayOneShot(_enemyBullet.enemyShoot);
                //var b = Instantiate(_enemyBullet, transform.position, transform.rotation);
                var b = Instantiate(_enemyBullet, positionChild.position, positionChild.rotation);
                //b.transform.up = transform.up;
                b.transform.up = positionChild.up;
                b.speed = _enemyBullet.speed;
                b.damage = damage;
                prepareToShootTime = 0;
                

            }
        }
    }
    void LookAtPlayer()
    {
        CheckDistanceFromPlayer();
        if (distanceFromPlayer <= minDistance)
        {
            //transform.GetChild(0).GetComponent<Transform>().transform.up;
            lookAt = _player.transform.position - transform.position;
            lookAtPj = true;
            animatorEnemy.SetBool("Walking", false);


        }
        else
        {
            _ChangePosTimer += Time.deltaTime;
            lookAtPj = false;
            animatorEnemy.SetBool("Walking", true);
        }
        positionChild.up = lookAt;
        //transform.up = lookAt;
    }
    void Move()
    {

        if (moveVertical)
        {
            if (_changeDir)
            {
                transform.position += _speed * new Vector3(0, 1, 0) * Time.deltaTime; // si es true
            }
            else
            {
                transform.position += _speed * new Vector3(0, -1, 0) * Time.deltaTime; // si es false
            }

        }
        else
        {
            if (_changeDir)
            {
                transform.position += _speed * new Vector3(1, 0, 0) * Time.deltaTime; // si es true
            }
            else
            {
                transform.position += _speed * new Vector3(-1, 0, 0) * Time.deltaTime; // si es false
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10) //PjBullet
        {
            currentEnemyLife -= _playerShoot.damage;
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 13) //objetos
        {
            changeDirection();
        }
    }

    void changeDirection()
    {
        _changeDir = !_changeDir;
        _ChangePosTimer = 0;
    }
}
