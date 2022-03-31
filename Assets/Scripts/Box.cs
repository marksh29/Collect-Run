using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public bool off;
    [SerializeField] Rigidbody body;
    [SerializeField] Box[] sosed;
    [SerializeField] float force, sosedForce;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void Drop(float forc)
    {
        if(!off)
        {
            off = true;
            gameObject.tag = "Untagged";
            body.isKinematic = false;
            Destroy(gameObject, 2);
            for (int i = 0; i < sosed.Length; i++)
            {
                sosed[i].Drop(sosedForce);
            }
            Impulse(forc == 0 ? force : forc);
        }       
    }
    void Impulse(float frc)
    {       
        body.AddForce(Vector3.forward * frc, ForceMode.Impulse);
    }   
}
