using System;
using System.Collections.Generic;

public class ResearchData
{
	List<Research> colonyResearchs;
	List<Research> metropolyResearchs;
	private int colonyIndex = 0;
	private int metropolyIndex = 0;

	public ResearchData()
	{
		colonyResearchs = new List<Research>();
		metropolyResearchs = new List<Research>();
		//wood  |   documents   |   sugar
		//colonyResearchs.Add(Research.NewColonyResearch(5, 5, 5));
		//colonyResearchs.Add(Research.NewColonyResearch(8, 8, 5));
		//colonyResearchs.Add(Research.NewColonyResearch(10, 10, 10));
		//colonyResearchs.Add(Research.NewColonyResearch(12, 12, 12));
		//colonyResearchs.Add(Research.NewColonyResearch(15, 15, 15));
		//colonyResearchs.Add(Research.NewColonyResearch(20, 20, 20));
		//colonyResearchs.Add(Research.NewColonyResearch(25, 25, 25));
        colonyResearchs.Add(Research.NewColonyResearch(0, 0, 0));
        colonyResearchs.Add(Research.NewColonyResearch(0, 0, 0));
        colonyResearchs.Add(Research.NewColonyResearch(0, 0, 0));
        colonyResearchs.Add(Research.NewColonyResearch(0, 0, 0));
        colonyResearchs.Add(Research.NewColonyResearch(0, 0, 0));
        colonyResearchs.Add(Research.NewColonyResearch(0, 0, 0));
        colonyResearchs.Add(Research.NewColonyResearch(0, 0, 0));

        //gold   |   Guns    |    Tecnology
		metropolyResearchs.Add(Research.NewMetropolyResearch(5, 5, 5));
		metropolyResearchs.Add(Research.NewMetropolyResearch(8, 8, 5));
		metropolyResearchs.Add(Research.NewMetropolyResearch(10, 10, 10));
		metropolyResearchs.Add(Research.NewMetropolyResearch(12, 12, 12));
		metropolyResearchs.Add(Research.NewMetropolyResearch(15, 15, 15));
		metropolyResearchs.Add(Research.NewMetropolyResearch(20, 20, 20));
		metropolyResearchs.Add(Research.NewMetropolyResearch(25, 25, 25));
	}

	public Research GetNextColonyResearch() {
		if(colonyIndex >= colonyResearchs.Count) {
			return null;
		}
		Research research = colonyResearchs[colonyIndex];
		colonyIndex++;
		return research;
	}
    public Research GetNextMetropolyResearch()
    {
		if (metropolyIndex >= metropolyResearchs.Count)
        {
            return null;
        }
		Research research = metropolyResearchs[metropolyIndex];
		metropolyIndex++;
        return research;
    }
	public int getSize() {
		return colonyResearchs.Count + metropolyResearchs.Count;
	}
}