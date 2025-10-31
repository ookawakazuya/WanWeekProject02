using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StageManager.Instance.OnStageCleared();
        }
    }
}
