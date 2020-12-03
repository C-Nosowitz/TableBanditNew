using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCamera : MonoBehaviour
{
    public Transform player;
    Vector3 position;
    public float offset;

    // Update is called once per frame
    void Update()
    {
        position = new Vector3(player.position.x, transform.position.y, player.position.z + offset);
        transform.position = position;
    }
}
