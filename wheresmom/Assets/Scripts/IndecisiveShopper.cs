using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IndecisiveShopper : MonoBehaviour
{
    public GameObject[] waypoints;
    private int target;
    private int direction;
    private float prevTime;

    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        target = 0;
        direction = 1;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = waypoints[target].transform.position;
        if (Vector3.Distance(transform.position, waypoints[target].transform.position) < 2)
        {
            Next();
        }
    }

    public void Next()
    {
        if (target + direction >= waypoints.Length || target + direction < 0)
        {
            direction = -direction;
        }
        target = target + direction;
    }
}
