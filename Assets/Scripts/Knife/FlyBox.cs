using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBox : MonoBehaviour
{
    Transform player;
    Rigidbody body;
    [SerializeField] float flySpeed, force, xx, yy, zz;
    bool move;
    Vector3 movePos;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        body = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(move)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, player.localScale.y, player.position.z), flySpeed * Time.deltaTime);
        }
    }
    public void Fly()
    {
        body.isKinematic = false;
        body.AddForce(new Vector3(xx, yy, zz) * force, ForceMode.Impulse);
        StartCoroutine(FlyOn());
    }
    IEnumerator FlyOn()
    {
        yield return new WaitForSeconds(1);
        body.useGravity = false;
        body.velocity = new Vector3(0, 0, 0);
        move = true;
    }  
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player" && move)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }   
}
