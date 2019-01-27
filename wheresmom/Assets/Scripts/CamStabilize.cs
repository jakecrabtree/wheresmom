using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamStabilize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(0f, 0.225f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0f, 0.225f, 0f);
        transform.localRotation.Set(transform.localRotation.x, 0f, 0f, transform.localRotation.w);
    }
}
