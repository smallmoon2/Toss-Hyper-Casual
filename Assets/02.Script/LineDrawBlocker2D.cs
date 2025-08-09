using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer), typeof(EdgeCollider2D))]
public class LineDrawBlocker2D : MonoBehaviour
{
    public float minDragDistance = 0.1f;
    public float maxPointInterval = 0.05f;
    private float pointAddTimer = 0f;

    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    private List<Vector3> points = new List<Vector3>();
    private bool isDragging = false;

    public bool isFinished = false;
    public bool iscrash = false;
    public bool isGoalReached = false;  

    public float goalCheckDelay = 0.4f;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();

        lineRenderer.positionCount = 0;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        edgeCollider.enabled = false;
    }

    void OnEnable()
    {
        // 상태 초기화
        isDragging = false;
        isFinished = false;
        iscrash = false;
        isGoalReached = false;

        // 선과 점 초기화
        pointAddTimer = 0f;
        points.Clear();
        lineRenderer.positionCount = 0;
        edgeCollider.points = new Vector2[0];
        edgeCollider.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = GetWorldPoint();
            if (IsInStartRange(worldPos))
            {
                StartDrawing(worldPos);
            }
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            pointAddTimer += Time.deltaTime;

            if (pointAddTimer >= maxPointInterval)
            {
                AddPointIfNeeded(forceAdd: true);
                pointAddTimer = 0f;
            }
            else
            {
                AddPointIfNeeded(forceAdd: false);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndDrawing();
        }
    }

    void StartDrawing(Vector3 firstPoint)
    {
        SoundManager.Instance.PlayLoop("PenDrawing");
        isDragging = true;
        isGoalReached = false;
        pointAddTimer = 0f;
        points.Clear();
        lineRenderer.positionCount = 0;
        edgeCollider.enabled = false;

        AddPoint(firstPoint);
    }

    void AddPointIfNeeded(bool forceAdd = false)
    {
        Vector3 newPoint = GetWorldPoint();

        if (points.Count == 0 ||
            Vector3.Distance(points[points.Count - 1], newPoint) > minDragDistance ||
            forceAdd)
        {
            AddPoint(newPoint);
        }
    }

    void AddPoint(Vector3 point)
    {
        points.Add(point);
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, point);
    }

    void EndDrawing()
    {
        SoundManager.Instance.Stop();
        isDragging = false;
        isFinished = true;

        if (points.Count >= 2)
        {
            Vector2[] edgePoints = new Vector2[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                edgePoints[i] = new Vector2(points[i].x, points[i].y);
            }

            edgeCollider.points = edgePoints;
            edgeCollider.enabled = true;

            StartCoroutine(CheckGoalAfterDelay());
        }
    }

    IEnumerator CheckGoalAfterDelay()
    {
        yield return new WaitForSeconds(goalCheckDelay);

        if (!isGoalReached)
        {
            Debug.Log("Goal과 충돌하지 않아 선 제거됨");
            lineRenderer.positionCount = 0;
            edgeCollider.points = new Vector2[0];
            edgeCollider.enabled = false;
            points.Clear();
        }
        else
        {
            Debug.Log("Goal 충돌 성공!");
            lineRenderer.positionCount = 0;
            edgeCollider.points = new Vector2[0];
            edgeCollider.enabled = false;
            points.Clear();
        }
    }

    Vector3 GetWorldPoint()
    {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = 10f;
        return Camera.main.ScreenToWorldPoint(screenPoint);
    }

    bool IsInStartRange(Vector3 pos)
    {
        return (pos.x > 1f && pos.x < 3.5f && pos.y < -7f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            iscrash = true;
            Debug.Log("충돌 감지됨: Wall");
        }

        if (other.CompareTag("Goal"))
        {
            isGoalReached = true;
            Debug.Log("Goal 충돌 감지됨!");
        }
    }
}
