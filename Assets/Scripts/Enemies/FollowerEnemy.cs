using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerEnemy : MonoBehaviour
{
    PlayerMove _player;
    public float _speed;
    Rigidbody2D _myRB;
    public float damage = 2;
    public float currentFatEnemyLife;
    public float distanceFromPlayer;
    public float minDistancefromPlayer;

    public List<Transform> waypoints = new List<Transform>();
    public int targetWayPointIndex;
    Transform targetWayPoint;
    public int lastWayPointIndex;
    
    public bool moveForward;


    private void Awake()
    {
        _player = FindObjectOfType<PlayerMove>();
        _myRB = GetComponent<Rigidbody2D>();

    }
    private void Start()
    {
        lastWayPointIndex = waypoints.Count-1;
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
            _myRB.velocity += new Vector2(_player.transform.position.x - transform.position.x, _player.transform.position.y - transform.position.y).normalized * _speed * Time.deltaTime;
        }
        else
        {
            //CheckDistancetoWayPoint();
            _myRB.velocity += new Vector2(targetWayPoint.position.x - transform.position.x, targetWayPoint.position.y - transform.position.y).normalized * _speed * Time.deltaTime;

        }

    }

    void CheckDistanceFromPlayer()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, _player.transform.position);
    }

    //void CheckDistancetoWayPoint()
    //{
    //    distanceFromWayPoint = Vector3.Distance(transform.position,targetWayPoint.position);
    //    if (distanceFromWayPoint <= minDistancetoWatPoint)
    //    {
    //        targetWayPointIndex += 1;
    //        UpdateTargetWayPoint();
    //    }
    //}

    void UpdateTargetWayPoint()
    {
        if (targetWayPointIndex >= lastWayPointIndex)
        {
            moveForward = false;
            Debug.Log("Iguales");
            //targetWayPointIndex --;
        }
        else if(targetWayPointIndex == 0)
        {
            moveForward = true;
            //targetWayPointIndex ++;
        }
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
        if (collision.gameObject.layer == 16) //WayPoints
        {
            if (moveForward)
                targetWayPointIndex++;
            else
                targetWayPointIndex--;
            UpdateTargetWayPoint();
        }
    }
}
