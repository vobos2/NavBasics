using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    //public float hSpeed, vSpeed;
    private float lookSpeed, mouseX, mouseY, moveSpeed;
    private Rigidbody rb;
    float moveForward, moveHorizontal, moveVertical;

    void Start()
    {
        moveVertical = moveHorizontal = moveForward = 0.0f;

        RaycastHit hit;
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            // Do something with the object that was hit by the raycast.
        }
        lookSpeed = 3.0f;
        moveSpeed = 5.0f;
        mouseX = mouseY = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        movement();

    }
    private void movement()
    {
        mouseX += lookSpeed * Input.GetAxis("Mouse X");
        mouseY -= lookSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(mouseY, mouseX, 0.0f);

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


        Vector3 input = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        transform.position += input * Time.deltaTime * moveSpeed;


    }
}
