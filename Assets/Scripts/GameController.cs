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
    private List<GameObject> agents;
    private bool atGoal;
    void Start()
    {
        if (agentNum < 5)
        {
            agentNum = 5;
        }

        agents = new List<GameObject>();
        hits = new List<GameObject>();

        SpawnAgents();

        atGoal = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DetectObjects();
    }

    // Randomize a spawn location and return a vector
    private Vector3 GenSpawnPoint()
    {
        GameObject spawn = GameObject.FindGameObjectWithTag("SpawnBase");

        return new Vector3(spawn.transform.position.x + (Random.insideUnitCircle * 3).x, 1f, spawn.transform.position.z + (Random.insideUnitCircle * 3).x);
    }

    //Spawn agents up to agentNum
    private void SpawnAgents()
    {
        for (int i = 0; i < agentNum; i++)
        {
            GameObject agent = Instantiate(agentPrefab) as GameObject;
            agents.Add(agent.gameObject);
            agent.transform.parent = agentParent.transform;
            agent.transform.position = GenSpawnPoint();

        }
    }

    // Raycasting object selection/deselection method
    private void DetectObjects()
    {
        // Select object
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1500))
            {
                Transform objectHit = hit.transform;
                //Debug.DrawRay(ray.origin, ray.direction, Color.red);

                // Do something with the object that was hit by the raycast.
                if (objectHit.CompareTag("Agent"))
                {
                    // If object already in our hit list, ignore.
                    if (!hits.Contains(objectHit.gameObject))
                    {
                        hits.Add(objectHit.gameObject);
                        objectHit.GetComponent<AgentController>().changeColor();
                        //Debug.Log("Agent Found");
                        //Debug.Log(hit.distance);
                    }

                }
                else
                {
                    Debug.Log("Sending agents to destination " + hit.transform);
                    if (hit.transform)
                    {
                        foreach (GameObject h in hits)
                        {
                            //Debug.Log(hit.transform.position);
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
                    //Debug.Log("Removing Agent");
                    objectHit.GetComponent<AgentController>().changeColor();
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
                //Debug.Log(goal.transform);
                if (goal)
                {
                    Debug.Log("Sending agents to Maze Goal");
                    foreach (GameObject h in hits)
                    {
                        //Debug.Log(goal.transform.position);
                        h.GetComponent<AgentController>().MoveAgent(goal.transform.position);

                    }
                    atGoal = true;
                }
            }
            else
            {
                Debug.Log("Sending agents to Spawn");
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
