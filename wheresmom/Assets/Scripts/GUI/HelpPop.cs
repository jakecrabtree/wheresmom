using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HelpPop : MonoBehaviour
{
	public Button help;
	// Use this for initialization
	void Start () {
		help = GetComponent<Button>();
		help.onClick.AddListener(Help);
	}
	
	void Help() {
		SceneManager.LoadScene("Help");
	}
}
