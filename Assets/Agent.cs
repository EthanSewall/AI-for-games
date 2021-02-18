using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{

    Rigidbody rigid;

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
    }

    public Vector3 velocity;

    public void UpdateMovement(Vector3 pos)
    {
        velocity = new Vector3(pos.x, pos.y, pos.z);

        rigid.MovePosition(rigid.position + velocity * Time.deltaTime);
    }
}
