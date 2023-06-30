using UnityEngine;

public class TaskTrigger : MonoBehaviour
{
    [SerializeField] TaskList taskList;
    [SerializeField] string taskName;
    public bool isCurrent { get; set; }

    public void TriggerTask()
    {
        if (isCurrent)
        {
            //taskList.SetTask(taskName);
        }
    }
}
