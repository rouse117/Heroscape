using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController_Cube : MonoBehaviour
{
    public float speed = 5f;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        transform.position += direction * speed * Time.deltaTime;
    }
}

