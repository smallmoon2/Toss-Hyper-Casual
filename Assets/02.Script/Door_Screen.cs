using UnityEngine;

public class Door_Screen : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator�� Door ������Ʈ�� �����ϴ�!");
        }
    }

    public void OpenDoor()
    {
        Debug.Log("������");
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