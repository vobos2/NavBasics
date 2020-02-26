using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;
    private Vector3 spawnCoords, target;
    private bool isPicked;
    public float brakingDistance, agentSpeed, intensity;

    private List<GameObject> agents;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agents = new List<GameObject>(GameObject.FindGameObjectsWithTag("Agent"));
        agents.Remove(this.agent.gameObject);
        //Debug.Log(agents.Count);
        RememberSpawn();
        isPicked = false;
        agent.isStopped = true;
        brakingDistance = 1.5f;
        agentSpeed = 6f;
        intensity = 5f;

        this.GetComponent<Renderer>().material.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (Picked())
        {
            CheckDist();
        }
    }
    // Decelerate. If value is lower than 0, set to 0. 
    private void Decelerate(float val)
    {
        if (agent.speed > 0)
        {
            agent.speed -= val;
        }
        else if (agent.speed < 0)
        {
            agent.speed = 0;
        }
    }
    // Accelerate. If value is higher than max speed, cap it. 
    private void Accelerate(float val)
    {
        if (agent.speed < agentSpeed)
        {
            agent.speed += val;
        }
        else if (agent.speed > agentSpeed)
        {
            agent.speed = agentSpeed;
        }
    }
    // Checks if agent near other agents, stops it if it is.
    private void CheckDist()
    {
        agents.ForEach(delegate (GameObject a)
         {
             if (a.GetComponent<AgentController>().Picked() == true)
             {
                 float distToOtherAgent = DistToTarget(agent.gameObject, a.transform.position);
                 //Debug.Log(distToOtherAgent);
                 if (distToOtherAgent < brakingDistance)
                 {
                 
                         //Debug.Log("Agent " + distToOtherAgent + " units away, SLOWING DOWN");
                         Decelerate(intensity);
                  
                 }
                 else
                 {
                     Accelerate(agent.speed);
                 }
             }
         });

        float distanceToTarget = DistToTarget(agent.gameObject, target);

        //If distance is less than threshold, initiate braking protocol.
        if (distanceToTarget <= brakingDistance)
        {
            Decelerate(intensity);
        }
        // If agent exits range, accelerate
        else if (distanceToTarget > brakingDistance)
        {
            Accelerate(intensity);
        }
    }
    // Is the agent selected?
    public bool Picked()
    {
        return isPicked;
    }
    // Returns distance from obj to target
    private float DistToTarget(GameObject obj, Vector3 target)
    {
        return Vector3.Distance(obj.transform.position, target);
    }

    // Moves agent to desired vector.
    public void MoveAgent(Vector3 t)
    {
        agent.isStopped = false;
        agent.destination = t;
        target = t;
    }

    // spawnCoords helper method.
    public Vector3 GetSpawnCoords()
    {
        return spawnCoords;
    }
    // Sets spawnCoords vector to agent's spawn coordinates.
    private void RememberSpawn()
    {
        spawnCoords = this.transform.position;
    }
    // Changes color to green if picked, white if not picked.
    public void changeColor()
    {
        if (!isPicked)
        {
            this.GetComponent<Renderer>().material.color = Color.green;
            isPicked = true;
        }
        else
        {
            this.GetComponent<Renderer>().material.color = Color.white;
            isPicked = false;
        }
    }

}
