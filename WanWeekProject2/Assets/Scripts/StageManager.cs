using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }
    public enum StageState
    {
        Playing,    //通常プレイ中
        Rewinding, // 巻き戻り中
        Cleared,    //ステージクリア
        Failed      //ゲームオーバー 
    }

    public StageState CurrentState { get; private set; } = StageState.Playing;

    [Header("参照するオブジェクト")]
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
