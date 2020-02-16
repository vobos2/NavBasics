using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player, agentPrefab;
    public CameraController camController;

    public int agentNum;

    void Start()
    {
        if (agentNum < 5)
        {
            agentNum = 5;
        }
        for (int i = 0; i < agentNum; i++)
        {
            //spawn agents
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
