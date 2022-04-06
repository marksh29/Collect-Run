using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoxFinish : MonoBehaviour
{
    public static BoxFinish Instance;
    [SerializeField] int nideBox, curBox;
    [SerializeField] TextMeshPro txt;
    void Start()
    {
        if (Instance == null)
            Instance = this;
        nideBox = PlayerControll.Instance.nideBoxToWin;
        txt.text = curBox + "/" + nideBox;
    }   
    public void AddBox()
    {
        curBox++;
        txt.text = curBox + "/" + nideBox;
    }
    public void End()
    {
        StartCoroutine(EndGame());
    }
    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1);
        if (curBox >= nideBox)
            Controll.Instance.Set_state("Win");
        else
            Controll.Instance.Set_state("Lose");
    }
}
