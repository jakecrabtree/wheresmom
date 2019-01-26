using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public bool timeUp;
    private float timeLeft;
    Text time;
    // Start is called before the first frame update
    void Start()
    {
        time = GetComponent<Text>();
        timeLeft = 120;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeLeft > 0) {
            timeLeft = Mathf.Clamp(timeLeft - Time.deltaTime, 0, timeLeft);
            time.text = "" + (int) Mathf.Floor(timeLeft);
        } else {
            timeUp = true;
        }
    }
}