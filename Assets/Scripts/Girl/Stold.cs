using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stold : MonoBehaviour
{
    [SerializeField] float speedRot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * speedRot * Time.deltaTime);
    }
}
