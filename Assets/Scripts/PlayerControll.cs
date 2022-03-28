using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class PlayerControll : MonoBehaviour
{
    [Header("--------Options--------")]
    [SerializeField] float moveSpeed;
    public float boostSpeed, slowSpeed, boostTime, addScale;
    [SerializeField] SkinnedMeshRenderer sovok;
    [Header("--------Game--------")]
    [SerializeField] PathFollower path;

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    void Start()
    {
        path.speed = moveSpeed;
    }
    void FixedUpdate()
    {
        if (Controll.Instance._state == "Game")
        {
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
                    sovok.SetBlendShapeWeight(0, sovok.GetBlendShapeWeight(0) + (sovok.GetBlendShapeWeight(0) + addScale < 100 ? addScale : 100 - sovok.GetBlendShapeWeight(0)));
                    MeshChange();
                }
                if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) // swip down
                {
                    sovok.SetBlendShapeWeight(0, sovok.GetBlendShapeWeight(0) - (sovok.GetBlendShapeWeight(0) - addScale > 0 ? addScale : sovok.GetBlendShapeWeight(0)));
                    MeshChange();
                }
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }           
        }
    }

    void MeshChange()
    {
        Mesh bakeMesh = new Mesh();
        sovok.BakeMesh(bakeMesh);
        var collider = sovok.GetComponent<MeshCollider>();
        collider.convex = true;
        collider.sharedMesh = bakeMesh;
        collider.convex = false;
    }

    public void Boost()
    {
        path.speed = boostSpeed;
        StartCoroutine(SlowSpeed());
    }  
    IEnumerator SlowSpeed()
    {
        yield return new WaitForSeconds(boostTime);
        while (path.speed > moveSpeed)
        {
            path.speed -= slowSpeed;
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void Lose()
    {
        Controll.Instance.Set_state("Lose");
        path.speed = 0;      
    }
    public void Win()
    {
        Controll.Instance.Set_state("Win");
        path.speed = 0;
    }
}
