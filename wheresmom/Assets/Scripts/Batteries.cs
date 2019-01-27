using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batteries : Interactable
{
    public AudioClip shootSound;
    void Start(){
        guiFileName = "battery";
    }
    public override void Interact(GameObject interactor){
        Player player = GameManager.Instance.PlayerObj;
        AudioSource speaker = GameManager.Instance.SpeakerObj;
        speaker.PlayOneShot(shootSound, .5f);
        player.energy += 20;
        Destroy(gameObject);
    }
}
