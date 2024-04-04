using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public GameObject Player;
    private GameObject Current_Spawn;
    public GameObject[] Spawns;

    public static int index = 0;

    [SerializeField] int NumRooms;
    
    //public GameObject DDR_Spawn;

   


    // Start is called before the first frame update
    void Start()
    {
        //For initial testing, we will set the spawn to the display room
        Current_Spawn = Spawns[index];
        resetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        //If player fell through the map y < -1
        if(Player.transform.position.y <-1)
            resetPosition();
    }

    public void resetPosition(){
        Player.transform.position = Current_Spawn.transform.position;
        Player.transform.rotation = Current_Spawn.transform.rotation;
        //Debug.Log("Reseting");
        //Player.transform.LookAt();
    }

    public void previousRoom(){
        if(index != 0){
            index --;
            Current_Spawn = Spawns[index];
            //Debug.Log("The current index is" + index);
        }
    }

    public void previousRoomTP(){
        if(index != 0){
            index --;
            Current_Spawn = Spawns[index];
            //Debug.Log("The current index is" + index);
            resetPosition();
        }
    }

    public void nextRoom(){
        if(index != NumRooms - 1){
            index ++;
            Current_Spawn = Spawns[index];
            //Debug.Log("The current index is" + index);
        }
    }

    public void nextRoomTP(){
        if(index != NumRooms - 1){
            index ++;
            Current_Spawn = Spawns[index];
            //Debug.Log("The current index is" + index);
            resetPosition();
        }
    }

    public int roomNum(){
        return NumRooms;
    }
}
