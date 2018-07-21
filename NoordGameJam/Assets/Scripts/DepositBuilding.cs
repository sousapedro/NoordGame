using UnityEngine;
using System.Collections;

public class DepositBuilding : Building
{
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
    }

    // Update is called once per frame
    public new void FixedUpdate()
    {
        UpdateGalpão();
		//if (Input.GetKeyDown(KeyCode.G) && (Type == BuildingTypes.Galpão))
        //{
        //    foreach (Resource res in ResourceList)
        //    {
        //        print(res.name + ":" + res.value);
        //    }

        //}
    }

	override public void Interact(Player player)
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
	public override void WhileInteracting(Player player)
	{
		
	}
	public override void EndInteraction(Player player)
	{
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
}
