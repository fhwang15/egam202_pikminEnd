using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Treasure : MonoBehaviour
{

    //Will function once given required amount of pikmins are attached ==> "Child"
    public NavMeshAgent treasureCarried;
    GameObject marker;

    //Depending on the collision with Treasure, the pikmin's state will change.

    //Two types of collision
    //1. Collision when the number is satisfied ==> it will switch the enum state into Carrying
    //2. Collision when the number is not staisfied  ==> it will switch the enum state into TryingToCarry

    //Nvm enum is for the polishing part ill do it later haha lol

    //The Treasure will become the parent fo the playerCharacter or...whatever I was touching
    //I will lose the Navmesh control over the playerCharaacter. so the NavMesh for PlayerCharacter will be enabled.
    //******* the Pikmin navmesh is in the pikmin.cs

    //right click on the treasure will change the enum of the ALL pikmin into IDLE.
    //




    // Start is called before the first frame update
    void Start()
    {
        
        marker = this.transform.GetChild(0).gameObject; //getting the Marker
        marker.SetActive(false); //Not seen when the pikmin is not selected.

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void activatedTreasure(bool activated)
    {
        if (activated)
        {
            marker.SetActive(true);
        }
        else if (!activated)
        {
            marker.SetActive(false);

        }
    }

}
