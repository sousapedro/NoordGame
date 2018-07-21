﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public enum PlayerType{
        Colonia = 0,
        Metropole
    }

    public float Speed = 5f;
    public int playerId = 1;
    public PlayerType Type;
    private Rigidbody rb;

    private string HorizontalAxis = "Horizontal";
    private string VerticalAxis = "Vertical";
    private string ActionBtn;

    public List<Resource> MyResources;
	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody>();
        HorizontalAxis += playerId;
        VerticalAxis += playerId;
        
        if (playerId == 1)
            ActionBtn = "space";
        else if (playerId == 2)
            ActionBtn = "[0]";

        if (Type == PlayerType.Colonia)
        {
            MyResources.Add(new Resource("Madeira"));
            MyResources.Add(new Resource("Documentação"));
            MyResources.Add(new Resource("Açucar"));
        }
        else
        {
            MyResources.Add(new Resource("Ouro"));
            MyResources.Add(new Resource("Armas"));
            MyResources.Add(new Resource("Tecnologia"));
        }
	}
	
	// Update is called once per frame
	void Update () {
     //   print("" + KeyCode.Keypad0);
	}

    void FixedUpdate()
    {
        float moveHorizontal = 0;
        float moveVertical = 0;

        moveHorizontal = Input.GetAxis(HorizontalAxis);
        moveVertical = Input.GetAxis(VerticalAxis);

		Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);

        rb.velocity  = movement * Speed;


        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    foreach (Resource res in MyResources)
        //    {
        //        print(res.name + ":" + res.value);
        //    }
            
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(ActionBtn))
        {
            Building building = other.GetComponent<Building>();
			if(building != null) {
				building.Interact(this);
            }
           // GameObject GO = other.gameobject;
		} else if (Input.GetKey("space")) {
            Building building = other.GetComponent<Building>();
			if (building != null)
			{
				building.WhileInteracting(this);
			}
		} else {
            Building building = other.GetComponent<Building>();
			if (building != null)
			{
				building.EndInteraction(this);
			}
		}
    }

    public void collectResources(List<Resource> resources)
    {
        foreach (Resource myRes in MyResources)
	    {
            foreach (Resource otherRes in resources)
            {
                if (myRes.name == otherRes.name)
                {
                    myRes.modifyResource(otherRes.value);// 1;
                    otherRes.modifyResource(-otherRes.value);
                }
            }
	    }
    }

    public void depositResources(DepositBuilding depot)
    {
        List<Resource> resources = depot.ResourceList;

        foreach (Resource myRes in MyResources)
        {
            foreach (Resource otherRes in resources)
            {
                if (myRes.name == otherRes.name)
                {
                    otherRes.modifyResource(myRes.value);// 1;
                    //depot.ResourceAmount.Find(p => p[myRes.value
                    myRes.modifyResource(-myRes.value);
                }
            }
        }
    }
}
