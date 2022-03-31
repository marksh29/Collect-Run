using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dress : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] float speedRot;

    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        transform.GetChild(id).gameObject.SetActive(true);
    }
    private void Update()
    {
        transform.Rotate(Vector3.up * speedRot * Time.deltaTime);
    }
    public int DressId()
    {
        return id;
    }
}
