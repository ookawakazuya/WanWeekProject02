using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }
    public enum StageState
    {
        Playing,    //�ʏ�v���C��
        Rewinding, // �����߂蒆
        Cleared,    //�X�e�[�W�N���A
        Failed      //�Q�[���I�[�o�[ 
    }

    public StageState CurrentState { get; private set; } = StageState.Playing;

    [Header("�Q�Ƃ���I�u�W�F�N�g")]
    [SerializeField] PlayerController player;
    [SerializeField] TimeRewindManager rewindManager;

    private void Awake()
    {
        Instance = this;
    }

    public void OnStageCleared()
    {
        CurrentState = StageState.Cleared;
        SceneController.instance.GoToResult();
    }

    public void OnPlayerFailed()
    {
        CurrentState = StageState.Failed;
        SceneController.instance.GoToGameOver();
    }

    public void SetRewinding(bool enable)
    {
        CurrentState = enable ? StageState.Rewinding : StageState.Playing;
    }
}
