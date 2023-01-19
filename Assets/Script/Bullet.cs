using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bullectSpeed = 8f;
    public Rigidbody bulletRigid;
    private Vector3 direction;

    public void Shoot(Vector3 direction)
    {
        this.direction = direction;
        Invoke("DestroyBullet", 5.0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletRigid = GetComponent<Rigidbody>();
        bulletRigid.velocity = transform.forward * bullectSpeed;
        // Destroy(gameObject, 3.0f);
    }

    // public void DestroyBullet()
    // {
    // ObjectPool.ReturnObject(this);
    // }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction);
        if (gameObject.transform.position.z <= -20)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.Die();
            }
            else
            {
                return;
            }
        }
    }
}
