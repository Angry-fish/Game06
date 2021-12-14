using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Camera : MonoBehaviour
{
    public static CS_Camera instance;
    Vector3 y;

    // Start is called before the first frame update
    void Start()
    {
        if (!instance) instance = this;

        y = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CameraMove(float time, float force)
    {
        StartCoroutine(CameraMove_IE(time, force));
    }

    IEnumerator CameraMove_IE(float time, float force)
    {
        Vector3 newy;
        float timeLeft = time;
        while (timeLeft > 0)
        {
            newy = y + Random.insideUnitSphere * force;
            newy.z = y.z;
            transform.position = newy;
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        transform.position = y;
    }
}
