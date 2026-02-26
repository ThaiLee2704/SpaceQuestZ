using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(IPlayerInput))]
[RequireComponent(typeof(PlayerEnergy))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerVFX))]

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private IPlayerInput input;
    private PlayerEnergy playerEnergy;

    // Cờ chặn boost cho đến khi người chơi thả phím Space
    private bool blockedBoostUntilRelease;

    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float boostMultiplier = 5f;
    [SerializeField] private bool isPlayerBoosting;
    [SerializeField] private bool isDead;

    public IPlayerInput Input => input;
    public bool IsPlayerBoosting => isPlayerBoosting;
    public float BoostSpeed => isDead ? 0f : (isPlayerBoosting ? boostMultiplier : 1f);

    //Bỏ trong OnEnable() để sau này xử lý coi quảng cáo hồi sinh là ngon luôn
    private void OnEnable()
    {
        //Mỗi khi clone level thì đăng ký PlayerController trong level đó cho GameManager
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
            HandleFireBullet();
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

        bool wantsBoost = input.IsBoostBtnDown;
        bool hasEnergy = playerEnergy.HasEnergy();

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

        //isPlayerBoosting là biến tạm để lưu trạng thái boost ở frame trước, khi trạng thái thay đổi
        //tức là newBoostState thay đổi so với frame trước (isPlayerBoosting) thì chạy âm thanh boost
        //sau đó lại gán trạng thái frame vừa rồi cho isPlayerBoosting rồi lại nhảy qua frame mới
        bool newBoostState = wantsBoost && hasEnergy && !blockedBoostUntilRelease;
        if (newBoostState != isPlayerBoosting)
        {
            if (newBoostState)
            {
                Observer.Notify("playerBoost", null);
            }

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

    #region Fire Bullet
    private void HandleFireBullet()
    {
        bool wantsFire = input.IsFirePressed;
        if (wantsFire)
        {
            if (currentWeapon != null)
                currentWeapon.FireBullet();
        }
    }

    #endregion

    public void OnDeath()
    {
        isDead = true;
        ForceStopBoost();
        rb.linearVelocity = Vector2.zero;
    }
}
