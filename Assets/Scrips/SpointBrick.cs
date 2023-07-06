using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpointBrick : MonoBehaviour
{
    public int width, height;
    public GameObject[] Listbrick;

    public Vector3 origin;
    public Transform mother;
    void Start()
    {
        for(int i = 0; i < width; i++)
        {
            for(int k = 0; k < height; k++)
            {
                int random = UnityEngine.Random.Range(0, 3);
                Vector3 position = new Vector3(transform.position.x + i *2, 0, transform.position.z + k);
                Instantiate(Listbrick[random], position, transform.rotation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
