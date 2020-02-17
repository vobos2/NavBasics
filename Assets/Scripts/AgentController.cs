using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Destination
      
    }
    // our raycasts can set the destination of the agent(s) by calling this method and passing in the raycast destination
    public void moveAgent(Vector3 t)
    {
            agent.destination = t;
    }
    
}
