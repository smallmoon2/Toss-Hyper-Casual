using UnityEngine;

public class Door_Screen : MonoBehaviour
{
    private Animator animator => GetComponent<Animator>();

    private void Awake()
    {
        //animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator�� Door ������Ʈ�� �����ϴ�!");
        }
    }

    public void OpenDoor()
    {

        if (animator != null)
        {
            animator.SetTrigger("Door Open");
        }
        else
        {

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