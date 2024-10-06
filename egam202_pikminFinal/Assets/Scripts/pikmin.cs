using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pikmin : MonoBehaviour
{
    public NavMeshAgent playerCharacter;
    GameObject marker;
    Renderer renderer;

    Color active;


    //What happens here?
    //1. Individual pikmin's ability = Marker activate / moving around / change color when selected
    //2. 

    //activate marker
    //using bool decide if it's gonna move or not but tlqkf how
    //if playercharacter is active, the marker is also active.
    //==> apply to not all pikmin but the only pikmin 
    // :'( 
    // movement
    //서순이 선택 (색변화/마커ON/이동권한부여) => ??
    //

    // Start is called before the first frame update
    
    void Start()
    {
        marker = GetComponentInChildren<GameObject>();
        marker.SetActive(false);
        active = Color.black;
    }

    // Update is called once per frame
    void Update()
    {  
        
    }


    //Once the Pikmin activation is true,
    public void activatedPikmin(bool activated)
    {
        if (activated)
        {
            marker.SetActive(true);
            this.renderer.material.color = active;
        }
        else
        {
            return;
        }

        
    }


}
