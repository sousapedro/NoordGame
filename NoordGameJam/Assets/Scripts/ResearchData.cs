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
		colonyResearchs.Add(Research.NewColonyResearch(2, 2, 2));
		colonyResearchs.Add(Research.NewColonyResearch(4, 1, 2));
		colonyResearchs.Add(Research.NewColonyResearch(3, 3, 4));
		colonyResearchs.Add(Research.NewColonyResearch(5, 2, 1));
		colonyResearchs.Add(Research.NewColonyResearch(5, 3, 2));
		colonyResearchs.Add(Research.NewColonyResearch(1, 7, 3));
		colonyResearchs.Add(Research.NewColonyResearch(8, 3, 5));
        //colonyResearchs.Add(Research.NewColonyResearch(0, 0, 0));
        //colonyResearchs.Add(Research.NewColonyResearch(0, 0, 0));
        //colonyResearchs.Add(Research.NewColonyResearch(0, 0, 0));
        //colonyResearchs.Add(Research.NewColonyResearch(0, 0, 0));
        //colonyResearchs.Add(Research.NewColonyResearch(0, 0, 0));
        //colonyResearchs.Add(Research.NewColonyResearch(0, 0, 0));
        //colonyResearchs.Add(Research.NewColonyResearch(0, 0, 0));

        //gold   |   Guns    |    Tecnology
		metropolyResearchs.Add(Research.NewMetropolyResearch(3, 1, 1));
		metropolyResearchs.Add(Research.NewMetropolyResearch(2, 2, 2));
		metropolyResearchs.Add(Research.NewMetropolyResearch(2, 1, 3));
		metropolyResearchs.Add(Research.NewMetropolyResearch(4, 3, 2));
		metropolyResearchs.Add(Research.NewMetropolyResearch(2, 1, 5));
		metropolyResearchs.Add(Research.NewMetropolyResearch(4, 2, 3));
		metropolyResearchs.Add(Research.NewMetropolyResearch(4, 5, 4));
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