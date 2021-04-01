using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFiniteStateMachine : MonoBehaviour
{
    public enum States {Chase, OpenFire, Melee }
    public States currentState;

    public int startRange; public int endRange; public int meleeRange; public int moveSpeed; public GameObject prefab;
    public float reloadSpeed; float delay; float distance; public int maxHealth; int currentHealth;
    Pathfinding path; NodeGrid grid; float pathingDelay;

    void Start()
    {
        path = gameObject.GetComponent<Pathfinding>();
        grid = GameObject.Find("node grid").GetComponent<NodeGrid>();
        currentHealth = maxHealth;
        delay = 0;
        pathingDelay = 0;
    }

    void Update()
    {
        distance = ((gameObject.transform.position - GameObject.Find("player").transform.position)).magnitude;
        switch(currentState)
        {
            case States.Chase:
                {
                    if(!path.pathfinding)
                    {
                        path.pathfinding = true;
                    }

                    pathingDelay += Time.deltaTime;
                    if(pathingDelay > 1f)
                    {
                        if (path.navigatingTo.Length == 0)
                        {
                            path.SetDestination(grid.PlayerLocation());
                            pathingDelay = 0;
                        }
                        else
                        {
                            if(!(grid.PlayerLocation() == path.navigatingTo[0]))
                            {
                                path.SetDestination(grid.PlayerLocation());
                            }
                            pathingDelay = 0;
                        }
                    }

                    if(distance < startRange)
                    {
                        path.pathfinding = false;
                        currentState = States.OpenFire;
                    }
                }
                break;
            case States.OpenFire:
                {
                    transform.LookAt(GameObject.Find("player").transform);
                    delay += Time.deltaTime;
                    if (delay > reloadSpeed)
                    {
                        delay = 0;
                        GameObject obj = Instantiate(prefab, gameObject.transform);
                        obj.transform.LookAt(GameObject.Find("player").transform);
                        obj.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 1000);
                        obj.GetComponent<Projectile>().SetProjectile(false);
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
