using UnityEngine;

public class Door_Screen : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator가 Door 오브젝트에 없습니다!");
        }
    }

    public void OpenDoor()
    {
        Debug.Log("문열림");
        if (animator != null)
        {
            animator.SetTrigger("Door Open");
        }
    }

    public void CloseDoor()
    {
        if (animator != null)
        {
            animator.SetTrigger("Door Close");
        }
    }
}