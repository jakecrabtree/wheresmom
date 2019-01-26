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
        if(Input.GetKeyDown(KeyCode.E)){
            RaycastHit hit;
            Ray ray = cam.ViewportPointToRay (new Vector3(0.5f,0.5f,0f));
            if (Physics.Raycast(ray, out hit)) {
                Transform objectHit = hit.transform;
                Interactable inter = objectHit.gameObject.GetComponent<Interactable>();
                if (inter != null){
                    inter.Interact();
                }
            }
        }
    }

    
}
