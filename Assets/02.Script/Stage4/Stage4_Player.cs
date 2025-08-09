using UnityEngine;
using System.Collections;

public class Stage4_Player : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Animator anim;
    public float surviveTime = 5f;

    private float jumpDelay = 1f;
    private bool isGround = false;
    private bool canJump = true; // 점프 쿨타임 여부
    public bool isCrash = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        anim.ResetTrigger("MyTrigger");
        isCrash = false;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isGround && canJump)
        {
            playerRb.AddForce(Vector2.up * 4f, ForceMode2D.Impulse);
            canJump = false;

            // 애니메이션에 IsJump 활성화
            SoundManager.Instance.Play("Jump");
            anim.SetBool("IsJump", true);

            StartCoroutine(JumpCooldown());
        }
    }

    IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(jumpDelay);
        canJump = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("착지");
            isGround = true;

            // 착지하면 IsJump 비활성화
            anim.SetBool("IsJump", false);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("공중");
            isGround = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Rope"))
        {
            if (isGround)
            {
                Debug.Log("줄에 걸림");
                isCrash = true;
            }
        }
    }
}
