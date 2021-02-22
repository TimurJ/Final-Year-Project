using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;// input the starting positons that you want the first room to initiate from
    public GameObject[] rooms; //input the different room gameobjects: index 0 --> LR, 1 --> LRB, 2 --> LRT, 3 --> LRTB
    public GameObject[] player; // input the player object
    public GameObject[] portal;

    // Direction and the disance room moves
    private int direction;
    public float moveAmount;

    //Time between room spawns
    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;

    //Level gen boundry
    public float minX;
    public float maxX;
    public float minY;
    public bool stopGeneration;// stops rooms from being generated

    public LayerMask room;
    private int downCounter;

    private void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);//chooses one of the starting positions that you input at random
        transform.position = startingPositions[randStartingPos].position;// sets this game objects position to the random position that was chosen
        Instantiate(rooms[0], transform.position, Quaternion.identity);// instantiates the first room at the randomly chosen position
        //Instantiate(player[0], transform.position, Quaternion.identity);// instantiates the player at the starting room position

        direction = Random.Range(1, 6);
    }

	private void Update()
	{
		if(timeBtwRoom <= 0 && stopGeneration == false)// checks for time between rooms anf if generation reached bottom
		{
            Move();
            timeBtwRoom = startTimeBtwRoom;
		}
		else
		{
            timeBtwRoom -= Time.deltaTime;
		}
	}

	private void Move()
	{
        if (direction == 1 || direction == 2) // Move Right
		{
			if (transform.position.x < maxX)//checks for level boundry
			{
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);// spawns random room from the array
                Instantiate(rooms[rand], transform.position, Quaternion.identity);// instantiate the room
                // stops rooms being spawned on top of each other
                direction = Random.Range(1, 6);
                if(direction == 3)// if the room spawned left then move it right
				{
                    direction = 2;
				}else if(direction == 4){// if the room spawned left then move down
                    direction = 5;
				}
			}
			else
			{
                direction = 5;// if boundry reached move down
			}
		}else if (direction == 3 || direction == 4) // Move Left
		{
            if( transform.position.x > minX)// checks for level boundry
			{
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);// spawns a random room 
                Instantiate(rooms[rand], transform.position, Quaternion.identity);// instantiate the room

                direction = Random.Range(3, 6);// stops the rooom being generated to the right 
			}
			else
			{
                direction = 5;// if boundry reached move down
			}
        }else if (direction == 5) // Move Down
		{
            downCounter++;

            if(transform.position.y > minY)// checks of for level boundry
			{
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);// casts a circle and checks for room type
                if(roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3)
				{
                    if(downCounter >= 2)// blocked room fix checks if the generator moved down twice and spawns room with all 4 openings
					{
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
					}
					else
					{
                        roomDetection.GetComponent<RoomType>().RoomDestruction();

                        int randBottomRoom = Random.Range(1, 4);
                        if (randBottomRoom == 2)
                        {
                            randBottomRoom = 1;
                        }
                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                    }
				}
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4);// only chooses a room with top opening so the is no block
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);// rooms can again spawn freely right and left
			}
			else
			{
                Instantiate(portal[0], transform.position, Quaternion.identity);
                stopGeneration = true;// bottom is reached and room generation stops
			}
        }
	} 
}
