using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerEnergy))]
[RequireComponent(typeof(IPlayerInput))]

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private IPlayerInput input;
    public IPlayerInput Input => input;
    [SerializeField] private bool isPlayerBoosting;
    public bool IsPlayerBoosting
    {
        get => isPlayerBoosting;
        set => isPlayerBoosting = value;
    }

    [SerializeField] private float moveSpeed;
    [SerializeField] private float boostSpeed = 1f;
    public float BoostSpeed
    {
        get => boostSpeed;
        set => boostSpeed = value;
    }

    [SerializeField] private float minEnergyToBoost = 0.2f;
    private PlayerEnergy playerEnergy;

    // Cờ chặn boost cho đến khi người chơi thả phím Space
    private bool blockedBoostUntilRelease;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<IPlayerInput>();
        playerEnergy = GetComponent<PlayerEnergy>();

        if (input == null)
            Debug.LogWarning($"{nameof(PlayerController)}: No PlayerInput found on {gameObject.name}");
    }

    private void FixedUpdate()
    {
        if (input == null) return;

        HandleMovement();
        HandleBoosting();
    }

    #region Movement
    private void HandleMovement()
    {
        rb.linearVelocity = input.Direction * moveSpeed;    //.linearVelocity là chuẩn hóa mới cơ bản ko khác gì .velocity
    }
    #endregion

    #region Boosting
    private void HandleBoosting()
    {
        // Quyết định isPlayerBoosting dựa trên ý muốn (Nhấn Space) + đủ năng lượng
        bool wantsBoost = input.IsBoostButtonDown;
        bool hasEnergy = playerEnergy.CurrentEnergy >= minEnergyToBoost;

        // Nếu đang giữ Space mà hết năng lượng => chặn boost cho đến khi thả Space
        if (wantsBoost && !hasEnergy)
        {
            blockedBoostUntilRelease = true;
            isPlayerBoosting = false;
            ExitBoost();
            return;
        }

        // Nếu người chơi thả Space => bỏ chặn, cho phép nhấn lại để boost
        if (!wantsBoost && blockedBoostUntilRelease)
        {
            blockedBoostUntilRelease = false;
        }

        // Chỉ cho boost khi:
        // - người chơi đang muốn boost (wantsBoost)
        // - đủ năng lượng (hasEnergy)
        // - không bị chặn do chưa thả phím (boostBlockedUntilRelease == false)
        isPlayerBoosting = wantsBoost && hasEnergy && !blockedBoostUntilRelease;

        if (isPlayerBoosting)
            EnterBoost();
        else
            ExitBoost();
    }

    private void EnterBoost()
    {
        boostSpeed = 5f;
    }

    private void ExitBoost()
    {
        boostSpeed = 1f;
    }
    #endregion
}
