using UnityEngine;

[CreateAssetMenu(fileName ="DungeonGenerationData.Asset", menuName ="DungeonGenerationData/DungeonData")]
public class DungeonGenerationData : ScriptableObject
{
    public int numberOfCrawlers;
    public int iterationMin;
    public int iterationMax;
}
