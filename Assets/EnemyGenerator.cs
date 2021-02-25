using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject prefab;
    float loading = 0;
    public float delay;


    void Update()
    {
        loading += Time.deltaTime;
        if (loading > delay)
        {
            Instantiate(prefab, gameObject.transform);
            loading = 0;
        }
    }
}
