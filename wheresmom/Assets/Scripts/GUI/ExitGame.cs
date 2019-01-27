using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour
{
	public Button exit;
	// Use this for initialization
	void Start () {
		exit = GetComponent<Button>();
		exit.onClick.AddListener(Exit);
	}
	
	void Exit() {
		SceneManager.LoadScene("Main");
	}
}
