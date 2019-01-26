using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mom : Interactable
{

    [SerializeField]
    bool realMom = false;
    void Start()
    {
        guiFileName = "mom";
    }


    public override void Interact(GameObject interactor){
        //TODO: play animation
        if (realMom){
            GameManager.Instance.Win();
        }
    }
}
