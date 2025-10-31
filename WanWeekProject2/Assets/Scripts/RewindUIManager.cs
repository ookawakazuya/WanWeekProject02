using UnityEngine;
using UnityEngine.UI;

public class RewindUIManager : MonoBehaviour
{
    [SerializeField] Text stateText;
    [SerializeField] CanvasGroup rewindOverlay; // �t�F�[�h���o�pCanvasGroup

    void Update()
    {
        var state = StageManager.Instance.CurrentState;

        if (state == StageManager.StageState.Rewinding)
        {
            stateText.text = " Rewinding...";
            rewindOverlay.alpha = Mathf.Lerp(rewindOverlay.alpha, 0.5f, Time.deltaTime * 3);
        }
        else
        {
            stateText.text = "Normal";
            rewindOverlay.alpha = Mathf.Lerp(rewindOverlay.alpha, 0f, Time.deltaTime * 3);
        }
    }
}
