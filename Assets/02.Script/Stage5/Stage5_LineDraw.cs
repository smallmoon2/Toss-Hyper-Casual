using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer), typeof(EdgeCollider2D))]
public class Stage5_LineDraw : MonoBehaviour
{
    public float minDragDistance = 0.1f;
    public float maxPointInterval = 0.05f;
    private float pointAddTimer = 0f;

    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    private List<Vector3> points = new List<Vector3>();
    private bool isDragging = false;

    public int lineCount = 0;
    public bool isFinished = false;

    public float goalCheckDelay = 3f;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();

        lineRenderer.positionCount = 0;
        lineRenderer.startWidth = 0.4f;
        lineRenderer.endWidth = 0.4f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        edgeCollider.enabled = false;
    }

    void OnEnable()
    {
        // 상태 초기화
        lineCount = 0;
        isDragging = false;
        isFinished = false;

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
            StartDrawing(GetWorldPoint());
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
        isDragging = true;
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

        lineRenderer.positionCount = 0;
        edgeCollider.points = new Vector2[0];
        edgeCollider.enabled = false;
        points.Clear();


    }

    Vector3 GetWorldPoint()
    {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = 10f;
        return Camera.main.ScreenToWorldPoint(screenPoint);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Line"))
        {
            lineCount++;

        }
    }
}
