using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private IPlayerInput input;
    private Animator animator;

    private static readonly int MoveX = Animator.StringToHash(CONSTANT.ANIMATION_MOVE_X);
    //Giải thích StringToHash: Dùng để chuyển dạng String về dạng int để truy cập nhanh hơn
    //Ta có CONSTANT.ANIMATION_MOVE_X là String "moveX" chuyển nó thành dạng int
    private static readonly int MoveY = Animator.StringToHash(CONSTANT.ANIMATION_MOVE_Y);
    private static readonly int Boosting = Animator.StringToHash(CONSTANT.ANIMATION_BOOSTING);

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        input = GameManager.Instant.Player.Input;

        if (input == null)
            Debug.LogWarning($"{nameof(AnimationController)}: No PlayerInput found on {gameObject.name}.");
    }

    void Update()
    {
        if (input == null) return;
        animator.SetFloat(MoveX, input.DirectionX);
        animator.SetFloat(MoveY, input.DirectionY);
        animator.SetBool(Boosting, GameManager.Instant.Player.IsPlayerBoosting);
    }
}
