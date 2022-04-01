using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public bool off;
    [SerializeField] Rigidbody body;
    [SerializeField] Box[] sosed;
    [SerializeField] float force, destroyTime;
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
            for (int i = 0; i < sosed.Length; i++)
            {
                sosed[i].Drop(force);
            }
            Impulse(forc == 0 ? force : forc);
        }       
    }
    void Impulse(float frc)
    {       
        body.AddForce(new Vector3(Random.Range(-1f,1f), Random.Range(-1f, 1f), Random.Range(3f, 7f)) * frc, ForceMode.Impulse);
        StartCoroutine(ScaleUpAndDown());
    }

    IEnumerator ScaleUpAndDown()
    {
        float elapsedTime = 0;
        Vector3 initialScale = transform.localScale;
        while (elapsedTime < destroyTime)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0,0,0), (elapsedTime / destroyTime));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = initialScale;
        gameObject.SetActive(false);      
    }
}
