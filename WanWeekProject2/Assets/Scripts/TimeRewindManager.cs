using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 巻き戻しを発生させるマネージェーオブジェクト
/// </summary>
public class TimeRewindManager : MonoBehaviour
{
    public static TimeRewindManager Instance { get; private set; }

    [Header("時間制御設定")]
    [SerializeField] float recordDuration = 10f;         // 最大何秒記録するか
    [SerializeField] KeyCode rewindKey = KeyCode.R;

    [SerializeField] RewindEffectController rewindFx;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip rewindClip;


    public bool IsRewinding { get; private set; }

    List<RecordableObject> trackedObjects = new List<RecordableObject>();

    [SerializeField] RewindRippleController rippleFx;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void RegisterObject(RecordableObject obj)
    {
        trackedObjects.Add(obj);
        obj.Init(recordDuration);
    }

    void Update()
    {
        if (Input.GetKeyDown(rewindKey)) StartRewind();
        if (Input.GetKeyUp(rewindKey)) StopRewind();
    }

    void FixedUpdate()
    {
        if (IsRewinding)
        {
            foreach (var obj in trackedObjects) obj.Rewind();
        }
        else
        {
            foreach (var obj in trackedObjects) obj.Record();
        }
    }

    public void StartRewind()
    {
        IsRewinding = true;
        StageManager.Instance.SetRewinding(true);
        Debug.Log("巻き戻し開始");
        Time.timeScale = 0.1f; // 演出的に少し減速


        if (rewindFx) rewindFx.EnableEffect();
        rippleFx.SetRewind(true);
        rippleFx.SetRewind(false);
        if (audioSource && rewindClip)
        {
            audioSource.pitch = -1f;
            audioSource.PlayOneShot(rewindClip);
        }
    }

   public void StopRewind()
    {
        IsRewinding = false;
        StageManager.Instance.SetRewinding(false);
        Debug.Log("再生");
        Time.timeScale = 1f;

        if (rewindFx) rewindFx.DisableEffect();
        if (audioSource) audioSource.pitch = 1f;
    }
}
