using UnityEngine;
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
    
	void ChangeState(Player player) {
		if(State == BuildingState.UnderAttack || State == BuildingState.SavingFromAttack) {
			UpdateAttackState();
		}
		else {
			if(interacting) {
				if (State == BuildingState.Idle)
				{
					State = BuildingState.Generating;
					currentCollect = 0;
					
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
			}
        }
	}

    void UpdateGatherings()
    {
		if(!isUnderAttack) {
			if (State == BuildingState.Generating && currentCollect > collectRate)
			{
				State = BuildingState.Ready;
				GetComponent<SpriteRenderer>().color = Color.green;
			} else if (State == BuildingState.Generating){
				currentCollect += Time.deltaTime;
			}
        }
    }

   
}
