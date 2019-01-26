using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	private static GameManager instance;

	public static GameManager Instance {
		get {
			return instance;
		}
	}
	Player player;
	Mom mom;
	void Awake () {
		if(instance != null && instance != this) {
			Destroy(this.gameObject);
		} else {
			instance = this;
		}
		mom = GameObject.FindGameObjectWithTag("Mom").GetComponent<Mom>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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

}
