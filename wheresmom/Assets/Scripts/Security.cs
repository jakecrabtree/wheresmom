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
    [SerializeField]
    private float aggro_speed;
    [SerializeField]
    private float aggro_delay_detach;
    private float time_out_of_range;

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
        time_out_of_range = -1.0f;
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
            guard.speed = aggro_speed;
            if (time_out_of_range > 0)
            {
                if (time_out_of_range - GameManager.Instance.timer.timeLeft >= aggro_delay_detach)
                {
                    aggro = false;
                    time_out_of_range = -1.0f;
                    Debug.Log("De-aggroed");
                }
            }
            else if (Vector3.Distance(transform.position, guard.destination) > aggro_dist)
            {
                Debug.Log("out of range.");
                time_out_of_range = GameManager.Instance.timer.timeLeft;
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
