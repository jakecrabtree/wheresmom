using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSEnergy : MonoBehaviour {
    private float startWidth;
    private float smoothSpeed = .075f;
    void Start () {
        startWidth = transform.localScale[0];
    }

	void Update() {
        Player player = GameManager.Instance.PlayerObj;
        Vector3 oldScale = transform.localScale;
        Vector3 desiredScale = new Vector3(startWidth * (player.energy / Player.maxEnergy), oldScale[1], oldScale[2]);
		Vector3 smoothedScale = Vector3.Lerp(oldScale, desiredScale, smoothSpeed);
		transform.localScale = smoothedScale;
    }
}
