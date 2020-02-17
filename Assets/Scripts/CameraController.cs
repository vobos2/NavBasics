using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    private float lookSpeed, mouseX, mouseY, moveSpeed;

    private List<GameObject> hits;
    void Start()
    {
        hits = new List<GameObject>();
        lookSpeed = 4.0f;
        moveSpeed = 5.0f;
        mouseX = mouseY = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        detectObjects();

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
    private void detectObjects()
    {
        // Select object
        if (Input.GetMouseButtonDown(0))
        {
     
            RaycastHit hit;
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
          
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                Debug.DrawRay(ray.origin, ray.direction, Color.red);
                
                // Do something with the object that was hit by the raycast.
                if (objectHit.CompareTag("Agent"))
                {
                    // If object already in our hit list, ignore.
                    if (!hits.Contains(objectHit.gameObject))
                    {
                        hits.Add(objectHit.gameObject);
                        Debug.Log("Agent Found");
                        Debug.Log(hit.distance);
                    }
                

                } else
                {
                    Debug.Log("Sending agents to destination");
                    foreach (GameObject h in hits)
                    {
                        if (hit.transform)
                        {
                            Debug.Log(hit.transform.position);
                            h.GetComponent<AgentController>().moveAgent(hit.point);
                        }
                    }
                    
                }

            }
        }
        // Deselect object
        else if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                // Do something with the object that was hit by the raycast.
                if (hits.Contains(objectHit.gameObject))
                {
                    Debug.Log("Removing Agent");
                    hits.Remove(objectHit.gameObject);

                }

            }
        }
    }
}
