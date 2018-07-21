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
        Galpão
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

	// Use this for initialization
	public void Start () {
	}
	
	// Update is called once per frame
	public void FixedUpdate () {      
        //if (Input.GetKeyDown(KeyCode.G) && (Type == BuildingTypes.Galpão))
        //{
        //    foreach (Resource res in ResourceList)
        //    {
        //        print(res.name + ":" + res.value);
        //    }

        //}
	}

	abstract public void Interact(Player player);
   
}
