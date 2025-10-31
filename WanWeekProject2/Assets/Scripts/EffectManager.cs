using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }

    [SerializeField] Material worldMaterial;
    [SerializeField, Range(0, 1)] float colorBlend = 0f; // 0=ÉÇÉmÉNÉç,1=ÉJÉâÅ[

    void Awake()
    {
        Instance = this;
    }

    public void SetColorBlend(float value)
    {
        colorBlend = Mathf.Clamp01(value);
        worldMaterial.SetFloat("_Blend", colorBlend);
    }
}
