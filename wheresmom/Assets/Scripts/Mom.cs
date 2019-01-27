using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mom : Interactable
{

    [SerializeField]
    bool realMom = false;
    void Awake()
    {
        maxInteractDistance = 2.0f;
        guiFileName = "mom";
    }


    public override void Interact(GameObject interactor){
        GameManager.Instance.CheckTug(realMom);
    }
}
