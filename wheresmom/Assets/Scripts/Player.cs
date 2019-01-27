using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static float consumptionRate = .1f;
    public static float maxEnergy = 100.0f;
    public float energy;
    public bool on = false;
    // Start is called before the first frame update
    void Start()
    {
        energy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && energy > 0.0f) {
            on = true;
        }
        if (Input.GetMouseButtonUp(1)) {
            on = false;
        }
        if(on) {
            energy = Mathf.Clamp(energy - consumptionRate, 0, maxEnergy);
            if(energy == 0) {
                on = false;
            }
        }
    }

    public float momDistance() {
        Mom mom = GameManager.Instance.MomObj;
        float distance = Vector3.Distance(gameObject.transform.position, mom.transform.position);
        return distance;
    }

}
