using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Rigidbody2D rigid;
    public int speed;
    public Player player;
    

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
       
    }

    private void Update()
    {
        moveObstacle();
        trueChild();
    }

    void moveObstacle()
    {
        rigid.velocity = Vector2.down * speed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Bottom")
        {
            for (int i = 0; i < gameObject.transform.childCount; i++) {
                Transform child = gameObject.transform.GetChild(i);
                if (child.tag == "Player")
                {
                    child.transform.SetParent(null);
                    break;
                }
            }

            if (gameObject.name == "LastBlock(Clone)")
                player.isStageEnd = false;
               
              
            gameObject.SetActive(false);
        }
            

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.transform.SetParent(transform);

       
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
            collision.transform.SetParent(null);

        

        
    }

    void trueChild()
    {
        if ((gameObject.transform.childCount>=6))
        {
            for (int i = 3; i < 6; i++)
            {
                Transform child = gameObject.transform.GetChild(i);
                if (gameObject.activeSelf&& !child.gameObject.activeSelf)
                    child.gameObject.SetActive(true);
            }
        }
        
    }
}
