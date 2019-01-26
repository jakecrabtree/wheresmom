using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Security : MonoBehaviour
{
    private Collider[] detectors;
    private NavMeshAgent guard;

    private bool aggro;
    private bool dead;
    private int counter;
    [SerializeField]
    private float aggro_dist;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            // exit screen
        }
        else if (aggro)
        {

            // store prev patrol target
            // new target = kid, speed increased
        }
        else
        {
            guard.destination = waypoints[target].transform.position;
        }
        if (Vector3.Distance(transform.position, waypoints[target].transform.position) < 1)
            Next();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (counter == 0 && other.tag == "Player")
        {
            Debug.Log("aggro");
            aggro = true;
            counter++;
        } else if(counter == 1 && other.tag == "Player")
        {
            Debug.Log("Dead!");
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
