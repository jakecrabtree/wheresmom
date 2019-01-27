using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSnacks : Interactable
{

    private static float increaseWalkSpeed = 10;
    private static float increaseRunSpeed = 10;
    private static float initialDuration = 1;
    private static float decayDuration = 2; 
    private static float decayStep = 0.1f;

    bool consumed = false;

    void Start(){
        guiFileName = "gusher";
    }

    public override void Interact(GameObject interactor){
        if (!consumed){
            consumed = true;
            StartCoroutine("SugarRush");
            GetComponent<Collider>().enabled = false;
            GetComponent<Renderer>().enabled = false;
        }
    }

    void Update(){

    }

    private IEnumerator SugarRush(){
        UnityStandardAssets.Characters.FirstPerson.FirstPersonController controller = GameManager.Instance.PlayerObj.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        controller.IncreaseSpeeds(increaseRunSpeed, increaseWalkSpeed);
        yield return new WaitForSeconds(initialDuration);
        float multiplier = decayStep/decayDuration;
        for (float i = 0; i <= decayDuration; i+= decayStep){
            controller.DecreaseSpeeds(multiplier*increaseRunSpeed, multiplier*increaseWalkSpeed);
            yield return new WaitForSeconds(decayStep);
        }
        Destroy(gameObject);
        yield return null;
    }
}
