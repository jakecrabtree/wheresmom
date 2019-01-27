using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Security : MonoBehaviour
{
    private Collider[] detectors;
    private NavMeshAgent guard;
    private float initSpeed;

    private bool aggro;
    private bool dead;
    private int counter;
    [SerializeField]
    private float aggroDist;
    [SerializeField]
    private float aggroSpeed;
    [SerializeField]
    private float aggroDelayDetach;
    private float timeOutOfRange;

    public GameObject[] waypoints;
    private int target;
    private int direction;

    // since one collider is inside other, check counter for reference
    // Start is called before the first frame update
    void Start()
    {
        detectors = GetComponents<Collider>();
        aggro = false;
        counter = 0;

        guard = GetComponent<NavMeshAgent>();
        target = 0;
        direction = 1;
        timeOutOfRange = -1.0f;
        initSpeed = guard.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            GameManager.Instance.defeat = true;
        }
        else if (aggro)
        {
            guard.destination = GameManager.Instance.PlayerObj.transform.position;
            guard.speed = aggroSpeed;
            if (timeOutOfRange > 0)
            {
                if (timeOutOfRange - GameManager.Instance.timer.timeLeft >= aggroDelayDetach)
                {
                    aggro = false;
                    timeOutOfRange = -1.0f;
                    guard.speed = initSpeed;
                    Debug.Log("De-aggroed");
                }
            }
            else if (Vector3.Distance(transform.position, guard.destination) > aggroDist)
            {
                Debug.Log("out of range.");
                timeOutOfRange = GameManager.Instance.timer.timeLeft;
            }
        }
        else
        {
            guard.destination = waypoints[target].transform.position;
            if (Vector3.Distance(transform.position, guard.destination) < 2)
                Next();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (counter == 0 && other.tag == "Player")
        {
            aggro = true;
            counter++;
        } else if(counter == 1 && other.tag == "Player")
        {
            dead = true;
            counter++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        counter--;
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
