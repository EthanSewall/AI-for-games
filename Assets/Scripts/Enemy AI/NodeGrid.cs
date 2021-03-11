using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    public GameObject prefab;

    public GraphNode[] tiles;

    public int gridWidth;
    public int gridHeight;

    public float spacingX;
    public float spacingZ;

    void Start()
    {
        tiles = new GraphNode[gridWidth * gridHeight];

        Vector3 offset = Vector3.zero;

        for (int i = 0; i < gridHeight; ++i)
        {
            for (int j = 0; j < gridWidth; ++j)
            {
                GameObject newTile = Instantiate(prefab, transform.position + offset, transform.rotation);
                tiles[i * gridWidth + j] = newTile.GetComponent<GraphNode>();
                newTile.name = (i * gridWidth + j).ToString();
                offset.x += spacingX;
                newTile.transform.parent = gameObject.transform;
            }

            offset.x = 0.0f;
            offset.z += spacingZ;
        }

        for (int i = 0; i < tiles.Length; ++i)
        {
            List<GraphNode> connectedNodes = new List<GraphNode>();

            if (i % gridWidth != 0)
            {         
                int layerMask = 1 << 2;
                layerMask = ~layerMask;

                RaycastHit hit;
                if (!(Physics.Raycast(tiles[i].gameObject.transform.position, transform.TransformDirection(Vector3.forward), out hit, spacingZ, layerMask)))
                {
                    connectedNodes.Add(tiles[i + gridWidth]);
                }
            }
            if ((i + 1) % gridWidth != 0)
            {
                int layerMask = 1 << 2;
                layerMask = ~layerMask;

                RaycastHit hit;
                if (!(Physics.Raycast(tiles[i].gameObject.transform.position, transform.TransformDirection(Vector3.back), out hit, spacingZ, layerMask)))
                {
                    connectedNodes.Add(tiles[i - gridWidth]);
                }
            }
            if (i < gridWidth * gridHeight - gridWidth)
            {
                int layerMask = 1 << 2;
                layerMask = ~layerMask;

                RaycastHit hit;
                if (!(Physics.Raycast(tiles[i].gameObject.transform.position, transform.TransformDirection(Vector3.right), out hit, spacingX, layerMask)))
                {
                    connectedNodes.Add(tiles[i + 1]);
                }
            }
            if (i > gridWidth)
            {
                int layerMask = 1 << 2;
                layerMask = ~layerMask;

                RaycastHit hit;
                if (!(Physics.Raycast(tiles[i].gameObject.transform.position, transform.TransformDirection(Vector3.left), out hit, spacingX, layerMask)))
                {
                    connectedNodes.Add(tiles[i - 1]);
                }
            }

            tiles[i].connections = connectedNodes.ToArray();
        }
    }

    public GraphNode PlayerLocation()
    {
        GameObject player = GameObject.Find("player");
        GraphNode node = null;
        float nodeDist = float.MaxValue;

        GameObject[] nodes = GameObject.FindGameObjectsWithTag("GameController");
        foreach (GameObject obj in nodes)
        {
            if ((obj.transform.position - player.transform.position).magnitude < nodeDist)
            {
                nodeDist = (obj.transform.position - player.transform.position).magnitude;
                node = obj.GetComponent<GraphNode>();
            }
        }
        return node;
    }
}
