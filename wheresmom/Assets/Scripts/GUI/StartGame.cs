using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
	public Button start;
	// Use this for initialization
	void Start () {
		start = GetComponent<Button>();
		start.onClick.AddListener(OpenGame);
	}
	
	void OpenGame() {
		SceneManager.LoadScene("JakeGame"); 
        Time.timeScale = 1;
	}
}
