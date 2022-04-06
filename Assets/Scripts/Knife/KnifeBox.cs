using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBox : MonoBehaviour
{
    [SerializeField] FlyBox[] box;
    void Start()
    {        
    }
    public void Slise()
    {
        PlayerControll1.Instance.AddMaxHeight();
        transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        for (int i = 0; i < box.Length; i++)
        {
            box[i].Fly();
        }
    }
}
