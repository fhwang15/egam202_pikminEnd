using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Treasure : MonoBehaviour
{

    //Will function once given required amount of pikmins are attached ==> "Child"
    public NavMeshAgent treasureCarried;
    GameObject marker;



    public NavMeshSurface field;

    bool isDone;

 

    public int pikminRequired; //Can type it on it haha yay~
    public int pikminInserted;

    public pikmin[] requiredPikmin;

    //Depending on the collision with Treasure, the pikmin's state will change.

    //Two types of collision
    //1. Collision when the number is satisfied ==> it will switch the enum state into Carrying
    //2. Collision when the number is not staisfied  ==> it will switch the enum state into TryingToCarry


    //The Treasure will become the parent fo the playerCharacter or...whatever I was touching
    //I will lose the Navmesh control over the playerCharaacter. so the NavMesh for PlayerCharacter will be enabled.
    //******* the Pikmin navmesh is in the pikmin.cs

    //right click on the treasure will change the enum of the ALL pikmin into IDLE.



    // Start is called before the first frame update
    void Start()
    {
        isDone = false;

        pikminInserted = 0;

        requiredPikmin = new pikmin[pikminRequired];

        marker = this.transform.GetChild(0).gameObject; //getting the Marker
        marker.SetActive(false); //Not seen when the pikmin is not selected.

    }

    // Update is called once per frame
    void Update()
    {
        
        moveTreasure();

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseposition = Input.mousePosition;
            Ray worldRay = Camera.main.ScreenPointToRay(mouseposition);
            //Determine where the mouse is clicked

            if (Physics.Raycast(worldRay, out RaycastHit hitinfo))
            {
                treasureCarried.SetDestination(hitinfo.point);
            }
        }

    }

    //if inserted pikmin reaches the number of required number, the um... it will move haha :')

    void moveTreasure()
    {
        if(pikminRequired == pikminInserted)
        {
            for(int i = 0; i < requiredPikmin.Length; i++)
            {
                requiredPikmin[i].currentState = PikminStates.Carrying;

                if (i == requiredPikmin.Length - 1)
                {
                    field.BuildNavMesh();
                }
            }
        }

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
