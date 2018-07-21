using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class DepositBuilding : Building
{
    public enum LandType
    {
        Colonia,
        Metropole
    }

    public LandType landType;
    public Text ResourceAmount1;
    public Text ResourceAmount2;
    public Text ResourceAmount3;

    // Use this for initialization
    public new void Start()
    {
		base.Start();

        ResourceList.Add(new Resource("Madeira"));
        ResourceList.Add(new Resource("Documentação"));
        ResourceList.Add(new Resource("Açucar"));
        ResourceList.Add(new Resource("Ouro"));
        ResourceList.Add(new Resource("Armas"));
        ResourceList.Add(new Resource("Tecnologia"));

        if (landType == LandType.Colonia)
        {
            Resource res1 = ResourceList.Find(p => p.name == "Madeira");
            res1.registerOnUpdateCb(changeResource1);

            Resource res2 = ResourceList.Find(p => p.name == "Documentação");
            res2.registerOnUpdateCb(changeResource2);

            Resource res3 = ResourceList.Find(p => p.name == "Açucar");
            res3.registerOnUpdateCb(changeResource3);
        }
        else
        {
            Resource res1 = ResourceList.Find(p => p.name == "Ouro");
            res1.registerOnUpdateCb(changeResource1);

            Resource res2 = ResourceList.Find(p => p.name == "Armas");
            res2.registerOnUpdateCb(changeResource2);

            Resource res3 = ResourceList.Find(p => p.name == "Tecnologia");
            res3.registerOnUpdateCb(changeResource3);
        }
    }

    // Update is called once per frame
    public new void FixedUpdate()
    {
        UpdateGalpão();
		if (Input.GetKeyDown(KeyCode.G) && (Type == BuildingTypes.Galpão))
        {
            foreach (Resource res in ResourceList)
            {
                print(res.name + ":" + res.value);
            }

        }
    }

	override public void Interact(Player player)
    {
        if (State == BuildingState.Idle)
        {
            State = BuildingState.Loading;
            nextCollect = Time.time + collectRate;
            player.depositResources(this); //Depositar todos os recursos

            GetComponent<SpriteRenderer>().color = Color.blue;
        }
        if (State == BuildingState.UnderAttack)
        {
            State = BuildingState.SavingFromAttack;
            nextAttack = Time.time + attackRate;

            GetComponent<SpriteRenderer>().color = Color.red;
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


    public void Collect(Player player)
    {
        if (State == BuildingState.Idle)
        {
            State = BuildingState.Loading;
            nextCollect = Time.time + collectRate;
            player.collectResources(ResourceList);//Coletar todos os recursos

            GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

    public void ShipInteract(Ship ship)
    {
        if (State == BuildingState.Idle)
        {
            State = BuildingState.Loading;
            nextCollect = Time.time + collectRate;
            ship.depositResources(ResourceList);
            //ship.debugResources();
            ship.collectResources(this);
            //ship.debugResources();
            ship.startWaiting();

            GetComponent<SpriteRenderer>().color = Color.blue;
        }
        if (State == BuildingState.UnderAttack)
        {
            State = BuildingState.SavingFromAttack;
            nextAttack = Time.time + attackRate;

            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void changeResource1(Resource res)
    {
        ResourceAmount1.text = "x" + res.value;
    }

    public void changeResource2(Resource res)
    {
        ResourceAmount2.text = "x" + res.value;
    }

    public void changeResource3(Resource res)
    {
        ResourceAmount3.text = "x" + res.value;
    }

}
