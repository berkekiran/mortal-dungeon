using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = GameObject.Find("Player").GetComponent<Transform>().position;
    }
}
