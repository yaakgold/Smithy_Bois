using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int Width;
    public int Height;
    public int x;
    public int y;

    public int minSpawners = 3;
    public int maxSpawners;

    public List<GameObject> enemiesInRoom = new List<GameObject>();
    public List<ESpawner> eSpawners = new List<ESpawner>();

    private bool updatedDoors = false;

    public Room(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;

    public List<Door> doors = new List<Door>();


    void Start()
    {
        if(RoomController.instance == null)
        {
            Debug.Log("You Started in the Wrong Scene");
            return;
        }

        Door[] ds = GetComponentsInChildren<Door>();
        foreach (Door d in ds)
        {
            doors.Add(d);
            switch (d.doorType)
            {
                case Door.DoorType.left:
                    leftDoor = d;
                    break;
                case Door.DoorType.right:
                    rightDoor = d;
                    break;
                case Door.DoorType.top:
                    topDoor = d;
                    break;
                case Door.DoorType.bottom:
                    bottomDoor = d;
                    break;
            }
        }

        RoomController.instance.RegisterRoom(this);
    }

    private void Update()
    {
        if(name.Contains("End") && !updatedDoors)
        {
            RemoveUnconnectedDoors();
            updatedDoors = true;
        }

        if (enemiesInRoom.Count > 0)
        {
            //There are still enemies
            StartCoroutine(LockRoom());
        }
        else
        {
            ReAddDoors();
        }
    }

    public void ReAddDoors()
    {
        foreach (Door door in doors)
        {
            switch (door.doorType)
            {
                case Door.DoorType.left:
                    if (GetLeft() != null)
                        SetDoor(door.gameObject, true);
                    //door.gameObject.SetActive(false);
                    break;
                case Door.DoorType.right:
                    if (GetRight() != null)
                        SetDoor(door.gameObject, true);
                    //door.gameObject.SetActive(false);
                    break;
                case Door.DoorType.top:
                    if (GetTop() != null)
                        SetDoor(door.gameObject, true);
                    //door.gameObject.SetActive(false);
                    break;
                case Door.DoorType.bottom:
                    if (GetBottom() != null)
                        SetDoor(door.gameObject, true);
                    //door.gameObject.SetActive(false);
                    break;
            }
        }
    }

    public void RemoveUnconnectedDoors()
    {
        foreach (Door door in doors)
        {
            switch (door.doorType)
            {
                case Door.DoorType.left:
                    if (GetLeft() == null)
                        SetDoor(door.gameObject, false);
                        //door.gameObject.SetActive(false);
                    break;
                case Door.DoorType.right:
                    if (GetRight() == null)
                        SetDoor(door.gameObject, false);
                        //door.gameObject.SetActive(false);
                    break;
                case Door.DoorType.top:
                    if (GetTop() == null)
                        SetDoor(door.gameObject, false);
                        //door.gameObject.SetActive(false);
                    break;
                case Door.DoorType.bottom:
                    if (GetBottom() == null)
                        SetDoor(door.gameObject, false);
                        //door.gameObject.SetActive(false);
                    break;
            }
        }
    }

    private void SetDoor(GameObject door, bool active)
    {
        door.GetComponent<BoxCollider2D>().isTrigger = active;
        door.GetComponentInChildren<SpriteRenderer>().enabled = active;
    }

    IEnumerator LockRoom()
    {
        yield return new WaitForSeconds(1f);

        foreach (var door in doors)
        {
            door.GetComponent<BoxCollider2D>().isTrigger = false;
            door.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

    public Room GetRight()
    {
        if (RoomController.instance.DoesRoomExist(x + 1, y))
        {
            return RoomController.instance.FindRoom(x + 1, y);
        }
        return null;
    }
    public Room GetLeft()
    {
        if (RoomController.instance.DoesRoomExist(x - 1, y))
        {
            return RoomController.instance.FindRoom(x - 1, y);
        }
        return null;
    }
    public Room GetTop()
    {
        if (RoomController.instance.DoesRoomExist(x, y + 1))
        {
            return RoomController.instance.FindRoom(x , y + 1);
        }
        return null;
    }

    public Room GetBottom()
    {
        if (RoomController.instance.DoesRoomExist(x, y -1))
        {
            return RoomController.instance.FindRoom(x, y -1);
        }
        return null;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);

            if (eSpawners.Count < minSpawners)
                return;
            int randSpawners = Random.Range(minSpawners, Mathf.Min(eSpawners.Count, maxSpawners));
            for (int i = 0; i < randSpawners; i++)
            {
                ESpawner s = eSpawners[Random.Range(0, eSpawners.Count)];
                while (!s.canSpawn)
                {
                    s = eSpawners[Random.Range(0, eSpawners.Count)];
                }
                s.isChosen = true;
            }
        }
    }
}
