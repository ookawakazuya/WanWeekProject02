using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Collider))]
public class RewindableObject : MonoBehaviour
{
    /// <summary>
    /// 位置と回転の履歴を格納するリスト
    /// </summary>
    List<bool> history = new List<bool>();

    [Header("点滅設定")]
    [SerializeField] private float visibleDuration = 2f;   // 表示されている時間
    [SerializeField] private float invisibleDuration = 2f; // 消えている時間
    [SerializeField] private bool startVisible = true;     // 初期状態


    [SerializeField] int maxHistory = 600;

    private Renderer rend;
    private Collider col;

    // 履歴を保存（表示状態をフレーム単位で記録）
    private List<bool> visibleHistory = new List<bool>();

    // 現在の状態管理
    private bool isVisible;
    private float timer;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();

        // 初期状態を設定
        isVisible = startVisible;
        ApplyVisibility(isVisible);
    }

    void Update()
    {
        //巻き戻し中は位置を再生、それ以外は位置を記録
        if (StageManager.Instance.CurrentState == StageManager.StageState.Rewinding)
            Rewind();
        else
            //通常プレイ中：点滅パターンを更新・記録
            Blink();
        Record();
    }
    /// <summary>
    /// 通常時の点滅挙動。
    /// 一定時間ごとに出現／消滅を切り替える。
    /// </summary>
    private void Blink()
    {
        timer += Time.deltaTime;

        if (isVisible && timer > visibleDuration)
        {
            isVisible = false;
            ApplyVisibility(false);
            timer = 0f;
        }
        else if (!isVisible && timer > invisibleDuration)
        {
            isVisible = true;
            ApplyVisibility(true);
            timer = 0f;
        }
    }

    /// <summary>
    /// 現在の表示状態を履歴に記録。
    /// </summary>
    private void Record()
    {
        visibleHistory.Insert(0, isVisible);
        if (visibleHistory.Count > maxHistory)
            visibleHistory.RemoveAt(visibleHistory.Count - 1);
    }

    /// <summary>
    /// 過去の表示状態を再生。
    /// </summary>
    private void Rewind()
    {
        if (visibleHistory.Count == 0) return;

        bool pastState = visibleHistory[0];
        ApplyVisibility(pastState);
        visibleHistory.RemoveAt(0);
    }

    /// <summary>
    /// 表示／非表示を適用（見た目と当たり判定両方）
    /// </summary>
    private void ApplyVisibility(bool visible)
    {
        if (rend != null) rend.enabled = visible;
        if (col != null) col.enabled = visible;
    }
}
