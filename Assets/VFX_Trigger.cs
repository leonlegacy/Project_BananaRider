using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_Trigger : MonoBehaviour
{
    public static VFX_Trigger current;

    public event System.Action SmallViewEvent;
    public event System.Action WaterViewEvent;
    public event System.Action ThirdPersonEvent;


    enum VisualType
    {
        SmallView,
        WaterView,
        ThirdPerson
    }

    [SerializeField] VisualType vfx;

    private void Awake()
    {
        current = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            switch (vfx)
            {
                case VisualType.SmallView:
                    SmallViewEvent?.Invoke();
                    break;

                case VisualType.ThirdPerson:
                    ThirdPersonEvent?.Invoke();
                    break;

                case VisualType.WaterView:
                    WaterViewEvent?.Invoke();
                    break;
            }
    }

    public void SetVfxType(int i)
    {
        
    } 
}
