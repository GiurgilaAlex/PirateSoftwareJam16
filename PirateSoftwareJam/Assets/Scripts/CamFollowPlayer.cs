using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera cam;
    [SerializeField] private float sensitivity;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    [SerializeField] private float speed;
    private float targetZoom;

    // Start is called before the first frame update
    void Start()
    {
        targetZoom = cam.orthographicSize;
    }

    void Update()
    {
        targetZoom -= Input.mouseScrollDelta.y * sensitivity;
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        float newSize = Mathf.MoveTowards(cam.orthographicSize, targetZoom, speed * Time.deltaTime);
        cam.orthographicSize = newSize;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        gameObject.transform.position = player.transform.position + new Vector3(0,0,-10);
    }
}
