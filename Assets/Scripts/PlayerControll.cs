using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;
using Cinemachine;
public class PlayerControll : MonoBehaviour
{
    public static PlayerControll Instance;

    [Header("--------Options--------")]
    [SerializeField] float moveSpeed, camSpeed;
    public float addScale;

    [Header("--------Game--------")]
    [SerializeField] Rigidbody body;
    public Animator girlAnim;
    [SerializeField] Transform[] wall;
    [SerializeField] Transform girl;
    [SerializeField] SkinnedMeshRenderer sovok;
    [SerializeField] PathFollower path;
    [SerializeField] List<GameObject> manList;

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    [SerializeField] bool up;

    CinemachineTransposer cam;
    [SerializeField] CinemachineVirtualCamera camer;
    float nideCamPos;

    void Start()
    {
        cam = camer.GetCinemachineComponent<CinemachineTransposer>();
        nideCamPos = cam.m_FollowOffset.y;
        Instance = this;
        MeshChange();
        path.speed = moveSpeed;
    }
    void FixedUpdate()
    {
        if (Controll.Instance._state == "Game")
        {
            body.velocity = new Vector3(0, 0, 1) * moveSpeed;

            if (Input.GetMouseButtonDown(0))
            {
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                girlAnim.SetTrigger("loock");
            }
            if (Input.GetMouseButton(0))
            {
                secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                currentSwipe.Normalize();

                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) // swip up
                {
                    up = true;
                    sovok.SetBlendShapeWeight(0, sovok.GetBlendShapeWeight(0) + (sovok.GetBlendShapeWeight(0) + addScale < 100 ? addScale : 100 - sovok.GetBlendShapeWeight(0)));
                    MeshChange();

                    if(girl.localPosition.y < 2.25f + (sovok.GetBlendShapeWeight(0) * 0.2f))
                        girl.localPosition = new Vector3(girl.localPosition.x, 2.25f + (sovok.GetBlendShapeWeight(0) * 0.2f), girl.localPosition.z);
                    else
                        girl.localPosition = new Vector3(girl.localPosition.x, girl.localPosition.y, girl.localPosition.z);
                }
                if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) // swip down
                {
                    up = false;
                    sovok.SetBlendShapeWeight(0, sovok.GetBlendShapeWeight(0) - (sovok.GetBlendShapeWeight(0) - addScale > 0 ? addScale : sovok.GetBlendShapeWeight(0)));
                    MeshChange();
                }
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }

            if (cam.m_FollowOffset.y < nideCamPos)
                cam.m_FollowOffset += new Vector3(0, cam.m_FollowOffset.y + camSpeed <= nideCamPos ? camSpeed : nideCamPos - cam.m_FollowOffset.y, 0);
            else if(cam.m_FollowOffset.y > nideCamPos)
                cam.m_FollowOffset -= new Vector3(0, cam.m_FollowOffset.y - camSpeed >= 22 ? camSpeed : cam.m_FollowOffset.y - 22, 0);
            //if (Input.GetMouseButtonUp(0))
            //{
            //    girlAnim.SetTrigger("dance");
            //    if (!up)
            //    {
            //    }
            //    else
            //    {
            //    }
            //}
        }
    }

    void MeshChange()
    {
        nideCamPos = 22 + (sovok.GetBlendShapeWeight(0) * 0.25f);
        for (int i = 0; i < wall.Length; i++)
        {
            wall[i].localScale = new Vector3(wall[i].localScale.x, 3 + (sovok.GetBlendShapeWeight(0) * 0.4f), wall[i].localScale.z);
        }        
        //Mesh bakeMesh = new Mesh();
        //sovok.BakeMesh(bakeMesh);
        //var collider = sovok.GetComponent<MeshCollider>();
        //collider.convex = true;
        //collider.sharedMesh = bakeMesh;
        //collider.convex = false;
    }

    public void GirlDrop()
    {        
        if (!up)
        {          
            girlAnim.SetTrigger("landing");
        }
    }
   
    public void AddMan(GameObject obj)
    {
        manList.Add(obj);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Finish")
        {
            moveSpeed = 0;
            body.velocity = new Vector3(0, 0, 0);
            for (int  i = 0;  i < manList.Count;  i++)
            {
                manList[i].GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                manList[i].GetComponent<Man>().SetAnimation("happy");
            }
            Controll.Instance.Set_state("Win");
        }      
        if (coll.gameObject.tag == "Dress")
        {
            girl.GetComponent<Girl>().SetDress(coll.gameObject.GetComponent<Dress>().DressId());
            coll.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            coll.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            Destroy(coll.gameObject, 2);
        }
    }
    public void Lose()
    {
        moveSpeed = 0;
        body.velocity = new Vector3(0, 0, 0);
        girlAnim.SetTrigger("idle");
        for (int i = 0; i < manList.Count; i++)
        {
            manList[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            manList[i].GetComponent<Man>().SetAnimation("idle2");
        }
        Controll.Instance.Set_state("Lose");
    }
}
