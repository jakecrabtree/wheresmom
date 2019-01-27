using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavRoamer : MonoBehaviour
{
    public GameObject[] waypoints;
    private int target;
    private int direction;
    private bool isMom;
    private bool justInAnim;
    private float initSpeed;

    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        target = 0;
        direction = 1;
        agent = GetComponent<NavMeshAgent>();
        isMom = false;
        justInAnim = false;
        initSpeed = agent.speed;
        if (tag == "Mom")
            isMom = true;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = waypoints[target].transform.position;
        if (Vector3.Distance(transform.position, waypoints[target].transform.position) < 2)
        {
            Next();
        }
        if (isMom && GameManager.Instance.inTugAnimation)
        {
            agent.speed = 0;
            justInAnim = true;
        } else if(isMom && justInAnim)
        {
            justInAnim = false;
            agent.speed = initSpeed;
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
