using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] Text countDownText;
    private float time;

    private void Start()
    {
        time = 3.5f;
    }

    void Update()
    {

        if (time <= 0)
        {
            time = 0;
            gameObject.SetActive(false);
        }
        else
        {
            time -= Time.deltaTime;
            countDownText.text = Mathf.RoundToInt(time).ToString();
        }
    }
}
