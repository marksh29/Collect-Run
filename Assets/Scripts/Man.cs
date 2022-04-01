using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Animat
{
    dance, happy, hipHop, talk, idle2, idle, walk, stand
}
public class Man : MonoBehaviour
{    
    public Animat startAnimation;
    [SerializeField] Animator anim;
    [SerializeField] Man[] mans;
    [SerializeField] float force;
    Rigidbody body;
    bool drop;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        SetAnimation(startAnimation.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetAnimation(string name)
    {
        anim.SetTrigger(name);
    }
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            body.useGravity = true;
            PlayerControll.Instance.AddMan(gameObject);
            anim.SetTrigger("dance");            
            if(!drop)
                DropMans();
        }
    }
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            body.useGravity = true;
            PlayerControll.Instance.AddMan(gameObject);
            anim.SetTrigger("dance");
            if(!drop)
                DropMans();
        }
    }
    public void Drop(float forc)
    {
        GetComponent<BoxCollider>().enabled = false;
        drop = true;
        force = forc;
        body.AddForce(new Vector3(0, 0, -1) * forc, ForceMode.Impulse);        
        if (mans.Length > 0)
            DropMans();
    }
    void DropMans()
    {
        if (!drop)
        {
            for (int i = 0; i < mans.Length; i++)
            {
                mans[i].Drop(force + (force / 2f));
            }
            //mans = null;
        }        
    }
}
