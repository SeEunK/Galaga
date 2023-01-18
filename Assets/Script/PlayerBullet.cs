using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bullectSpeed = 8f;
    public Rigidbody bulletRigid;

    // Start is called before the first frame update
    void Start()
    {
        bulletRigid.velocity = transform.forward * bullectSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.z >= 20)
        {
            gameObject.SetActive(false);
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
            }
            else
            {
                return;
            }
        }
    }
}
