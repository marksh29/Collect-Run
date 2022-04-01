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
    }
    IEnumerator Scale()
    {
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        while (t < destroyTime)
        {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / destroyTime) % 360.0f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
            yield return null;
        }
    }
}
