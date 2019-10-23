using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode()]
public class DeepFry : MonoBehaviour {
    public Material effectMaterial;
    public float brilloMin = 0.1f;
    public float brilloMax = 1f;
    public float sizeMin = 0.0001f;
    public float sizeMax = 0.001f;
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        effectMaterial.SetFloat("_Size", Mathf.Lerp(sizeMin,sizeMax,((1+Mathf.Cos(Time.time))/2)) );
        effectMaterial.SetFloat("_Brillo", Mathf.Lerp(brilloMin, brilloMax, ((1 + Mathf.Cos(Time.time)) / 2)));
        Graphics.Blit(source, destination, effectMaterial);
    }
}
