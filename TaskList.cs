using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TaskList : MonoBehaviour
{
    Animator animator;
    [SerializeField] TMP_Text taskTitle;
    [HideInInspector] public bool isCompleted;
    UnityAction executeWhenCompleted;
    RectTransform rectTransform;


    private void Start()
    {
        animator = GetComponent<Animator>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Appear();
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            Disappear();
        }
    }

    void SetTask(string taskName)
    {
        TMP_Text taskInstance = Instantiate(taskTitle, transform);
        taskInstance.fontStyle = FontStyles.Normal;
        taskInstance.text = taskName;

        taskTitle = taskInstance;
    }

    public void SetTaskObjectPlacement(string taskName, GameObject objectToPlace, Collider objectPlace, UnityAction executeWhenCompleted)
    {
        SetTask(taskName);

        TaskObject taskObject = objectToPlace.AddComponent<TaskObject>();
        taskObject.taskList = this;

        PickableObject pickable = objectToPlace.GetComponent<PickableObject>();
        pickable.taskObject = taskObject;

        objectPlace.gameObject.layer = 10;

        this.executeWhenCompleted = executeWhenCompleted;
    }

    public void SetTaskClick(string taskName, GameObject objectToClick, UnityAction executeWhenCompleted)
    {
        SetTask(taskName);

        TaskObject taskObject = objectToClick.AddComponent<TaskObject>();
        taskObject.taskList = this;
        taskObject.clickToComplete = true;

        this.executeWhenCompleted = executeWhenCompleted;
    }


    public void Complete()
    {
        taskTitle.fontStyle = FontStyles.Strikethrough;
        if (executeWhenCompleted != null)
        {
            executeWhenCompleted.Invoke();
        }
    }

    void Appear()
    {
        animator.SetBool("ToAppear", true);
    }

    void Disappear()
    {
        animator.SetBool("ToAppear", false);
    }
}
