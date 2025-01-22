using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Camera cam;
    [SerializeField] float scrollSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        cam.orthographicSize += Input.mouseScrollDelta.y * scrollSpeed;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        gameObject.transform.position = player.transform.position + new Vector3(0,0,-10);
    }
}
