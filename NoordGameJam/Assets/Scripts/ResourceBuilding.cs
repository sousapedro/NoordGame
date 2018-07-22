using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResourceBuilding : Building
{
    public TimeBarResources timeBarResources;
    public Image ReadyIcon;

    // Use this for initialization
    public new void Start()
    {
        base.Start();
		ResourceList.Add(new Resource(Type.ToString()));

        if (timeBarResources != null)
            timeBarResources.MaxTime = this.collectRate;

        HideReadyIcon();
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

                    if (timeBarResources != null)
                        timeBarResources.Activate = true;
					//GetComponent<SpriteRenderer>().color = Color.blue;
				}
				else if (State == BuildingState.Ready)
				{
                    if (myAudioSource != null)
                    {
                        myAudioSource.clip = myAudioClip;
                        myAudioSource.Play();
                    }
					State = BuildingState.Idle;
					foreach (Resource res in ResourceList)
					{
						res.modifyResource(1);
					}
					player.collectResources(ResourceList);

                    HideReadyIcon();
					//GetComponent<SpriteRenderer>().color = Color.white;
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

                ShowReadyIcon();
				//GetComponent<SpriteRenderer>().color = Color.green;
			} else if (State == BuildingState.Generating){
				currentCollect += Time.deltaTime;
			}
        }
    }

    public void ShowReadyIcon()
    {
        if (ReadyIcon != null)
        {
            var tempColor = ReadyIcon.color;
            tempColor.a = 1f;
            ReadyIcon.color = tempColor;
        }
    }

    public void HideReadyIcon()
    {
        if (ReadyIcon != null)
        {
            var tempColor = ReadyIcon.color;
            tempColor.a = 0f;
            ReadyIcon.color = tempColor;
        }
    }



   
}
