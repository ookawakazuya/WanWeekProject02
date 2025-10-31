using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 地面が落下するオブジェクト
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class FallingRewindObject : MonoBehaviour
{
    private Rigidbody rb;
    private List<Vector3> positionHistory = new List<Vector3>();
    private int maxHistory = 600;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        var state = StageManager.Instance.CurrentState;

        if (state == StageManager.StageState.Rewinding)
        {
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            Rewind();
        }
        else
        {
            rb.useGravity = true;
            Record();
        }
    }

    void Record()
    {
        positionHistory.Insert(0, transform.position);
        if (positionHistory.Count > maxHistory)
            positionHistory.RemoveAt(positionHistory.Count - 1);
    }

    void Rewind()
    {
        if (positionHistory.Count == 0) return;
        transform.position = positionHistory[0];
        positionHistory.RemoveAt(0);
    }
}
