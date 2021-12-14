using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Boom : MonoBehaviour
{
    public GameObject[] point;
    public GameObject Z;
    public GameObject Po;
    public Color myColor;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void Init(Color c){
        myColor=c;

        for (int index = 0; index < point.Length; index++)
        {
            CS_Po tempPo = Instantiate(Po, point[index].transform.position, transform.rotation).GetComponent<CS_Po>();
            tempPo.dis = point[index].transform.position - Z.transform.position;
            tempPo.myColor = myColor;
            tempPo.Init();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
