using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSnacks : Interactable
{

    private static float increaseWalkSpeed = 10;
    private static float increaseRunSpeed = 10;
    private static float initialDuration = 1;
    private static float decayDuration = 2; 

    bool consumed = false;

    public override void Interact(GameObject interactor){
        if (!consumed){
            consumed = true;
            StartCoroutine("SugarRush");
            GetComponent<Renderer>().enabled = false;
        }
    }

    void Update(){

    }

    private IEnumerator SugarRush(){
        UnityStandardAssets.Characters.FirstPerson.FirstPersonController controller = GameManager.Instance.PlayerObj.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        controller.IncreaseSpeeds(increaseRunSpeed, increaseWalkSpeed);
        yield return new WaitForSeconds(initialDuration);
        for (float i = 0; i <= decayDuration; i+= Time.deltaTime){
            //Mathf.Lerp(0, increaseRunSpeed, );
            controller.DecreaseSpeeds(Time.deltaTime, Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
        yield return null;
    }
}
