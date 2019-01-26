using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
	private static GameManager instance;

	public static GameManager Instance {
		get {
			return instance;
		}
	}
	private bool defeat;
	public bool isPaused;
	public bool alreadyPaused;
	public GameObject pausePrefab;
	GameObject pauseScreen;
	Player player;
	FirstPersonController controller;
	Mom mom;
	Timer timer;
	Canvas canvas;
	Camera playerCamera;
	void Awake () {
		alreadyPaused = false;
		if(instance != null && instance != this) {
			Destroy(this.gameObject);
		} else {
			instance = this;
		}
		playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		canvas = GameObject.FindGameObjectWithTag("hud").GetComponent<Canvas>();
		mom = GameObject.FindGameObjectWithTag("Mom").GetComponent<Mom>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		controller = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
		timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
	}

	public Player PlayerObj {
		get {
			return player;
		}
	}

	public Mom MomObj {
		get {
			return mom;
		}
	}

	void Update() {
		if(timer.timeUp) {
			defeat = true;
			controller.m_MouseLook.SetCursorLock(false);
			SceneManager.LoadScene("Defeat");
		}
		if (Input.GetKeyDown(KeyCode.P)) {
			isPaused = !isPaused;
		}
	}
	
	void OnGUI() {
		if(isPaused) {
			if(!alreadyPaused) {
				Time.timeScale = 0;
				controller.enabled = false;
				pauseScreen = (GameObject) Instantiate(pausePrefab, new Vector3(Screen.width * .5f, Screen.height * .5f, 0), Quaternion.identity, canvas.transform);
				alreadyPaused = true;
				controller.m_MouseLook.SetCursorLock(false);
			}
		} else {
			Time.timeScale = 1;
			controller.enabled = true;
			alreadyPaused = false;
			Destroy(pauseScreen);
			controller.m_MouseLook.SetCursorLock(true);
		}
	}	

}
