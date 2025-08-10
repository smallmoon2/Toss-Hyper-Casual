using UnityEngine;

public class Stage9_BaseBall : MonoBehaviour
{
    public Stage9 stage9;
    public Rigidbody2D rb;
    public float pushForce = 20f;
    public float scaleSpeed = 0.5f;
    public float maxScale = 2f;

    public bool isClear = false;

    private Vector3 initialScale;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool isStopped = false;

    public bool babyTurn = true; // true면 Baby, false면 Player가 성공 조건

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

        SoundManager.Instance.PlayLoop("Fall");

        // 초기화
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        transform.localScale = initialScale;
        isStopped = false;
        isClear = false;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        Debug.Log(stage9.level);
        // Level별 Rigidbody2D 값 세팅
        switch (stage9.level)
        {
            case 1:
                rb.gravityScale = 0.2f;
                rb.mass = 1f;
                break;
            case 2:
                rb.gravityScale = 0.5f;
                rb.mass = 0.6f;
                break;
            case 3:
                rb.gravityScale = 0.7f;
                rb.mass = 0.5f;
                break;
            case 4:
                rb.gravityScale = 1f;
                rb.mass = 0.4f;
                break;
            default:
                rb.gravityScale = 0.2f;
                rb.mass = 1f;
                break;
        }

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

        if (babyTurn && collision.collider.CompareTag("Baby"))
        {
            isClear = true;
            babyTurn = false; // 다음엔 Player 차례
        }
        else if (!babyTurn && collision.collider.CompareTag("Player"))
        {
            isClear = true;
            babyTurn = true; // 다음엔 Baby 차례
        }

        isStopped = true;
        SoundManager.Instance.Stop();
        SoundManager.Instance.Play("Catch");
        gameObject.SetActive(false); // 삭제 대신 비활성화
    }
}
