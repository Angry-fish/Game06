using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Po : MonoBehaviour
{
    public float hitForce;
    public float speed;
    public Color myColor;
    public Rigidbody2D myRigidbody2D;
    public SpriteRenderer mySpriteRenderer;
    public Animator myAnimator;
    public AudioSource myAudioSource;
    public GameObject Hit_P;
    public Vector2 dis;
    public GameObject player;

    public float Ctime;
    public float Cforce;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void Init()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myRigidbody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        myAudioSource=GetComponent<AudioSource>();

        //myColor = player.GetComponent<CS_Player>().shootColor;
        mySpriteRenderer.color = myColor;
        //dis = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        dis.Normalize();
        myRigidbody2D.velocity = dis * speed;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        myAudioSource.Play(0);

        Vector3 temp = other.collider.ClosestPoint(transform.position);
        Vector3 v = temp - transform.position;
        v.z = 0;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, v);
        transform.rotation = rotation;
        Instantiate(Hit_P, temp, transform.rotation);
        myAnimator.Play("hit", 0, 0f);
        if (other.gameObject.GetComponent<CS_Enemy>())
        {
            if (other.gameObject.GetComponent<CS_Enemy>().myColor == myColor)
            {
                CS_UI.instance.S++;
                CS_Camera.instance.CameraMove(Ctime,Cforce);
                Destroy(this.gameObject);
            }
            other.gameObject.GetComponent<CS_Enemy>().ChangeColor(myColor);
            Destroy(this.gameObject);
        }
        if (other.gameObject.GetComponent<CS_Po>())
        {
            if (other.gameObject.GetComponent<CS_Po>().myColor == myColor)
            {
                CS_UI.instance.S++;
                CS_Camera.instance.CameraMove(Ctime,Cforce);
                Destroy(this.gameObject);
            }
        }
    }
}
