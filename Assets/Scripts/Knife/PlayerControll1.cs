using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;
using Cinemachine;
public class PlayerControll1 : MonoBehaviour
{
    public static PlayerControll1 Instance;

    [Header("--------Options--------")]
    [SerializeField] bool redIsLose;
    public float addScale;
    [SerializeField] float moveSpeed, addMaxKnifeHeight,  maxKnifeHeight, camSpeed;

    [Header("--------Game--------")]
    [SerializeField] Rigidbody body;
    [SerializeField] Transform[] wall;
    [SerializeField] SkinnedMeshRenderer knife;

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    CinemachineTransposer cam;
    [SerializeField] CinemachineVirtualCamera camer;
    float nideCamPos;

    void Start()
    {
        cam = camer.GetCinemachineComponent<CinemachineTransposer>();
        nideCamPos = cam.m_FollowOffset.y;
        Instance = this;
        MeshChange();
    }
    void FixedUpdate()
    {
        if (Controll.Instance._state == "Game")
        {
            body.velocity = new Vector3(0, 0, 1) * moveSpeed;

            if (Input.GetMouseButtonDown(0))
            {
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
            if (Input.GetMouseButton(0))
            {
                secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                currentSwipe.Normalize();

                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) // swip up
                {
                    knife.SetBlendShapeWeight(0, knife.GetBlendShapeWeight(0) + (knife.GetBlendShapeWeight(0) + addScale < maxKnifeHeight ? addScale : maxKnifeHeight - knife.GetBlendShapeWeight(0)));
                    MeshChange();
                }
                if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) // swip down
                {
                    knife.SetBlendShapeWeight(0, knife.GetBlendShapeWeight(0) - (knife.GetBlendShapeWeight(0) - addScale > 0 ? addScale : knife.GetBlendShapeWeight(0)));
                    MeshChange();
                }
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
            //if (cam.m_FollowOffset.y < nideCamPos)
            //    cam.m_FollowOffset += new Vector3(0, cam.m_FollowOffset.y + camSpeed <= nideCamPos ? camSpeed : nideCamPos - cam.m_FollowOffset.y, 0);
            //else if(cam.m_FollowOffset.y > nideCamPos)
            //    cam.m_FollowOffset -= new Vector3(0, cam.m_FollowOffset.y - camSpeed >= 22 ? camSpeed : cam.m_FollowOffset.y - 22, 0);           
        }
    }

    void MeshChange()
    {
        //nideCamPos = 22 + (knife.GetBlendShapeWeight(0) * 0.25f);
        for (int i = 0; i < wall.Length; i++)
        {
            wall[i].localScale = new Vector3(wall[i].localScale.x, 3 + (knife.GetBlendShapeWeight(0) * 0.0512f), wall[i].localScale.z);
        }        
        //Mesh bakeMesh = new Mesh();
        //sovok.BakeMesh(bakeMesh);
        //var collider = sovok.GetComponent<MeshCollider>();
        //collider.convex = true;
        //collider.sharedMesh = bakeMesh;
        //collider.convex = false;
    }
    public void AddMaxHeight()
    {
        maxKnifeHeight += addMaxKnifeHeight;
        if (maxKnifeHeight > 100)
            maxKnifeHeight = 100;
    }
       
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Finish")
        {
            body.velocity = new Vector3(0, 0, 0);
            Controll.Instance.Set_state("Win");              
            moveSpeed = 0;
        }
        if (coll.gameObject.tag == "BoxKnife")
        {
            coll.gameObject.GetComponent<KnifeBox>().Slise();
        }
    }
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            if (redIsLose)
                Lose();
            coll.gameObject.GetComponent<Box>().Drop(0);
        }
    }
    public void Lose()
    {
        moveSpeed = 0;
        body.velocity = new Vector3(0, 0, 0);    
        Controll.Instance.Set_state("Lose");
    }
}
