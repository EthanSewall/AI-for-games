using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode : MonoBehaviour
{
    public GraphNode previous;
    public GraphNode[] connections;

    public float score;
    public float weight = 1;

}
