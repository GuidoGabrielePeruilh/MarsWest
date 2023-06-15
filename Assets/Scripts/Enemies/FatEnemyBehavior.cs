using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatEnemyBehavior : MonoBehaviour
{
    PlayerMove _player;
    PlayerShoot _playerShoot;
    public float _speed;
    Rigidbody2D _myRB;
    public float damage = 2;
    public float currentFatEnemyLife;
    public float distanceFromPlayer;
    public float minDistancefromPlayer;

    public List<Transform> waypoints = new List<Transform>();
    public int targetWayPointIndex;
    Transform targetWayPoint;
    public float minDistance = 0.1f;
    public float movementSpeed = 3f;
    public float distanceFromWayPoint;

    Animator enemyAnimator;


    private void Awake()
    {
        _player = FindObjectOfType<PlayerMove>();
        _myRB = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        targetWayPoint = waypoints[targetWayPointIndex];
    }
    void Update()
    {
        if (_player != null && _player.currentLifePlayer > 0)
        {
            MoveToPlayer();
        }
    }

    void MoveToPlayer()
    {
        CheckDistanceFromPlayer();
        if(distanceFromPlayer <= minDistancefromPlayer)
        {
            _myRB.velocity += new Vector2(_player.transform.position.x - transform.position.x, _player.transform.position.y - transform.position.y).normalized * Random.Range(5,10) * Time.deltaTime;
         
        }
        else
        {
            CheckDistancetoWayPoint();
            _myRB.velocity += new Vector2(targetWayPoint.position.x - transform.position.x, targetWayPoint.position.y - transform.position.y).normalized * Random.Range(5, 10) * Time.deltaTime;
        }

    }

    void CheckDistanceFromPlayer()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, _player.transform.position);
    }

    void CheckDistancetoWayPoint()
    {
        distanceFromWayPoint = Vector3.Distance(transform.position,targetWayPoint.transform.position);
        if (distanceFromWayPoint <= minDistance)
        {
            targetWayPointIndex++;
            UpdateTargetWayPoint();
        }
    }

    void UpdateTargetWayPoint()
    {
        targetWayPoint = waypoints[targetWayPointIndex];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8) //Player
        {
            _player.currentLifePlayer -= damage;
            Debug.Log("Current Life Player: " + _player.currentLifePlayer);
            Destroy(gameObject); // fat enemy explota
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10) //PjBullet
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            _player.enemiesRemaining -= 1;
        }
    }

}
