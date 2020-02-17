using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject agentPrefab, agentParent;

    public int agentNum;

    void Start()
    {
        if (agentNum < 5)
        {
            agentNum = 5;
        }
        spawnAgents();

    }

    // Update is called once per frame
    void Update()
    {

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
}
