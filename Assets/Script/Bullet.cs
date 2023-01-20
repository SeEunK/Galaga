using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bullectSpeed = 5f;
    public Rigidbody bulletRigid;
    public Enemy shooter;


    void Start()
    {
        bulletRigid = GetComponent<Rigidbody>();
        bulletRigid.velocity = transform.forward * bullectSpeed;
      
    }

 
    void Update()
    {
        Vector3 enemyPos = shooter.gameObject.transform.position;
        Vector3 bulletPos = this.gameObject.transform.position;
        Vector3 distance = bulletPos- enemyPos; 

        if (distance.magnitude >= 30)
        {
            shooter.PushBullet(this);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                if (playerController.playerState == PlayerController.PlayerState.Live)
                {
                    playerController.Die();
                    shooter.PushBullet(this);
                }
            }
            else
            {
                return;
            }
        }
    }
}
