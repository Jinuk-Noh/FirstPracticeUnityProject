using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float h;
    public float v;

    public bool isJump;
    public bool isTouchLeft;
    public bool isTouchRight;

    bool isDie;

    CapsuleCollider2D playerCollider;
    Animator animator;

    public GManager manager;

    public bool[] joyControl;
    public bool isControl;
    public bool isButtonA;

    public bool check;

    public float coolTime;

    Rigidbody2D rigid;

    public bool isStageEnd;
    public bool isMove;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        coolTime += Time.deltaTime;
        Move();
        Jump();

    }

    private void FixedUpdate()
    {

        SuperPower();

    }

    //이동
    public void JoyPanel(int type)
    {
        for (int i = 0; i < 9; i++)
        {
            joyControl[i] = i == type;
        }
    }

    public void JoyDwon()
    {
        isControl = true;
    }

    public void JoyUp()
    {
        isControl = false;
    }

    private void Move()
    {
        if (isControl)
        {
            //JoyControl Value
            if (joyControl[0]) { h = -1; v = 1; }
            if (joyControl[1]) { h = 0; v = 1; }
            if (joyControl[2]) { h = 1; v = 1; }
            if (joyControl[3]) { h = -1; v = 0; }
            if (joyControl[4]) { h = 0; v = 0; }
            if (joyControl[5]) { h = 1; v = 0; }
            if (joyControl[6]) { h = -1; v = 0; }
            if (joyControl[7]) { h = 0; v = 0; }
            if (joyControl[8]) { h = 1; v = 0; }
        }
        else { h = 0; v = 0; }

        //키보드 입력시
        if (Input.GetButton("Horizontal"))
        {
            h = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
        }

        if (isDie == true)
            h = 0;

        //벽에 닿았을 때
        if ((isTouchLeft == true && h < 0) || (isTouchRight == true && h > 0))
            h = 0;

        
        // 좌우 반전
        if (h < 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
            
        else if (h > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x)*(-1), transform.localScale.y, 1);

        rigid.AddForce(Vector2.right * h * speed, ForceMode2D.Impulse);

        int maxSpeed = 3;
        // 오른쪽 최대 속도
        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        // 왼쪽 최대 속도
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }

        if (Input.GetButtonUp("Horizontal") && !isControl)
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.8f, rigid.velocity.y);
            h = 0;
        }


        //걷기 애니메이션
        if (animator.GetInteger("isWalk") != h)
            animator.SetInteger("isWalk", (int)h);
    }

    void Jump()
    {
        if (isButtonA)
            return;

        //Jump       
        if ((Input.GetKey(KeyCode.UpArrow) || (v == 1 && isControl)) && isJump == false)
        {
            JumpAni();
            rigid.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        }

    }

    private void SuperPower()
    {

        float xPoint = transform.position.x;
        float yPoint = transform.position.y;

        if (Input.GetButton("Jump"))
            isButtonA = true;


        if (coolTime < 2)
        {
            isButtonA = false;
            return;
        }

        if (isControl && isButtonA)
        {
            if (h > 0)
                xPoint = xPoint + 1.5f;
            else if (h < 0)
                xPoint = xPoint - 1.5f;

            //x좌표 최대 값 치솟 값
            if (xPoint < -2.5)
                xPoint = -2.5f;
            else if (xPoint > 2.5)
                xPoint = 2.5f;

            if (v > 0)
            {
                yPoint += 2.5f;
                isJump = true;
            }
            if (yPoint > 3.5)
                yPoint = 3.5f;

            coolTime = 0;

        }
        else if (isButtonA && Input.GetButton("Horizontal"))
        {
            //x 좌표 순간이동 거리                                       
            if (h > 0)
                xPoint = xPoint + 1.5f;
            else if (h < 0)
                xPoint = xPoint - 1.5f;

            //x좌표 최대 값 치솟 값
            if (xPoint < -2.2)
                xPoint = -2.2f;
            else if (xPoint > 2.2)
                xPoint = 2.2f;

            isButtonA = false;
            coolTime = 0;


        }
        else if (Input.GetKey(KeyCode.UpArrow) && isButtonA)
        {
            yPoint += 2.5f;
            if (yPoint > 3.5)
                yPoint = 3.5f;

            isJump = true;
            coolTime = 0;
            isButtonA = false;
        }
        transform.position = new Vector3(xPoint, yPoint, 0);

    }

    public void ButtonADown()
    {
        isButtonA = true;
    }

    public void ButtonUp()
    {
        isButtonA = false;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block" && isJump == true)
        {
            if (gameObject.transform.position.x > collision.gameObject.transform.position.x)
                isTouchLeft = true;
            else if (gameObject.transform.position.x < collision.gameObject.transform.position.x)
                isTouchRight = true;
        }

        if (collision.gameObject.tag == "TopBlock" && isJump == true)
        {

            if (collision.gameObject.name == "LastBlock(Clone)")
            {
                manager.StageEnd(isStageEnd);
                isStageEnd = true;
            }

            if (gameObject.transform.position.y > collision.gameObject.transform.position.y)
            {
                isJump = false;
                animator.SetBool("isJump", false);
            }
            else
            {
                if (gameObject.transform.position.x > collision.gameObject.transform.position.x)
                    isTouchLeft = true;
                else if (gameObject.transform.position.x < collision.gameObject.transform.position.x)
                    isTouchRight = true;
            }
        }

        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Left":
                    isTouchLeft = true;
                    break;

                case "Right":
                    isTouchRight = true;
                    break;
            }
        }

        if (collision.gameObject.tag == "Floor" && isJump == true)
        {
            isJump = false;
            animator.SetBool("isJump", false);

        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Left":
                    isTouchLeft = false;
                    break;

                case "Right":
                    isTouchRight = false;
                    break;
            }
        }

        if (collision.gameObject.tag == "Block" || collision.gameObject.tag == "TopBlock")
        {
            isTouchLeft = false;
            isTouchRight = false;
        }

        if (rigid.velocity.y < 0 && collision.gameObject.tag == "TopBlock" && !(transform.position.y < collision.gameObject.transform.position.y))
        {
            JumpAni();
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            OnDie(8);
            Invoke("IsGameOver", 1);
        }

        if (collision.gameObject.name == "Bottom")
        {
            OnDie(15);
            Invoke("IsGameOver", 1);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }


    void OnDie(int i)
    {
        isJump = true;
        isDie = true;
        playerCollider.enabled = false;
        rigid.AddForce(Vector2.up * i, ForceMode2D.Impulse);
        transform.localScale = new Vector3(-0.7f, -0.75f, 1);
        manager.isGameEnd = true;
        manager.Gameover();
    }

    void IsGameOver()
    {
        Time.timeScale = 0;
    }

    void JumpAni()
    {
        isJump = true;
        animator.SetBool("isJump", true);
    }

}

