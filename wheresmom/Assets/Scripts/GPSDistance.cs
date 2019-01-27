using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPSDistance : MonoBehaviour
{
    Material m_Material;
    // Start is called before the first frame update
    void Start()
    {
        m_Material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Player player = GameManager.Instance.PlayerObj;
        int dist = (int) Mathf.Floor(player.momDistance());
        if(player.on) {
            if(dist > 40) {
                m_Material.color = new Color(0.3366411f, 0.8396226f, 0.7878978f, 0.951569f);
            } else if(dist > 20) {
                m_Material.color = new Color(1f, 0.4172136f, 0f, 0.95f);
            } else {
                m_Material.color = new Color(1f, 0.03f, 0f, 0.95f);
            }
        } else {
            m_Material.color = new Color(0.6745283f, .67f, 0.67f, 0.5f);
        }
    }
}
