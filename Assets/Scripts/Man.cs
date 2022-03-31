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

    void Start()
    {
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
            PlayerControll.Instance.AddMan(gameObject);
            anim.SetTrigger("dance");
        }
    }
}
