using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Settings/GameData", order = 1)]
public class GameData : ScriptableObject
{
    public string PlayerName;
    public string PlayerSurname;
    public string PlayerEmail;
    public string PlayerAgeRange;
    public string PlayerNationality;
    public string PlayerUsername;
    public bool[] levelCompleted;
    public int[] levelPercentage;
}
