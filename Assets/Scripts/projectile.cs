using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    float despawn = 0;

    void Update()
    {
        despawn += Time.deltaTime;
        if(despawn > 8)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(gameObject.layer == 10 && collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyFiniteStateMachine>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (gameObject.layer == 11 && collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if(collision.gameObject.layer == 12)
        {
            Destroy(gameObject);
        }
    }

    public void SetProjectile(bool h)
    {
        if(h)
        {
            gameObject.layer = 10;
        }
        else
        {
            gameObject.layer = 11;
        }
        GetComponent<SphereCollider>().enabled = true;
    }
}
