using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour {

    public enum BuildingTypes
    {
        Madeira = 0,
        Documentação,
        Açucar,
        Ouro,
        Armas,
        Tecnologia,
        Galpão,
        Research
    }

    public enum BuildingState
    {
        Idle = 0,
        UnderAttack,
        SavingFromAttack,
        Loading,
        Generating,//Generating the resource
        Ready// Ready to be collected
    }

    public BuildingTypes Type;
    public List<Resource> ResourceList;
    public BuildingState State = BuildingState.Idle;

    public float collectRate = 0.5F;
    public float nextCollect = 0.0F;

    public float attackRate = 0.5F;
    public float nextAttack = 0.0F;

	public float currentCollect = 0.0f;
	public float startCollect = 0.0f;
	public bool interacting = false;
    
	// Use this for initialization
	public void Start () {
	}
	
	// Update is called once per frame
	public void FixedUpdate () {      
        
	}

	abstract public void Interact(Player player);
	abstract public void WhileInteracting(Player player);
	abstract public void EndInteraction(Player player);
   
}
