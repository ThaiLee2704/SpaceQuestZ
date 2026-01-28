using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(IPlayerInput))]
[RequireComponent(typeof(PlayerEnergy))]
[RequireComponent(typeof(PlayerHealth))]

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private IPlayerInput input;
    public IPlayerInput Input => input;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float boostMultiplier = 5f;

    [SerializeField] private float minEnergyToBoost = 0.2f;
    private PlayerEnergy playerEnergy;

    [SerializeField] private bool isPlayerBoosting;
    public bool IsPlayerBoosting => isPlayerBoosting;

    // Trạng thái chết
    [SerializeField] private bool isDead;

    public float BoostSpeed => isDead ? 0f : (isPlayerBoosting ? boostMultiplier : 1f);

    // Cờ chặn boost cho đến khi người chơi thả phím Space
    private bool blockedBoostUntilRelease;

    private void OnEnable()
    {
        //Môi khi clone level thì đăng ký PlayerController trong level đó cho GameManager
        GameManager.Instant.RegisterPlayer(this);
    }

    private void Start()
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

        if (Time.timeScale > 0)
        {
            HandleMovement();
            HandleBoosting();
        }
    }

    #region Movement
    private void HandleMovement()
    {
        if (isDead)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = input.Direction * moveSpeed;    //.linearVelocity là chuẩn hóa mới cơ bản ko khác gì .velocity
    }
    #endregion

    #region Boosting
    private void HandleBoosting()
    {
        if (isDead)
        {
            ForceStopBoost();
            return;
        }

        // Quyết định isPlayerBoosting dựa trên ý muốn (Nhấn Space) + đủ năng lượng
        bool wantsBoost = input.IsBoostBtnDown;
        bool hasEnergy = playerEnergy.CurrentEnergy >= minEnergyToBoost;

        // Nếu đang giữ Space mà hết năng lượng => chặn boost cho đến khi thả Space
        if (wantsBoost && !hasEnergy)
        {
            blockedBoostUntilRelease = true;
            ForceStopBoost();
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
        bool newBoostState = wantsBoost && hasEnergy && !blockedBoostUntilRelease;
        if (newBoostState != isPlayerBoosting)
        {
            isPlayerBoosting = newBoostState;
        }
    }

    public void ForceStopBoost()
    {
        if (isPlayerBoosting)
        {
            isPlayerBoosting = false;
        }

        blockedBoostUntilRelease = true;
    }
    #endregion

    public void OnDeath()
    {
        isDead = true;
        ForceStopBoost();
        rb.linearVelocity = Vector2.zero;
    }
}
