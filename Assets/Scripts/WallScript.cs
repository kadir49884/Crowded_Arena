using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WallScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<NeutralControl>()?.ChangeRotation();
    }

    
    public void ReduceChangeOpacity()
    {
        gameObject.GetComponent<ChangeRenderingMode>()?.MakeFade();
        gameObject.GetComponent<Renderer>()?.material.DOFade(0.8f, 0.5f);
    }
    public void PlusChangeOpacity()
    {
        gameObject.GetComponent<ChangeRenderingMode>()?.MakeOpaque();
    }

}
