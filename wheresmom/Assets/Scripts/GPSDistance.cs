using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPSDistance : MonoBehaviour
{
    Text score;
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Player player = GameManager.Instance.PlayerObj;
        if(!player.on) {
            score.text = "";
        } else { 
            score.text = player.momDistance() + " m";
        }
    }
}
