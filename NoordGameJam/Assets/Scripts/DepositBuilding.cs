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

	private bool isLoading = false;
    public ResearchBuilding researchBuilding;

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
		if (State == BuildingState.Loading)
		{
			UpdateGalpão();
		}
	}
	override public void Interact(Player player)
	{
        interacting = true;
        ChangeState(player);
    }
	public override void WhileInteracting(Player player)
	{
        ChangeState(player);
	}
	public override void EndInteraction(Player player)
	{
		interacting = false;
        ChangeState(player);
	}


	void ChangeState(Player player)
    {
        if (State == BuildingState.UnderAttack || State == BuildingState.SavingFromAttack)
        {
            UpdateAttackState();
        }
        else
        {
			if(State == BuildingState.Loading) {
				UpdateGalpão();
			}
			else if (interacting && State == BuildingState.Idle)
            {
				isLoading = true;
				State = BuildingState.Loading;
                currentCollect += Time.deltaTime;
                player.depositResources(this); //Depositar todos os recursos
                GetComponent<SpriteRenderer>().color = Color.grey;
            }
        }
    }
	void UpdateGalpão() {
        currentCollect += Time.deltaTime;
        if (currentCollect >= collectRate)
        {
            State = BuildingState.Idle;
            currentCollect = 0;
			isLoading = false;
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

            GetComponent<SpriteRenderer>().color = Color.grey;
        }
    }

    public void ShipInteract(Ship ship)
    {
        State = BuildingState.Loading;
        nextCollect = Time.time + collectRate;
		ship.depositResources(researchBuilding);
        //ship.debugResources();
        ship.collectResources(this);
        //ship.debugResources();
        ship.startWaiting();

        GetComponent<SpriteRenderer>().color = Color.grey;
    }

    public void changeResource1(Resource res)
    {
        ResourceAmount1.text = "" + res.value;
    }

    public void changeResource2(Resource res)
    {
        ResourceAmount2.text = "" + res.value;
    }

    public void changeResource3(Resource res)
    {
        ResourceAmount3.text = "" + res.value;
    }

	public new void TryRemoveAttack()
    {
        if (attackCurrentTime >= attackSaveTime)
        {
			if(isLoading) {
				State = BuildingState.Loading;
                GetComponent<SpriteRenderer>().color = Color.grey;
			} else {
				State = BuildingState.Idle;
				GetComponent<SpriteRenderer>().color = Color.white;
            }
            onAttackFinished();
        }
    }
}
