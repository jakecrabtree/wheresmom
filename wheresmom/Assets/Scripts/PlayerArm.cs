using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArm : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1)) {
			anim.Play("Raise");  
		} 
        if(Input.GetMouseButtonUp(1)) {
            anim.Play("Lower");
        }
    }
}
