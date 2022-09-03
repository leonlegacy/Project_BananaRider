using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField]
    private Text txt;

    public void Show(bool isPass)
    {
        if (isPass)
        {
            txt.text = "Pass";
        }
        else
        {
            txt.text = "Fail";
        }

        gameObject.SetActive(true);
    }
}
