using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public int maxHP;
    int currentHP;
    GameObject hinge;
    Rigidbody rigid;
    GameObject gauge;

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
        currentHP = maxHP;
        hinge = GameObject.Find("x-hinge");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gauge = GameObject.Find("gauge");
    }
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        input = Vector3.ClampMagnitude(input, 1);

        rigid.AddRelativeForce(input * speed * Time.deltaTime * 6000);

        transform.Rotate(0, Input.GetAxisRaw("Mouse X") * rotationSpeed * Time.deltaTime * 6f, 0);
        hinge.transform.Rotate(-Input.GetAxisRaw("Mouse Y") * rotationSpeed * Time.deltaTime * 6f, 0, 0);

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void TakeDamage(int damage)
    {
        gauge.transform.localScale = new Vector3(1, 1.5f, 1);

        gauge.transform.localScale = new Vector3(((float)currentHP / (float)maxHP) * 10, 1.5f, 1);

        currentHP -= damage;
        if (currentHP <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadSceneAsync(1);
        }
    }
}
