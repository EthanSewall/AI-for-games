using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFiniteStateMachine : MonoBehaviour
{
    public enum States {Chase, OpenFire, Melee }
    public States currentState;

    public int startRange;
    public int endRange;
    public int meleeRange;
    public int moveSpeed;

    public GameObject prefab;
    public float reloadSpeed;
    float delay;

    float distance;

    public int maxHealth;
    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        delay = 0;
    }

    void Update()
    {
        distance = ((gameObject.transform.position - GameObject.Find("player").transform.position)).magnitude;
        switch(currentState)
        {
            case States.Chase:
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, GameObject.Find("player").transform.position, moveSpeed * Time.deltaTime);

                    if(distance < startRange)
                    {
                        currentState = States.OpenFire;
                    }
                }
                break;
            case States.OpenFire:
                {
                    delay += Time.deltaTime;
                    if (delay > reloadSpeed)
                    {
                        delay = 0;
                        GameObject obj = Instantiate(prefab, gameObject.transform);
                        obj.transform.LookAt(GameObject.Find("player").transform);
                        obj.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 1000);
                        obj.GetComponent<projectile>().SetProjectile(false);
                    }

                    if (distance < meleeRange)
                    {
                        currentState = States.Melee;
                    }
                    else if (distance > endRange)
                    {
                        currentState = States.Chase;
                    }
                }
                break;
            case States.Melee:
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, GameObject.Find("player").transform.position, 1.5f * moveSpeed * Time.deltaTime);
                    if (distance > meleeRange)
                    {
                        currentState = States.OpenFire;
                    }
                }
                break;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
