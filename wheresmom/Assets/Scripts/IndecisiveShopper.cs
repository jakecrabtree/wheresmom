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
    [SerializeField]
    private float pauseAmount;
    private bool stopped;
    private bool isWaiting;

    private NavMeshAgent agent;
    private float initSpeed;
    // Start is called before the first frame update
    void Start()
    {
        target = 0;
        direction = 1;
        agent = GetComponent<NavMeshAgent>();
        prevTime = -1;
        stopped = true;
        isWaiting = false;
        initSpeed = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = waypoints[target].transform.position;
        if (timeUp() || prevTime < 0)
        {
            resetInterval();
        }
        if (stopped)
        {
            agent.speed = 0f;
        }
        else
        {
            agent.speed = initSpeed;
        }

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

    private void resetInterval()
    {
        prevTime = getGameTime();
        stopped = !stopped;
    }

    private bool timeUp()
    {
        return prevTime - getGameTime() >= pauseAmount;
    }

    private float getGameTime()
    {
        return GameManager.Instance.timer.timeLeft;
    }
}
