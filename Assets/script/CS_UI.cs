using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CS_UI : MonoBehaviour
{
    public static CS_UI instance; 
    public Animator myAnimator;
    public Text Score;
    public Text Health;
    public int S=0;
    public int H;
    // Start is called before the first frame update
    void Start()
    {
        if(!instance)instance=this;

        myAnimator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Score.text=S.ToString();
        Health.text=H.ToString();
    }

    public void BeHurt(int damage){
         H-=damage;
         if(H<0)H=0;

        myAnimator.Play("BeHurt",0,0f);
    }
}
