using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    public Sprite map;
    float mapMinX, mapMaxX, mapMinY, mapMaxY;
    Camera cam;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        cam = GetComponent<Camera>();

        mapMinX = map.bounds.min.x + 8.88f;
        mapMaxX = map.bounds.max.x - 8.88f;
        mapMaxY = map.bounds.max.y - 5;
        mapMinY = map.bounds.min.y + 5;
    }

    // Update is called once per frame
    void Update()
    {
        //clamps the position to inside the map sprite
        transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, mapMinX, mapMaxX), Mathf.Clamp(player.transform.position.y, mapMinY, mapMaxY), transform.position.z);
    }
}
