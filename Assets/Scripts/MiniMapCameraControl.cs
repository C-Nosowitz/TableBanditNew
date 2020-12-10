using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraControl : MonoBehaviour
{
    public Transform player;
    public Transform icon;
    Vector3 position;
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        position = new Vector3(player.position.x, player.position.y + offset, player.position.z);
        transform.position = position;
    }
}
