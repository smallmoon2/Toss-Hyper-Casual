using System.Collections;
using UnityEngine;

public class Stage1_Clear : MonoBehaviour
{
    public GameObject subway;
    private Vector3 startSubwayPos;
    public float moveSpeed = 5f;
    public float targetX = 20f;

    private bool hasStartedMoving = false;

    private void Awake()
    {
        startSubwayPos = subway.transform.position;
    }
    void OnEnable()
    {
        hasStartedMoving = false;
        subway.transform.position = startSubwayPos;
        if (subway != null && !hasStartedMoving)
        {
            hasStartedMoving = true;
            StartCoroutine(MoveSubwayRight());
        }
    }

    private IEnumerator MoveSubwayRight()
    {
        Vector3 startPos = subway.transform.position;
        Vector3 targetPos = new Vector3(targetX, startPos.y, startPos.z);

        while (Vector3.Distance(subway.transform.position, targetPos) > 0.01f)
        {
            subway.transform.position = Vector3.MoveTowards(subway.transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        subway.transform.position = targetPos;
    }
}
