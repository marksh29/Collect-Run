using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : MonoBehaviour
{
    [SerializeField] GameObject[] girls;
    [SerializeField] float rotate;

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
        StartCoroutine(Rotate());
    }
    IEnumerator Rotate()
    {
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        while (t < rotate)
        {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / rotate) % 360.0f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
            yield return null;
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
            coll.gameObject.GetComponent<Box>().Drop(0);
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
