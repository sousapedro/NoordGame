using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

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
    private float nextCollect = 0.0F;

    public float attackRate = 0.5F;
    private float nextAttack = 0.0F;

	// Use this for initialization
	void Start () {
        if (Type != BuildingTypes.Galpão)
            ResourceList.Add(new Resource(Type.ToString()));
        else
        {
            ResourceList.Add(new Resource("Madeira"));
            ResourceList.Add(new Resource("Documentação"));
            ResourceList.Add(new Resource("Açucar"));
            ResourceList.Add(new Resource("Ouro"));
            ResourceList.Add(new Resource("Armas"));
            ResourceList.Add(new Resource("Tecnologia"));
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Type == BuildingTypes.Galpão)
            UpdateGalpão();
        else
            UpdateGatherings();

        if (Input.GetKeyDown(KeyCode.G) && (Type == BuildingTypes.Galpão))
        {
            foreach (Resource res in ResourceList)
            {
                print(res.name + ":" + res.value);
            }

        }
	}

    void UpdateGatherings () {
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

    void UpdateGalpão()
    {
        if (State == BuildingState.Loading && Time.time > nextCollect)
        {
            nextCollect = Time.time + collectRate;
            State = BuildingState.Idle;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        else if (State == BuildingState.SavingFromAttack && Time.time > nextAttack)
        {
            nextAttack = Time.time + attackRate;
            State = BuildingState.Idle;

            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void CollectResources(Player player)
    {
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
                res.value += 1;
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

    public void DepositBuilding(Player player)
    {
        if (State == BuildingState.Idle)
        {
            State = BuildingState.Loading;
            nextCollect = Time.time + collectRate;
            player.depositResources(ResourceList); //Depositar todos os recursos

            GetComponent<SpriteRenderer>().color = Color.blue;
        }
        if (State == BuildingState.UnderAttack)
        {
            State = BuildingState.SavingFromAttack;
            nextAttack = Time.time + attackRate;

            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void CollectBuilding(Player player)
    {
        if (State == BuildingState.Idle)
        {
            State = BuildingState.Loading;
            nextCollect = Time.time + collectRate;
            player.collectResources(ResourceList);//Coletar todos os recursos

            GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }
}
