using System.Collections;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private static readonly int Boosting = Animator.StringToHash(CONSTANT.ANIMATION_BOOSTING);
    private static readonly int MoveY = Animator.StringToHash(CONSTANT.ANIMATION_MOVE_Y);
    private static readonly int MoveX = Animator.StringToHash(CONSTANT.ANIMATION_MOVE_X);
    //Giải thích StringToHash: Dùng để chuyển dạng String về dạng int để truy cập nhanh hơn
    //Ta có CONSTANT.ANIMATION_MOVE_X là String "moveX" chuyển nó thành dạng int

    private IPlayerInput input;
    private Animator animator;
    private FlashWhite flashWhite;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        flashWhite = GetComponent<FlashWhite>();
    }

    private void Start()
    {
        input = GameManager.Instant.Player.Input;

        if (input == null)
            Debug.LogWarning($"{nameof(AnimationController)}: No PlayerInput found on {gameObject.name}.");
    }

    private void OnEnable()
    {
        Observer.AddListener(CONSTANT.OBSERVER_PLAYERHIT, OnChangeMaterialWhenHit);
    }

    private void OnDisable()
    {
        Observer.RemoveListener(CONSTANT.OBSERVER_PLAYERHIT, OnChangeMaterialWhenHit);
        flashWhite.ResetMaterialAfterDisable();
    }

    void Update()
    {
        if (input == null) return;

        if (Time.timeScale > 0)
        {
            animator.SetFloat(MoveX, input.DirectionX);
            animator.SetFloat(MoveY, input.DirectionY);
            animator.SetBool(Boosting, GameManager.Instant.Player.IsPlayerBoosting);
        }
    }

    private void OnChangeMaterialWhenHit(object[] datas)
    {
        flashWhite.Flash();
    }
}
