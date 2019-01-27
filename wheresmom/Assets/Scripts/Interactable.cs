using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected string guiFileName;
    private static string guiFolderName = "interact_gui";

    public float maxInteractDistance = 6.0f;
    public abstract void Interact(GameObject interactor);

    public bool canInteract(GameObject interactor){
        return Vector3.Distance(interactor.transform.position, transform.position) <= maxInteractDistance;
    }

    public string getGuiFileName(){
        return (guiFileName == null) ? guiFileName : makeAbsPath(guiFileName);
    }

    public static string makeAbsPath(string relPath){
        return guiFolderName + Path.DirectorySeparatorChar + relPath;
    }
}
