using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : Interactable
{   
    [SerializeField]
    private GameObject lookDir;

    // Start is called before the first frame update
    void Start()
    {
        guiFileName = "hidingspot";
    }

    // Update is called once per frame
    void Update()
    {  
        
    }

    public override void Interact(GameObject interactor){
        GetComponent<Collider>().enabled = false;
        GameManager.Instance.CrawlIntoHidingSpot(transform.position, lookDir.transform.position);
    }
}
