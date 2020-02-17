using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject agentPrefab, agentParent;
    public Camera cam;
    public int agentNum;
    private List<GameObject> hits;
    private bool atGoal;
    void Start()
    {
        if (agentNum < 5)
        {
            agentNum = 5;
        }

        spawnAgents();

        hits = new List<GameObject>();

        atGoal = false;
    }

    // Update is called once per frame
    void Update()
    {
        detectObjects();
    }
    /*    // Return a random value between two values. First value is set up to be negative, second to be positive. Add/Subtract 1 as padding.
        private float RandomizeCoordinates(float[] constraints)
        {
            return Random.Range(constraints[0] + 1, constraints[1] - 1);
        }
        // Randomize a spawn location and return a vector
        private Vector3 GenSpawnPoint(float[] xConstraints, float[] zConstraints)
        {
            return new Vector3(RandomizeCoordinates(xConstraints), 1f, RandomizeCoordinates(zConstraints));
        }
    */

    private void spawnAgents()
    {
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("Spawn");
        Vector3 coord = spawnPoint.transform.position;
        coord.y = 0f;

        //NEED TO IMPLEMENT RANDOMIZED SPAWNING. LOOK INTO GRIDS,CANVAS!!!!

        for (int i = 0; i < agentNum; i++)
        {
            GameObject agent = Instantiate(agentPrefab);
            coord.x += 1f;
            coord.z += .5f;
            agent.transform.parent = agentParent.transform;
            agent.transform.position = coord;
        }
    }

    // Raycasting object selection/deselection method
    private void detectObjects()
    {
        // Select object
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

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


                }
                else
                {
                    Debug.Log("Sending agents to destination");
                    foreach (GameObject h in hits)
                    {
                        if (hit.transform)
                        {
                            Debug.Log(hit.transform.position);
                            h.GetComponent<AgentController>().MoveAgent(hit.point);
                        }
                    }

                }

            }
        }
        // Deselect object
        else if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

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
        //Send object to maze goal ** NEED TO ADD GOAL STATE TO INDIVIDUAL AGENTS **
        else if (Input.GetMouseButtonDown(2))
        {
            if (!atGoal)
            {
                GameObject goal = GameObject.FindGameObjectWithTag("Maze Target");
                Debug.Log(goal.transform);
                if (goal)
                {
                    Debug.Log("Sending agents to Maze Goal");
                    foreach (GameObject h in hits)
                    {
                        Debug.Log(goal.transform.position);
                        h.GetComponent<AgentController>().MoveAgent(goal.transform.position);

                    }
                    atGoal = true;
                }
            } else
            {
                Debug.Log("Sending agents to Maze Goal");
                foreach (GameObject h in hits)
                {
                    AgentController cntrl = h.GetComponent<AgentController>();
                    cntrl.MoveAgent(cntrl.GetSpawnCoords());

                }
                atGoal = false;
            }
        }
    }
}
