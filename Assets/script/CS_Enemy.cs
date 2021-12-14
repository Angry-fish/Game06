using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Enemy : MonoBehaviour
{
    public GameObject Boom;
    public GameObject[] Point;
    public Vector3[] myPoint;
    public int index;
    public Rigidbody2D myRigidbody2D;
    public SpriteRenderer mySpriteRenderer;

    public Color myColor;
    public float maxSpeed;
    public float moveForce;
    public Vector3 target;
    public Vector3 player;

    public int health;
    public int maxHealth;
    public float chaseTime;
    float chaseTimeLeft;
    public float Ctime;
    public float Cforce;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
        health = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform.position;
        /*
                int temp = Random.Range(0, 3);
                switch (temp)
                {
                    case 0:
                        ChangeColor(new Color(125, 0, 0));
                        break;
                    case 1:
                        ChangeColor(new Color(0, 125, 0));
                        break;
                    case 2:
                        ChangeColor(new Color(0, 0, 125));
                        break;
                }
         */
        Color TempColor = Color.white;
        TempColor.r = Random.Range(80f, 180f);
        TempColor.g = Random.Range(80f, 180f);
        TempColor.b = Random.Range(80f, 180f);
        ChangeColor(TempColor);
        for (int tempIndex = 0; tempIndex < Point.Length; tempIndex++)
        {
            myPoint[tempIndex] = Point[tempIndex].transform.position;
        }
        index = 0;
        target = myPoint[index];
        chaseTimeLeft = chaseTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (chaseTimeLeft > 0)
        {
            chaseTimeLeft -= Time.deltaTime;
        }
        else
        {
            chaseTimeLeft = chaseTime;
            ChangeTarget(Random.Range(0, 2));
        }

        if (health <= 0)
        {
            ChangeTarget(0);
        }

        if (Vector3.Distance(transform.position, target) > .8f)
        {
            Vector2 dis = target - transform.position;
            dis.Normalize();
            myRigidbody2D.AddForce(dis * moveForce);
        }
        else
        {
            ChangeTarget(Random.Range(0, 2));
        }

        if (myRigidbody2D.velocity.magnitude > maxSpeed)
        {
            Vector3 myspeed = myRigidbody2D.velocity;
            myspeed.Normalize();
            myspeed.z = 0;
            myRigidbody2D.velocity = myspeed * maxSpeed;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<CS_Po>())
        {

            Vector2 localPosition = transform.position;
            Vector2 dis = localPosition - other.collider.ClosestPoint(transform.position);
            dis.Normalize();
            //myRigidbody2D.AddForce(dis * other.gameObject.GetComponent<CS_Po>().hitForce);

            if (target == player)
            {
                health--;
            }
        }

        if (other.gameObject.GetComponent<CS_Enemy>())
        {
            if (other.gameObject.GetComponent<CS_Enemy>().myColor == myColor)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<CS_Player>().BOOM(other.collider.ClosestPoint(transform.position), myColor);
                CS_UI.instance.S += 5;
                CS_Camera.instance.CameraMove(Ctime, Cforce);
                Destroy(this.gameObject);
            }
        }

        if (other.gameObject.GetComponent<CS_Player>())
        {
            ChangeTarget(0);
        }

    }

    public void ChangeColor(Color newColor)
    {
        health = maxHealth;
        myColor = newColor;
        mySpriteRenderer.color = myColor;
    }

    void ChangeTarget(int i)
    {
        if (i == 0)
        {
            index++;
            if (index >= Point.Length) index = 0;
            target = myPoint[index];
        }
        else
        {
            target = player;
        }

    }
}
