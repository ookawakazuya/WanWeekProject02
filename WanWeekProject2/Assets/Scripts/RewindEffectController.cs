using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RewindEffectController : MonoBehaviour
{
    [Header("PostProcess References")]
    public Volume volume;

    ColorAdjustments colorAdjust;
    MotionBlur motionBlur;
    Vignette vignette;
    Bloom bloom;

    [Header("Effect Intensity")]
    public float colorIntensity = 0.3f;
    public float vignetteIntensity = 0.4f;
    public float blurIntensity = 0.6f;
    public float bloomIntensity = 0.8f;

    void Start()
    {
        volume.profile.TryGet(out colorAdjust);
        volume.profile.TryGet(out motionBlur);
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out bloom);

        // èâä˙èÛë‘ÉIÉt
        SetEffects(0);
    }

    public void EnableEffect()
    {
        SetEffects(1);
    }

    public void DisableEffect()
    {
        SetEffects(0);
    }

    void SetEffects(float t)
    {
        if (colorAdjust) colorAdjust.colorFilter.value = Color.Lerp(Color.white, Color.cyan, t * colorIntensity);
        if (motionBlur) motionBlur.intensity.value = t * blurIntensity;
        if (vignette) vignette.intensity.value = t * vignetteIntensity;
        if (bloom) bloom.intensity.value = t * bloomIntensity;
    }
}
