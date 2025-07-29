using UnityEngine;

public class Stage9_BaseBall : MonoBehaviour
{
    public Rigidbody2D rb;
    public float pushForce = 20f;
    public float scaleSpeed = 0.5f;
    public float maxScale = 2f;

    public bool isClear = false;
    private Vector3 initialScale;
    private bool isStopped = false;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        rb.AddForce(Vector2.left * pushForce, ForceMode2D.Impulse);
        initialScale = transform.localScale;
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

        Destroy(gameObject);
    }
}
