using UnityEngine;

public class RewindRippleController : MonoBehaviour
{
    [SerializeField] Material rippleMaterial; // ��������Inspector�ŃZ�b�g
    [SerializeField] string shaderParam = "_Intensity";
    [SerializeField] float maxIntensity = 0.12f;
    [SerializeField] float transitionSpeed = 2f;

    float currentIntensity = 0f;
    bool rewinding;

    public void SetRewind(bool active)
    {
        rewinding = active;
    }

    void Update()
    {
        if (rippleMaterial == null) return; // �� null�h�~

        float target = rewinding ? maxIntensity : 0f;
        currentIntensity = Mathf.Lerp(currentIntensity, target, Time.deltaTime * transitionSpeed);
        rippleMaterial.SetFloat(shaderParam, currentIntensity);
    }
}
