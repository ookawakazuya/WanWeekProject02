using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 5f;
    public float gravity = -9.8f;
    public float jumpForce = 5f;

    [Header("カメラ設定")]
    public Transform cameraTransform;

    CharacterController controller;
    Vector3 velocity;
    float yVelocity;

    bool isGrounded;
    float theBottom = -5f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Rewind中は完全停止（位置はTimeRewind側が操作）
        if (TimeRewindManager.Instance != null && TimeRewindManager.Instance.IsRewinding)
        {
            // CharacterControllerがONだとTransformを戻せないのでOFF
            if (controller.enabled) controller.enabled = false;
            return;
        }
        else
        {
            // Rewind終了 → コントローラ復活
            if (!controller.enabled) controller.enabled = true;
        }

        HandleMovement();

        // 落下チェック
        if (transform.position.y < theBottom)
        {
            StageManager.Instance.OnPlayerFailed();
        }
    }

    void HandleMovement()
    {
        // 地面判定
        isGrounded = controller.isGrounded;

        if (isGrounded && yVelocity < 0)
            yVelocity = -2f;

        // 入力
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // カメラ方向に基づく移動ベクトル
        Vector3 moveDir = cameraTransform.forward * vertical + cameraTransform.right * horizontal;
        moveDir.y = 0f; // 上下移動はなし（FPSではないため）
        moveDir.Normalize();

        controller.Move(moveDir * moveSpeed * Time.deltaTime);

        // ジャンプ
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            yVelocity = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // 重力
        yVelocity += gravity * Time.deltaTime;
        velocity.y = yVelocity;

        controller.Move(velocity * Time.deltaTime);
    }
}
