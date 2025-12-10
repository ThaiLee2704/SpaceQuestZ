using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
//[RequireComponent(typeof(PCInput))]

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private IPlayerInput input;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float boostSpeed = 1f;
    public float BoostSpeed => boostSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<IPlayerInput>();

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
        if (input.IsBoosting)
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
