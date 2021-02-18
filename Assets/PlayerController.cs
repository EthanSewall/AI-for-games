using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Agent agent;
    public float speed;
    void Start()
    {
        agent = gameObject.GetComponent<Agent>();
    }
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        input = Vector3.ClampMagnitude(input, 1);
        agent.UpdateMovement(input * speed);
    }
}
