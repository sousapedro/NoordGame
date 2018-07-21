﻿using UnityEngine;
using System.Collections;

public class ResourceBuilding : Building
{
    // Use this for initialization
    public new void Start()
    {
		ResourceList.Add(new Resource(Type.ToString()));
    }

    // Update is called once per frame
    public new void FixedUpdate()
    {
        UpdateGatherings();
    }

	override public void Interact(Player player) {
		if (State == BuildingState.Idle)
        {
            State = BuildingState.Generating;
            nextCollect = Time.time + collectRate;

            GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else if (State == BuildingState.Ready)
        {
            State = BuildingState.Idle;
            foreach (Resource res in ResourceList)
            {
                res.modifyResource(1);
            }
            player.collectResources(ResourceList);

            GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (State == BuildingState.UnderAttack)
        {
            State = BuildingState.SavingFromAttack;
            nextAttack = Time.time + attackRate;

            GetComponent<SpriteRenderer>().color = Color.red;
        }
	}
    public override void WhileInteracting(Player player)
    {

	}
    public override void EndInteraction(Player player)
    {
    }

    void UpdateGatherings()
    {
        if (State == BuildingState.Generating && Time.time > nextCollect)
        {
            nextCollect = Time.time + collectRate;
            State = BuildingState.Ready;
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (State == BuildingState.SavingFromAttack && Time.time > nextAttack)
        {
            nextAttack = Time.time + attackRate;
            State = BuildingState.Idle;

            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
   
}