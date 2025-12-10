using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerController player;
    public PlayerController Player => player;


}
