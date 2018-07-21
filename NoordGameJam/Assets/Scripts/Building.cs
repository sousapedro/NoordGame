using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    public enum BuildingTypes
    {
        Madeira = 0,
        Documentacao,
        Açucar,
        Ouro,
        Armas,
        Tecnologia
    }

    public enum BuildingState
    {
        Idle = 0,
        UnderAttack,
        Generating,//Generating the resource
        Ready// Ready to be collected
    }

    public BuildingTypes Type;
    public Resource OwnResource;
    public BuildingState State = BuildingState.Idle;

	// Use this for initialization
	void Start () {
        OwnResource = new Resource(Type.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		//if()
        {
            //float translation = Time. * 10;
        }
	}

    public void ModifyState()
    {
        if (State == BuildingState.Idle)
        {
            State = BuildingState.Generating;
        }
        else if (State == BuildingState.Ready)
        {
            State = BuildingState.Idle;
        }
    }
}
