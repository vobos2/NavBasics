using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;
    private Vector3 spawnCoords;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        RememberSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    private void RememberSpawn()
    {
        spawnCoords = this.transform.position;
    }
    #region public 
    // Moves agent to desired vector.
    public void MoveAgent(Vector3 t)
    {
        agent.destination = t;
    }
   
    public Vector3 GetSpawnCoords()
    {
        return spawnCoords;
    }
    #endregion
}
