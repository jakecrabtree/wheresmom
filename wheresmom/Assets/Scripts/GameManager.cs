using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {
	private static GameManager instance;

	public static GameManager Instance {
		get {
			return instance;
		}
	}
	public AudioClip winSound;
	public AudioClip heartbeatSound;
	public AudioClip breathingSound;
	public bool defeat;
	public bool isPaused;
	public bool alreadyPaused;
	public bool inAnimation = false;
	public bool inTugAnimation = false;
	public bool animationEnded = true;
	public bool exitAnimationStarted = false;
	private Vector3 initialPosition;
	private Quaternion startRot;
	private HidingSpot prevHidingSpot;
	public GameObject pausePrefab;
	AudioSource playerSpeaker;
	GameObject pauseScreen;
	Player player;
	Animator rightArmAnimator;
	FirstPersonController controller;
	Mom mom;
	public Timer timer;
	Canvas canvas;
	public Camera playerCamera;
	Image interactImage;

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
		interactImage = GameObject.FindGameObjectWithTag("InteractImage").GetComponent<Image>();
		interactImage.enabled = false;
		rightArmAnimator = GameObject.FindGameObjectWithTag("RightArm").GetComponent<Animator>();
		playerSpeaker = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
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

	public AudioSource SpeakerObj {
		get {
			return playerSpeaker;
		}
	}

	void Update() {
		if(timer.timeUp || defeat) {
			controller.m_MouseLook.SetCursorLock(false);
			SceneManager.LoadScene("Defeat");
		}
		if (Input.GetKeyDown(KeyCode.P)) {
			isPaused = !isPaused;
		}
		if (inAnimation && animationEnded && !exitAnimationStarted && Input.GetKeyDown(KeyCode.E)){
			exitAnimationStarted = true;
			StartCoroutine(CrawlingOutAnimation(initialPosition, controller.transform.position));
		}
	}

	public void Win(){
		controller.m_MouseLook.SetCursorLock(false);
		playerSpeaker.PlayOneShot(winSound, .5f);
		SceneManager.LoadScene("Victory");
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
			if (!inAnimation && !inTugAnimation){
				controller.enabled = true;
			}
			alreadyPaused = false;
			Destroy(pauseScreen);
			controller.m_MouseLook.SetCursorLock(true);
		}
	}	

	public void LoadInteractImage(string imagePath){
		interactImage.sprite = Resources.Load<Sprite>(imagePath);
		interactImage.enabled = true;
	}

	public void DisableInteractImage(){
		if (!inAnimation){
			interactImage.enabled = false;
		}
	}

	public void DisableControls(){
		controller.enabled = false;
	}

	public void CrawlIntoHidingSpot(Vector3 pos, Vector3 forward, HidingSpot hiding){
		inAnimation = true;
		animationEnded = false;
		prevHidingSpot = hiding;
		DisableControls();
		StartCoroutine(CrawlingAnimation(pos, forward));
	}

	public void CheckTug(bool realMom){
		inTugAnimation = true;
		DisableControls();
		StartCoroutine(TugAnimation(realMom));
	}

	float crouchTime = 0.33f;
	float crouchDist = 1;
	float crawlTime = 1f;
	float turnTime = 1f;
	float tugTime = 3f;
	float waitTime = 1f;

	public IEnumerator TugAnimation(bool realMom) {
		rightArmAnimator.Play("Tug");
		yield return new WaitForSeconds(tugTime);
		startRot = Camera.main.transform.rotation;
	 	for(float i = 0; i <= turnTime; i+= Time.deltaTime){
		 	Camera.main.transform.Rotate(new Vector3(-1.0f, 0, 0) * Time.deltaTime * 30);
			yield return null;
		}
		if(realMom) {
			GameManager.Instance.Win();
			yield return null;
		}
		rightArmAnimator.Play("Lower");
		yield return new WaitForSeconds(waitTime);
		inTugAnimation = false;
		yield return null;
	}

	public IEnumerator CrawlingAnimation(Vector3 pos, Vector3 forward){
		Vector3 startControllerPos = controller.transform.position;
		initialPosition = startControllerPos;
		Vector3 startCameraPos = Camera.main.transform.position;
		for(float i = 0; i <= crouchTime; i+= Time.deltaTime){
			float dist = Mathf.Lerp(0, crouchDist, i/crouchTime);
			Camera.main.transform.position = startCameraPos - new Vector3(0, dist, 0);
			yield return null;
		}
		startCameraPos = Camera.main.transform.position;
		Vector3 path = (pos - startControllerPos)*.8f;	
		for(float i = 0; i <= crawlTime; i+= Time.deltaTime){
			Vector3 curr = Vector3.Lerp(new Vector3(0,0,0), path, i/crawlTime);
			controller.transform.position = startControllerPos + curr;
			yield return null;
		}
		startRot = Camera.main.transform.rotation;
		Quaternion lookOnLook = Quaternion.LookRotation(forward- Camera.main.transform.position);
	 	for(float i = 0; i <= turnTime; i+= Time.deltaTime){
		 	Quaternion curr = Quaternion.Slerp(startRot, lookOnLook, i/turnTime);
			Camera.main.transform.rotation = curr;
			yield return null;
		}
		playerSpeaker.PlayOneShot(heartbeatSound, .5f);
		animationEnded = true;
		LoadInteractImage(Interactable.makeAbsPath("hidingexit"));
		yield return null;
	}

	public IEnumerator CrawlingOutAnimation(Vector3 pos, Vector3 forward){
		Quaternion initialRot = Camera.main.transform.rotation;
	 	for(float i = 0; i <= turnTime; i+= Time.deltaTime){
		 	Quaternion curr = Quaternion.Slerp(initialRot, startRot, i/turnTime);
			Camera.main.transform.rotation = curr;
			yield return null;
		}
		Vector3 startControllerPos = controller.transform.position;
		Vector3 path = (pos - startControllerPos);	
		for(float i = 0; i <= crawlTime; i+= Time.deltaTime){
			Vector3 curr = Vector3.Lerp(new Vector3(0,0,0), path, i/crawlTime);
			controller.transform.position = startControllerPos + curr;
			yield return null;
		}
		Vector3 startCameraPos = Camera.main.transform.position;
		for(float i = 0; i <= crouchTime; i+= Time.deltaTime){
			float dist = Mathf.Lerp(0, crouchDist, i/crouchTime);
			Camera.main.transform.position = startCameraPos + new Vector3(0, dist, 0);
			yield return null;
		}
		inAnimation = false;
		exitAnimationStarted = false;
		prevHidingSpot.gameObject.GetComponent<Collider>().enabled = true;
		yield return null;
	}
}
