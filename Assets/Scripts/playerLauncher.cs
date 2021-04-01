using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLauncher : MonoBehaviour
{
    public float reloadSpeed;
    float delay = 0;
    public GameObject prefab;

    void Update()
    {
        delay += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Mouse0) && delay > reloadSpeed)
        {
            delay = 0;
            GameObject obj = Instantiate(prefab, gameObject.transform);
            obj.transform.SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation);
            obj.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 1500);
            obj.GetComponent<Projectile>().SetProjectile(true);
            obj.transform.parent = null;
        }
    }
}
