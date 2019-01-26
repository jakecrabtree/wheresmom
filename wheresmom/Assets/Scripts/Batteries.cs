using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batteries : Interactable
{
    void Start(){
        guiFileName = "battery";
    }
    public override void Interact(GameObject interactor){
        Player player = GameManager.Instance.PlayerObj;
        player.energy += 20;
        Destroy(gameObject);
    }
}
