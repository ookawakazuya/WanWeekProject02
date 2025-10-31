using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Collider))]
public class RewindableObject : MonoBehaviour
{
    /// <summary>
    /// �ʒu�Ɖ�]�̗������i�[���郊�X�g
    /// </summary>
    List<bool> history = new List<bool>();

    [Header("�_�Őݒ�")]
    [SerializeField] private float visibleDuration = 2f;   // �\������Ă��鎞��
    [SerializeField] private float invisibleDuration = 2f; // �����Ă��鎞��
    [SerializeField] private bool startVisible = true;     // �������


    [SerializeField] int maxHistory = 600;

    private Renderer rend;
    private Collider col;

    // ������ۑ��i�\����Ԃ��t���[���P�ʂŋL�^�j
    private List<bool> visibleHistory = new List<bool>();

    // ���݂̏�ԊǗ�
    private bool isVisible;
    private float timer;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();

        // ������Ԃ�ݒ�
        isVisible = startVisible;
        ApplyVisibility(isVisible);
    }

    void Update()
    {
        //�����߂����͈ʒu���Đ��A����ȊO�͈ʒu���L�^
        if (StageManager.Instance.CurrentState == StageManager.StageState.Rewinding)
            Rewind();
        else
            //�ʏ�v���C���F�_�Ńp�^�[�����X�V�E�L�^
            Blink();
        Record();
    }
    /// <summary>
    /// �ʏ펞�̓_�ŋ����B
    /// ��莞�Ԃ��Ƃɏo���^���ł�؂�ւ���B
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
    /// ���݂̕\����Ԃ𗚗��ɋL�^�B
    /// </summary>
    private void Record()
    {
        visibleHistory.Insert(0, isVisible);
        if (visibleHistory.Count > maxHistory)
            visibleHistory.RemoveAt(visibleHistory.Count - 1);
    }

    /// <summary>
    /// �ߋ��̕\����Ԃ��Đ��B
    /// </summary>
    private void Rewind()
    {
        if (visibleHistory.Count == 0) return;

        bool pastState = visibleHistory[0];
        ApplyVisibility(pastState);
        visibleHistory.RemoveAt(0);
    }

    /// <summary>
    /// �\���^��\����K�p�i�����ڂƓ����蔻�藼���j
    /// </summary>
    private void ApplyVisibility(bool visible)
    {
        if (rend != null) rend.enabled = visible;
        if (col != null) col.enabled = visible;
    }
}
