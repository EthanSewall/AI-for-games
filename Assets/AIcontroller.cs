using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIcontroller : MonoBehaviour
{
    Agent agent;
    public enum Behaviors { Seek, Flee}
    List<Behaviors> currentBehaviors;
    public Transform target;
    public float speed;
    public float maxForce;
    public bool addBehavior;
    public Behaviors addingBehavior;

    void Start()
    {
        currentBehaviors = new List<Behaviors>();
        agent = gameObject.GetComponent<Agent>();
    }


    void Update()
    {
        if(addBehavior)
        {
            currentBehaviors.Add(addingBehavior);
            addBehavior = false;
        }

        Vector3 steering = Vector3.zero;
        foreach (Behaviors b in currentBehaviors)
        {
            switch (b)
            {
                case Behaviors.Seek:
                    {
                        Vector3 seek = (target.position - gameObject.transform.position).normalized * speed;
                        steering += seek;
                    }
                    break;
                case Behaviors.Flee:
                    {
                        Vector3 flee = (target.position - gameObject.transform.position).normalized * speed;
                        steering -= flee;
                    }
                    break;
            }
        }
        steering = Vector3.ClampMagnitude(steering, maxForce);
        agent.UpdateMovement(steering);
    }
}
