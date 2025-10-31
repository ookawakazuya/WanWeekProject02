using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FrameData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 velocity;

    public FrameData(Vector3 pos, Quaternion rot, Vector3 vel)
    {
        position = pos;
        rotation = rot;
        velocity = vel;
    }
}

public class RecordableObject : MonoBehaviour
{
    Rigidbody rb;
    float maxTime;
    List<FrameData> records;

    public void Init(float duration)
    {
        maxTime = duration;
        rb = GetComponent<Rigidbody>();
        records = new List<FrameData>();
    }

    public void Record()
    {
        if (records.Count > Mathf.Round(maxTime / Time.fixedDeltaTime))
            records.RemoveAt(records.Count - 1);

        Vector3 vel = rb ? rb.linearVelocity : Vector3.zero;
        records.Insert(0, new FrameData(transform.position, transform.rotation, vel));
    }

    public void Rewind()
    {
        if (records.Count > 0)
        {
            FrameData data = records[0];
            transform.position = data.position;
            transform.rotation = data.rotation;

            if (rb)
            {
                rb.linearVelocity = data.velocity;
                rb.isKinematic = true;
            }

            records.RemoveAt(0);
        }
        else
        {
            if (rb) rb.isKinematic = false;
        }
    }
}
