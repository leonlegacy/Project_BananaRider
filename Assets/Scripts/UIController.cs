using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Image vehicleLife;

    [SerializeField]
    private Text timeText;

    private float maxLife;

    public void InitLifeBar(float maxLife)
    {
        this.maxLife = maxLife;
    }

    public void SetVehicleLife(float value)
    {
        vehicleLife.fillAmount = value/maxLife;
    }

    public void SetTimeText(int time)
    {
        int minute = time / 60;
        int second = time % 60;

        timeText.text = minute.ToString() + " : " + second.ToString();
    }
}
