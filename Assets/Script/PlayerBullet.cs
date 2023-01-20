using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bullectSpeed = 8f;
    public Rigidbody bulletRigid;
    public PlayerController playerController;

    void Start()
    {
        bulletRigid.velocity = transform.forward * bullectSpeed;
    }


    void Update()
    {
        Vector3 playerPos = playerController.gameObject.transform.position;
        Vector3 bulletPos = this.gameObject.transform.position;
        Vector3 distance = bulletPos - playerPos;

        if (distance.magnitude >= 30)
        {
            playerController.PushBullet(this);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Die();
                playerController.PushBullet(this);
            }
            else
            {
                return;
            }
        }
    }
}
