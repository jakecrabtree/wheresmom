using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GPSEnergy : MonoBehaviour {
    private float startWidth;
    private float smoothSpeed = .075f;
    void Start () {
        startWidth = transform.localScale[0];
    }

	void Update() {
        Player player = GameManager.Instance.PlayerObj;
        Vector3 oldScale = transform.localScale;

        if(!player.on) {
            transform.localScale = new Vector3(0, oldScale[1], oldScale[2]);
        } else { 
            transform.localScale = new Vector3(startWidth * (player.energy / Player.maxEnergy), oldScale[1], oldScale[2]);
        }
    }
}
