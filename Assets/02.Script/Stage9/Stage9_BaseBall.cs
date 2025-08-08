using UnityEngine;

public class Stage9_BaseBall : MonoBehaviour
{
    public Rigidbody2D rb;
    public float pushForce = 20f;
    public float scaleSpeed = 0.5f;
    public float maxScale = 2f;

    public bool isClear = false;

    private Vector3 initialScale;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool isStopped = false;

    void Awake()
    {
        // 초기 위치, 회전, 스케일 저장
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;
    }

    void OnEnable()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        // 초기화
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        transform.localScale = initialScale;
        isStopped = false;
        isClear = false;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        rb.AddForce(Vector2.left * pushForce, ForceMode2D.Impulse);
    }

    void Update()
    {
        if (isStopped) return;

        if (transform.localScale.x < maxScale)
        {
            float scaleAmount = scaleSpeed * Time.deltaTime;
            transform.localScale += new Vector3(scaleAmount, scaleAmount, 0f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("충돌");

        if (collision.collider.CompareTag("Baby"))
        {
            isClear = true;
        }

        isStopped = true;
        gameObject.SetActive(false); // 삭제 대신 비활성화
    }
}
