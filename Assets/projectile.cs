using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{

    bool playerProjectile;

    public int damage;

    void Start()
    {
        playerProjectile = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(playerProjectile && collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyFiniteStateMachine>().TakeDamage(damage);
        }
        if (!playerProjectile && collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    public void SetProjectile(bool h)
    {
        playerProjectile = h;
    }
}
