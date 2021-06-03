using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int Width;
    public int Height;
    public int x;
    public int y;

    void Start()
    {
        if(RoomController.instance == null)
        {
            Debug.Log("You Started in the Wrong Scene");
            return;
        }

        RoomController.instance.RegisterRoom(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0)); 
    }

    public Vector3 GetRoomCenter()
    {
        return new Vector3(x * Width, y * Height);
    }
}
