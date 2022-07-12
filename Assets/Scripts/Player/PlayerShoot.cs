using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    
    public Bullets _bulletPj;
    PlayerMove _player;


    public float fireRate;
    private float canShootTime;
    public float damage = 1;
    Vector3 mouseWorldPosition;

    Animator animatorPJ;

    private void Awake()
    {
        animatorPJ = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerMove>();

        canShootTime = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(_player.currentLifePlayer >0)
            Shoot();
    }

    void Shoot()
    {
        
        canShootTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0) && (canShootTime >= fireRate))
        {
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            animatorPJ.SetBool("shooting", true);
            canShootTime = 0;
            mouseWorldPosition.z = 0;
            if (mouseWorldPosition.x >= _player.transform.position.x)
                animatorPJ.SetBool("RightShoot", true);
            else
                animatorPJ.SetBool("RightShoot", false);
            

            if (mouseWorldPosition.y >= _player.transform.position.y)
                animatorPJ.SetBool("TopShoot", true);
            else
                animatorPJ.SetBool("TopShoot", false);
        }
    }

    void turnOffShootAnimation()
    {
        animatorPJ.SetBool("shooting", false);
    }

    void SoundandInstantiateAnimationEvent()
    {
        _bulletPj.playAudio(_bulletPj.bulletShootFromPlayer);
        var b = Instantiate(_bulletPj, transform.position, transform.rotation);
        b.transform.up = mouseWorldPosition - transform.position;
        b.speed = _bulletPj.speed;
        b.damage = damage;       
    }
}
