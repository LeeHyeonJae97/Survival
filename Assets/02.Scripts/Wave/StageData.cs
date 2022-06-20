using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageData
{
    public const string DIR_PATH = "StageData";
    public const string FILE_PATH = "stageData.json";

    [field: SerializeField] public Stage[] Stages { get; private set; }

    public StageData()
    {
        Stages = new Stage[StageFactory.Count];
        for (int i = 0; i < Stages.Length; i++) Stages[i] = new Stage();
    }

    public StageData(Stage[] stages)
    {
        this.Stages = stages;
    }
}
