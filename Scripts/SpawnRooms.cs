using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRooms : MonoBehaviour
{
    public LayerMask whatIsRoom; // enables it to detect room layer only
    public LevelGeneration levelGen;

    // Update is called once per frame
    void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom); // checks if there are rooms nearby
        if (roomDetection == null && levelGen.stopGeneration == true)// cheks if there are no rooms and level generation has stopped
		{
            int rand = Random.Range(0, levelGen.rooms.Length);// picks a random room
            Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);// instantiates room
            Destroy(gameObject);
		}
    }
}
