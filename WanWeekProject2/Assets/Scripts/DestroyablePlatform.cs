using UnityEngine;

public class DestroyablePlatform : MonoBehaviour
{
    [Header("�����܂ł̎���")]
    [SerializeField] float fallDelay = 2f;

    Rigidbody rb;
    Vector3 initialPosition;
    Quaternion initialRotation;

    bool isFalling = false;
    float timer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // �ŏ��͓����Ȃ��i�Œ�j
        rb.useGravity = false;
        rb.isKinematic = true;

        // �����ʒu��ۑ��i�����߂��ɕK�v�j
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // Rewind���Ȃ畜��
        if (TimeRewindManager.Instance.IsRewinding)
        {
            ResetPlatform();
            return;
        }

        // �v���C���[������ĕ���郂�[�h
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
        rb.WakeUp(); // �����ԉ����i�����J�n�j

        //�Î~��Ԃ��Əd�͂������Ȃ��΍�F�ق�̏���������
        rb.AddForce(Vector3.down * 0.1f, ForceMode.Impulse);
    }

    void ResetPlatform()
    {
        // ���̏�Ԃɖ߂�
        rb.isKinematic = true;
        rb.useGravity = false;
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        // �J�E���g�Ə�ԃ��Z�b�g
        timer = 0;
        isFalling = false;
    }

    // �v���C���[������ɏ���������J�E���g�J�n
    public void StartFallCountdown()
    {
        isFalling = true;
    }

}

