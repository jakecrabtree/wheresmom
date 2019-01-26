using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected string guiFileName;
    private string guiFolderName = "interact_gui";
    public abstract void Interact(GameObject interactor);

    public string getGuiFileName(){
        return (guiFileName == null) ? guiFileName : guiFolderName + Path.DirectorySeparatorChar + guiFileName;
    }
}
