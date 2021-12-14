using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_Player : MonoBehaviour
{
    public Animator myAnimator;
    public Color shootColor;
    public Transform shootPoint; 
    public Color myColor;
    public GameObject eyeR;
    public GameObject eyeL;
    public Vector2 dis;
    public GameObject Po;

    public float shootTime;
    float shootTimeLeft;
    bool canShoot;

    public GameObject boom;
    float boomtime;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        ChangeColor();
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(CS_UI.instance.H<=0){
            if(Input.GetKeyDown(KeyCode.R)){
                 SceneManager.LoadScene("SampleScene");
            }
            return;
        } 


        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        v.z = 0;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, v);
        transform.rotation = rotation;

        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            canShoot = false;
            shootTimeLeft = shootTime;
            myAnimator.Play("shoot", 0, 0f);
            shootColor = myColor;
            CS_Po tempPo = Instantiate(Po, shootPoint.position, transform.rotation).GetComponent<CS_Po>();
            tempPo.dis = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            tempPo.myColor = myColor;
            tempPo.Init();
            ChangeColor();
        }
        if (shootTimeLeft > 0)
        {
            shootTimeLeft -= Time.deltaTime;
        }
        else
        {
            canShoot = true;
        }

        if (boomtime > 0)
        {
            boomtime -= Time.deltaTime;
        }

    }
    void ChangeColor()
    {
        int temp = Random.Range(0, 3);
        Color tempColor = new Color(0, 0, 0);
        switch (temp)
        {
            case 0:
                myColor = new Color(125, 0, 0);
                tempColor = new Color(255, 0, 0);
                break;
            case 1:
                myColor = new Color(0, 125, 0);
                tempColor = new Color(0, 255, 0);
                break;
            case 2:
                myColor = new Color(0, 0, 125);
                tempColor = new Color(0, 0, 255);
                break;
        }
        eyeR.GetComponent<SpriteRenderer>().color = tempColor;
        eyeL.GetComponent<SpriteRenderer>().color = tempColor;
    }

    public void BOOM(Vector3 p, Color c)
    {
        if (boomtime > 0) { return; }
        boomtime = 0.5f;
        Instantiate(boom, p, transform.rotation).GetComponent<CS_Boom>().Init(c);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<CS_Enemy>())
        {
            CS_UI.instance.BeHurt(5);
        }
        if (other.gameObject.GetComponent<CS_Po>())
        {
            CS_UI.instance.BeHurt(1);
        }
        
    }
}

