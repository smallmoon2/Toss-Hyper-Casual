using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stage1 : MonoBehaviour
{
    public StageManager stageManager;
    public Door_Screen door;
    private int level;
    private int touchCount = 0;
    private float playTime = 5;
    public  int maxTouches = 10;
    
    public GameObject StartPos;
    public GameObject EndPos;
    public GameObject Player;
    public Image prograssbar; 
    

    private Vector3 startPosition;
    private Vector3 endPosition;

    public void Start()
    {
        
        door.OpenDoor();
        level = stageManager.StageLevel;
        playTime = playTime - (0.7f * (float)level);
        startPosition = StartPos.transform.position;
        endPosition = EndPos.transform.position;

        Player.transform.position = startPosition;
        StartCoroutine(UpdateProgressBar());

    }

    public void Update()
    {

        // 마우스 클릭 (에디터/PC)
        if (Input.GetMouseButtonDown(0))
        {
            HandleTouch();
        }

        // 터치 (모바일)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            HandleTouch();
        }
    }

    private void HandleTouch()
    {
        if (touchCount >= maxTouches) return;

        touchCount++;
        float t = touchCount / (float)maxTouches;
        Vector3 targetPos = Vector3.Lerp(startPosition, endPosition, t);
        StartCoroutine(MoveToPosition(targetPos, 0.3f)); // 0.3초에 걸쳐 이동
    }

    private IEnumerator MoveToPosition(Vector3 target, float duration)
    {

        Vector3 initialPos = Player.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            Player.transform.position = Vector3.Lerp(initialPos, target, t);
            yield return null;
        }

        Player.transform.position = target;

        if (touchCount >= maxTouches && Vector3.Distance(target, endPosition) < 0.01f)
        {
            MissionClear(); 
        }

    }

    private IEnumerator UpdateProgressBar()
    {
        prograssbar.fillAmount = 1f;

        while (prograssbar.fillAmount > 0f)
        {
            prograssbar.fillAmount -= Time.deltaTime / playTime;
            yield return null;
        }

        prograssbar.fillAmount = 0f;

        Debug.Log("Game Over");
    }

    public void MissionClear()
    {
        if (touchCount >= maxTouches)
        {
            Debug.Log("Mission Clear!");
            StopAllCoroutines();               // 진행바 멈추기
            prograssbar.fillAmount = 1f;       // 바 꽉 채우기 (시각적으로 성공 느낌)
            door.CloseDoor();                  // 문 닫기 연출
            enabled = false;                   // 더 이상 Update() 안 돌도록
        }
    }


}
