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
        if (coll.gameObject.tag == "Enemy")
        {
            PlayerControll.Instance.Lose();
            coll.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            Destroy(coll.gameObject, 2);
        }
    }
    private void OnTriggerEnter(Collider coll)
    {       
        if (coll.gameObject.tag == "Dress")
        {
            SetDress(coll.gameObject.GetComponent<Dress>().DressId());
            coll.gameObject.SetActive(false);
        }
    }
}
