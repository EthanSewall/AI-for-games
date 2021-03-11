using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject prefab;
    float loading = 0;
    public float delay;
    public int maximum;


    void Update()
    {
        loading += Time.deltaTime;
        if (loading > delay)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length <= maximum)
            {
                Instantiate(prefab, gameObject.transform);
                loading = 0;
            }
            else
            {
                loading = 0;
            }
        }
    }
}
