using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    AudioSource myAudioSource;
    public AudioClip walkAudio;

    public float _speedMovement;
    public float _initLife;
    Rigidbody2D _myRB;
    public float currentLifePlayer;
    public float quantityEnemiesToWin;
    public float enemiesRemaining;
    ShooterEnemy _shooterEnemy;
    Animator animatorPJ;
    SpriteRenderer mySpriteRenderer;
    public bool mirrowed = false;

    public PickUpItems pickUpItemsInformation;

    private void Awake()
    {
        animatorPJ = GetComponent<Animator>();
     }

    void Start()
    {
        _myRB = GetComponent<Rigidbody2D>();
        _shooterEnemy = FindObjectOfType<ShooterEnemy>();
        currentLifePlayer = _initLife;
        enemiesRemaining = quantityEnemiesToWin;
        myAudioSource = GetComponent<AudioSource>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        
        
    }

    void Update()
    {
        pickUpItemsInformation = FindObjectOfType<PickUpItems>();
        if (currentLifePlayer <= 0)
        {
            animatorPJ.SetBool("dead", true);
            //gameObject.SetActive(false);
            currentLifePlayer = 0;
            Debug.Log("Player PERDIO");
            myAudioSource.Stop();
        }
        else
            MoveController();
        if (enemiesRemaining <= 0)
        {
            enemiesRemaining = 0;
            Debug.Log("Player GANO, no hay mas enemigos");
        }
    }
    private void FixedUpdate()
    {

    }
    void MoveController()
    {
        var h = Input.GetAxis("Horizontal"); 
        var v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            animatorPJ.SetBool("Walking", true);
            
        }
        else
        {
            animatorPJ.SetBool("Walking", false);
            playAudio(walkAudio);
        }

        if (h != 0 && v != 0)
            animatorPJ.SetBool("Diagonal", true);
        else
            animatorPJ.SetBool("Diagonal", false);

        if (h > 0 && v > 0)
        {
            animatorPJ.SetBool("TopRight", true);
            animatorPJ.SetBool("TopLeft", false);
            animatorPJ.SetBool("ButtomRight", false);
            animatorPJ.SetBool("ButtomLeft", false);
        }
        else if (h> 0 && v< 0)
        {
            animatorPJ.SetBool("TopRight", false);
            animatorPJ.SetBool("TopLeft", false);
            animatorPJ.SetBool("ButtomRight", true);
            animatorPJ.SetBool("ButtomLeft", false);
        }
        else if (h< 0 && v< 0)
        {
            animatorPJ.SetBool("TopRight", false);
            animatorPJ.SetBool("TopLeft", false);
            animatorPJ.SetBool("ButtomRight", false);
            animatorPJ.SetBool("ButtomLeft", true);
        }
        else if (h< 0 && v> 0)
        {
            animatorPJ.SetBool("TopRight", false);
            animatorPJ.SetBool("TopLeft", true);
            animatorPJ.SetBool("ButtomRight", false);
            animatorPJ.SetBool("ButtomLeft", false);
        }
        else
        {
            animatorPJ.SetBool("TopRight", false);
            animatorPJ.SetBool("TopLeft", false);
            animatorPJ.SetBool("ButtomRight", false);
            animatorPJ.SetBool("ButtomLeft", false);
        }
            


        animatorPJ.SetFloat("VerticalMove", v);
        animatorPJ.SetFloat("HorizontalMove", h);
        _myRB.velocity += new Vector2(h, v).normalized * _speedMovement * Time.deltaTime;


        //Para setear la animacion que camine hacia la izquiera. 
        if ((_myRB.velocity.x < 0 && mirrowed == false))
        {
            mySpriteRenderer.flipX = true;
            mirrowed = true;
        }
        else if (_myRB.velocity.x > 0 && mirrowed == true)
        {
            mySpriteRenderer.flipX = false;
            mirrowed = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11) //Life PowerUp
        {
            
            if (currentLifePlayer < _initLife)
            {
                pickUpItemsInformation.GetComponent<SpriteRenderer>().enabled = false;
                
                currentLifePlayer += pickUpItemsInformation.lifeRecovery;
                pickUpItemsInformation.playAudio(pickUpItemsInformation.lifePickUpAudioClip);
                pickUpItemsInformation.GetComponent<CircleCollider2D>().enabled = false;
            }

            else
                Debug.Log("Vida Maxima Alcanzada");
            Debug.Log("Current Life Player: " + currentLifePlayer);
            if (!pickUpItemsInformation.pickUpAudioSource.isPlaying)
                Destroy(collision.gameObject);
        }

        if (collision.gameObject.layer == 8) //Player
        {
            currentLifePlayer -= _shooterEnemy.damage;
            Destroy(collision.gameObject);
        }

    }

    void playAudio(AudioClip newClip)
    {
        myAudioSource.Stop();
        myAudioSource.clip = newClip;
        myAudioSource.Play();
    }

}
