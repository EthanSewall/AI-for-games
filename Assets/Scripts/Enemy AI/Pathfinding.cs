using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public GraphNode[] tiles;
    public GraphNode[] navigatingTo;
    public float navigationSpeed;
    public float destinationThreshold;


    public bool pathfinding;

    public int nodeProgress = 0;
    public float destinationDist;

    private void Start()
    {
        pathfinding = true;
        nodeProgress = -1;
        tiles = GameObject.Find("node grid").GetComponent<NodeGrid>().tiles;
    }

    GraphNode GetCheapestTile(GraphNode[] nodes)
    {
        float best = float.MaxValue;
        GraphNode bestNode = null;

        if (nodes.Length != 0)
        {
            for (int i = 0; i < nodes.Length; ++i)
            {
                if (nodes[i].score < best)
                {
                    bestNode = nodes[i];
                    best = nodes[i].score;
                }
            }
        }
        else
        {
            Debug.Log("choosing from empty");
        }
        return bestNode;
    }

    float NodeScore(GraphNode node)
    {
        float distance = (node.gameObject.transform.position - node.previous.gameObject.transform.position).magnitude;
        float score = (distance * node.weight) + node.previous.score;
        return score;
    }

    void Update()
    {
        if (navigatingTo.Length != 0 && nodeProgress != -1 && pathfinding)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, navigatingTo[nodeProgress].gameObject.transform.position, Time.deltaTime * navigationSpeed);

            destinationDist = (gameObject.transform.position - navigatingTo[nodeProgress].gameObject.transform.position).magnitude;

            if (destinationDist < destinationThreshold)
            {
                nodeProgress--;
            }
        }
    }

    public void SetDestination(GraphNode destination)
    {
        GraphNode node = null;
        float nodeDist = float.MaxValue;

        GameObject[] nodes = GameObject.FindGameObjectsWithTag("GameController");
        foreach(GameObject obj in nodes)
        {
            if((obj.transform.position - gameObject.transform.position).magnitude < nodeDist)
            {
                nodeDist = (obj.transform.position - gameObject.transform.position).magnitude;
                node = obj.GetComponent<GraphNode>(); 
            }
        } 
        navigatingTo = CalculatePath(node, destination);

        nodeProgress = navigatingTo.Length - 1;
    }

    public GraphNode[] CalculatePath(GraphNode origin, GraphNode destination)
    {
        List<GraphNode> openList = new List<GraphNode>();
        List<GraphNode> closedList = new List<GraphNode>();

        openList.Add(origin);
        origin.previous = origin;
        origin.score = 0;

        while (openList.Count != 0 && !closedList.Contains(destination))
        {
            GraphNode current;
            if(openList.Count == 1)
            {
                current = openList[0];
            }
            else
            {
                current = GetCheapestTile(openList.ToArray());
            }

            openList.Remove(current);

            closedList.Add(current);



            for (int j = 0; j < current.connections.Length; j++)
            {
                if (!openList.Contains(current.connections[j]) && !closedList.Contains(current.connections[j]))
                {
                    current.connections[j].previous = current;
                    openList.Add(current.connections[j]);
                    current.connections[j].score = NodeScore(current.connections[j]);
                }
                else if (openList.Contains(current.connections[j]))
                {
                    if (NodeScore(current) + ((current.connections[j].gameObject.transform.position - current.connections[j].previous.gameObject.transform.position).magnitude * current.connections[j].weight) < NodeScore(current.connections[j]))
                    {
                        current.connections[j].previous = current;
                    }
                }
            }
        }
        List<GraphNode> path = new List<GraphNode>();
        int h = 0; 
        GraphNode destinationNode = destination;
        while(!path.Contains(origin) && h < tiles.Length)
        {
            path.Add(destinationNode);
            h++;
            if (destinationNode.previous)
            {
                destinationNode = destinationNode.previous;
            }
        }
        return path.ToArray();
    }


}
