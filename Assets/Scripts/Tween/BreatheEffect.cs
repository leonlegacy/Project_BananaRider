using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BreatheEffect : MonoBehaviour
{
    public bool start = false;
    public bool loop = false;

    [SerializeField]
    private Text targetText;

    private Tween tween1,tween2;
    private Sequence sequence;
    // Start is called before the first frame update
    void Start()
    {
        targetText.color = new Color(targetText.color.r, targetText.color.g, targetText.color.b, 0);
        //LoopAnimation();
    }

    public void LoopAnimation()
    {
        targetText.color = new Color(targetText.color.r, targetText.color.g, targetText.color.b, 0);

        tween2 = targetText.DOFade(1, 1).SetEase(Ease.InCubic);
        tween2.OnComplete(CheckRePlay);

        start = true;
        loop = true;

        tween2.Play();
    }

    public void StopLoopAnimation()
    {
        if (tween1 != null)
        {
            tween1.Pause();
            tween1 = null;
        }
        if (tween2 != null)
        {
            tween2.Pause();
            tween2 = null;
        }
        loop = false;
        start = false;
        targetText.color = new Color(targetText.color.r, targetText.color.g, targetText.color.b, 0);
    }

    private void OnDisable()
    {
        if(tween1 != null)
        {
            tween1.Pause();
            tween1 = null;
        }
        if (tween2 != null)
        {
            tween2.Pause();
            tween2 = null;
        }
    }

    private void CheckRePlay()
    {
        if (loop)
        {
            if(targetText.color.a == 0)
            {
                tween2 = targetText.DOFade(1, 1).SetEase(Ease.InCubic);
                tween2.OnComplete(CheckRePlay);
            }
            else
            {
                tween1 = targetText.DOFade(0, 1).SetEase(Ease.OutCubic);
                tween1.OnComplete(CheckRePlay);
            }
        }
        else
        {
            start = false;
        }
    }
}
