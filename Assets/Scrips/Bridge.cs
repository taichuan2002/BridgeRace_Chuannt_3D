using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private GameObject bridge;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            bridge.SetActive(false);
        }
    }
}
