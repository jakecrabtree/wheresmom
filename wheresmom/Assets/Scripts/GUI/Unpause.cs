using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Unpause : MonoBehaviour
{
    public Button unpause;
	// Use this for initialization
	void Start () {
		unpause = GetComponent<Button>();
		unpause.onClick.AddListener(UnpauseGame);
	}
	
	void UnpauseGame() {
        GameManager.Instance.isPaused = false;
    }
}
