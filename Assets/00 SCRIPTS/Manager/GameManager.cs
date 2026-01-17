using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerController player;
    public PlayerController Player => player;

    [SerializeField] private UIManager uIManager;
    public UIManager UIManager => uIManager;

    public float worldSpeed;
}
