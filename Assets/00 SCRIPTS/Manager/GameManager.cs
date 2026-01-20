using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float worldSpeed;

    [SerializeField] private PlayerController player;
    public PlayerController Player => player;

    private IPlayerInput input;

}
