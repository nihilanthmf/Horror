using UnityEngine;

public class TaskObject : MonoBehaviour
{
    public TaskList taskList;
    public bool clickToComplete;

    public void TryCompletingTask()
    {
        taskList.Complete();
        Destroy(this);
    }
}
