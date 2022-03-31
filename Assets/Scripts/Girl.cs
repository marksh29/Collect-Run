using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : MonoBehaviour
{
    [SerializeField] GameObject[] girls;

    void Start()
    {
        
    }
    public void SetDress(int id)
    {
        PlayerControll.Instance.girlAnim = girls[id].GetComponent<Animator>();
        for (int i = 0; i <  girls.Length; i++)
        {
            girls[i].SetActive(i == id ? true : false);
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            PlayerControll.Instance.GirlDrop();
        }
    }
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Enemy")
        {
            PlayerControll.Instance.Lose();
        }
        if (coll.gameObject.tag == "Dress")
        {
            SetDress(coll.gameObject.GetComponent<Dress>().DressId());
            coll.gameObject.SetActive(false);
        }
    }
}
