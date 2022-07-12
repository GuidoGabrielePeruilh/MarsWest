using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public PlayerMove player;
    //public Transform player;
    private Vector3 auxVector;
    [SerializeField]
    public float leftLimit;
    [SerializeField]
    public float rigthLimit;
    [SerializeField]
    public float bottomLimit;
    [SerializeField]
    public float topLimit;

    void Start()
    {
        player = FindObjectOfType<PlayerMove>();
    }

    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        //auxVector = new Vector3(player.transform.position.x, player.transform.position.y, -100);
        //transform.position = auxVector;
        // Clamp es una forma de limitar un valor a un determinado rango, Limito X al limite izquierdo y derecho
        transform.position = new Vector3(
            Mathf.Clamp(player.transform.position.x, leftLimit, rigthLimit),
            Mathf.Clamp(player.transform.position.y, bottomLimit, topLimit),
            -100
            );

    }
}
