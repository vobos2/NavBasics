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
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        RememberSpawn();
        isPicked = false;
        this.GetComponent<Renderer>().material.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {


    }
    private void RememberSpawn()
    {
        spawnCoords = this.transform.position;
    }
    // Moves agent to desired vector.
    public void MoveAgent(Vector3 t)
    {
        agent.destination = t;
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
