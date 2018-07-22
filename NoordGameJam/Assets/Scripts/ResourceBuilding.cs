using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResourceBuilding : Building
{
    public TimeBarResources timeBarResources;
    public Image ReadyIcon;

	private float iconTimeAnimation = 3f;
	private Vector3 iconTargetLocation;
	private float yOffset = 10f;
	private bool showIcon = false;
	private float currentTime = 0;

    // Use this for initialization
    public new void Start()
    {
		base.Start();
        iconTargetLocation = new Vector3(ReadyIcon.transform.position.x, ReadyIcon.transform.position.y + yOffset, ReadyIcon.transform.position.z);
		ResourceList.Add(new Resource(Type.ToString()));

        if (timeBarResources != null)
            timeBarResources.MaxTime = this.collectRate;

		HideReadyIcon();
        timeBarResources.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public new void FixedUpdate()
    {
        UpdateGatherings();
		UpdateIcon();
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
            timeBarResources.gameObject.SetActive(false);
			UpdateAttackState();
		}
		else {
			if(interacting) {
				if (State == BuildingState.Idle)
				{
					State = BuildingState.Generating;
					currentCollect = 0;

					if (timeBarResources != null) {
						timeBarResources.gameObject.SetActive(true);
						timeBarResources.Activate = true;
                    }
					//GetComponent<SpriteRenderer>().color = Color.blue;
				}
				else if (State == BuildingState.Ready)
				{
					State = BuildingState.Idle;
					foreach (Resource res in ResourceList)
					{
						res.modifyResource(1);
					}
					player.collectResources(ResourceList);

                    HideReadyIcon();
					timeBarResources.gameObject.SetActive(false);
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
                timeBarResources.gameObject.SetActive(false);

                ShowReadyIcon();
				//GetComponent<SpriteRenderer>().color = Color.green;
			} else if (State == BuildingState.Generating){
				currentCollect += Time.deltaTime;
			}
        }
    }

    public void ShowReadyIcon()
    {
		if(!isUnderAttack) {
			if (ReadyIcon != null)
			{
				print(iconTimeAnimation);
				var tempColor = ReadyIcon.color;
				tempColor.a = 1f;
				ReadyIcon.color = tempColor;
				showIcon = true;
			}
        }

    }

    public void HideReadyIcon()
    {
        if (ReadyIcon != null)
        {
            var tempColor = ReadyIcon.color;
            tempColor.a = 0f;
            ReadyIcon.color = tempColor;
			showIcon = false;
        }
    }

    public void UpdateIcon()
	{
		if(showIcon && !isUnderAttack) {
			float difference = Mathf.Abs(ReadyIcon.transform.position.y * .0001f);
			if (Mathf.Abs(ReadyIcon.transform.position.y - iconTargetLocation.y) <= difference) {
				yOffset *= -1;
				iconTargetLocation = new Vector3(ReadyIcon.transform.position.x, ReadyIcon.transform.position.y+yOffset, ReadyIcon.transform.position.z);
				currentTime = 0;
			}
			currentTime += Time.deltaTime;
			float step = currentTime / iconTimeAnimation;
			step = 1 + (--step) * step * step * step * step;
			ReadyIcon.transform.position = Vector3.MoveTowards(ReadyIcon.transform.position, iconTargetLocation, step);
		}

    }
   
}
