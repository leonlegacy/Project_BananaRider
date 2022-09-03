using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffect : MonoBehaviour
{
    public bool isSmallView = false;
    public Material smallViewMat;

    public Material waterViewMat;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (isSmallView)
        {
            Graphics.Blit(src, dst, smallViewMat);
        }
        else
        {
            Graphics.Blit(src, dst, waterViewMat);
        }
    }
}
