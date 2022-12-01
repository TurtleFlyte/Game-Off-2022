using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBG : MonoBehaviour
{
    public float speed, rotationSpeed;
    public float height, width, rotateAngle;

    void Update()
    {
        Vector3 pos = transform.position;

        float newY = Mathf.Sin(Time.time * speed);
        float newX = Mathf.Sin(Time.time * speed);
        float rotation = Mathf.Sin(Time.time * rotationSpeed);

        transform.position = new Vector3(pos.x + newX * width, pos.y + newY * height, pos.z);
        transform.Rotate(Vector3.forward,rotation * rotateAngle);
    }
}
