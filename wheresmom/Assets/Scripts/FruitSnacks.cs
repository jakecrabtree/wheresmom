﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSnacks : Interactable
{
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    public override void Interact(GameObject interactor){
       // GameManager.Instance.PlayerObj.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().
        Destroy(gameObject);
    }
}
