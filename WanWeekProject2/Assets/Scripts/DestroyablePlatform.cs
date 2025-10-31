using UnityEngine;

public class DestroyablePlatform : MonoBehaviour
{
    [Header("崩れるまでの時間")]
    [SerializeField] float fallDelay = 2f;

    Rigidbody rb;
    Vector3 initialPosition;
    Quaternion initialRotation;

    bool isFalling = false;
    float timer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 最初は動かない（固定）
        rb.useGravity = false;
        rb.isKinematic = true;

        // 初期位置を保存（巻き戻しに必要）
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // Rewind中なら復元
        if (TimeRewindManager.Instance.IsRewinding)
        {
            ResetPlatform();
            return;
        }

        // プレイヤーが乗って崩れるモード
        if (isFalling)
        {
            timer += Time.deltaTime;
            if (timer >= fallDelay)
            {
                Fall();
            }
        }
    }

    void Fall()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.WakeUp(); // 眠り状態解除（物理開始）

        //静止状態だと重力が効かない対策：ほんの少し動かす
        rb.AddForce(Vector3.down * 0.1f, ForceMode.Impulse);
    }

    void ResetPlatform()
    {
        // 元の状態に戻す
        rb.isKinematic = true;
        rb.useGravity = false;
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        // カウントと状態リセット
        timer = 0;
        isFalling = false;
    }

    // プレイヤーが足場に乗ったら崩れカウント開始
    public void StartFallCountdown()
    {
        isFalling = true;
    }

}

