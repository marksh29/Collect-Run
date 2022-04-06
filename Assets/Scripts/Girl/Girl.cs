using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : MonoBehaviour
{
    [SerializeField] GameObject[] girls;
    GameObject curGirl;
    [SerializeField] float rotate;

    void Start()
    {
        
    }
    public void SetDress(int id)
    {
        PlayerControll.Instance.girlAnim = girls[id].GetComponent<Animator>();
        for (int i = 0; i <  girls.Length; i++)
        {
            if(i == id)
            {
                girls[i].transform.rotation = Quaternion.Euler(0, 0, 0);
                girls[i].SetActive(true);
                curGirl = girls[i];
            }
            else
                girls[i].SetActive(false);
        }
        StartCoroutine(Rotate());
    }
    IEnumerator Rotate()
    {
        float startRotation = curGirl.transform.eulerAngles.y;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        while (t < rotate)
        {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / rotate) % 360.0f;
            curGirl.transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
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
