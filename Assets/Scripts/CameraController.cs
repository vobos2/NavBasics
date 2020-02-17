using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    private float lookSpeed, mouseX, mouseY;
    public float moveSpeed;

    void Start()
    {
        lookSpeed = 4.0f;
        mouseX = mouseY = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }
    // Camera free-look, moves where camera is pointing
    private void movement()
    {
        mouseX += lookSpeed * Input.GetAxis("Mouse X");
        mouseY -= lookSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(mouseY, mouseX, 0.0f);

        Vector3 input = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        transform.position += input * Time.deltaTime * moveSpeed;

        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += Vector3.up * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Only if we are above ground
            if (transform.position.y > 0.0f)
            {
                transform.position -= Vector3.up * Time.deltaTime * moveSpeed;
            }
        }
    }
   
}
