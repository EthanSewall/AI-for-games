using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode : MonoBehaviour
{
    public GraphNode previous;
    public GraphNode[] connections;

    public float score;
    public float weight = 1;

    Pathfinding navigator;
    public bool NavigateToThis;

    void Start()
    {
        navigator = GameObject.Find("navigator").GetComponent<Pathfinding>();
    }

    void Update()
    {
        if(NavigateToThis)
        {
            navigator.SetDestination(this);
            NavigateToThis = false;
        }
    }
}
