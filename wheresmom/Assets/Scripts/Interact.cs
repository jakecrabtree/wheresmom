using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main; 
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = cam.ViewportPointToRay (new Vector3(0.5f,0.5f,0f));
        if (Physics.Raycast(ray, out hit)) {
            Transform objectHit = hit.transform;
            Interactable inter = objectHit.gameObject.GetComponent<Interactable>();
            if (inter != null && inter.canInteract(gameObject)){
                string filepath = inter.getGuiFileName();
                if (filepath != null){
                    GameManager.Instance.LoadInteractImage(filepath);
                }
                if(Input.GetKeyDown(KeyCode.E)){
                    inter.Interact(gameObject);
                }   
            }
            else{
                GameManager.Instance.DisableInteractImage();
            }
            if (inter != null){
                Debug.Log(inter.getGuiFileName() + inter.canInteract(gameObject) );
            }
        }
        else{
            GameManager.Instance.DisableInteractImage();
        }
    }

    
    
}
