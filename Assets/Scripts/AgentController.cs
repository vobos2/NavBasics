using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;
    private Vector3 spawnCoords;
    private bool isPicked;
    private bool isAtTarget;
    private Vector3 target;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        RememberSpawn();
        isPicked = false;
        isAtTarget = false;
        agent.isStopped = true;
        this.GetComponent<Renderer>().material.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDest();
    }

    // Checks if agent near destination, stops it if it is.
    private void CheckDest()
    {
        float distanceToTarget = Vector3.Distance(this.agent.transform.position, target);
        if (distanceToTarget <= 1.1f && agent.isStopped == false)
        {
            ImmobilizeAgent();
        }
    }
    // On colliding with another agent that has reached its destination, stops the agent.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Agent"))
        {
            if (other.gameObject.GetComponent<AgentController>().isAtTarget == true)
            {
                ImmobilizeAgent();
            }
        }
    }
    // Stops agent from moving and resets its path
    private void ImmobilizeAgent()
    {
        isAtTarget = true;
        this.agent.isStopped = true;
        this.agent.ResetPath();
    }
    private void RememberSpawn()
    {
        spawnCoords = this.transform.position;
    }
    // Moves agent to desired vector.
    public void MoveAgent(Vector3 t)
    {
        agent.isStopped = false;
        agent.destination = t;
        isAtTarget = false;
        target = t;
    }

    public Vector3 GetSpawnCoords()
    {
        return spawnCoords;
    }
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
