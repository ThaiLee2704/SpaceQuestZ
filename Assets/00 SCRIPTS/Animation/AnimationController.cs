using System.Collections;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private IPlayerInput input;

    private Material defaulMaterial;
    [SerializeField] Material hitMaterial;

    private Animator animator;

    private static readonly int MoveX = Animator.StringToHash(CONSTANT.ANIMATION_MOVE_X);
    //Giải thích StringToHash: Dùng để chuyển dạng String về dạng int để truy cập nhanh hơn
    //Ta có CONSTANT.ANIMATION_MOVE_X là String "moveX" chuyển nó thành dạng int
    private static readonly int MoveY = Animator.StringToHash(CONSTANT.ANIMATION_MOVE_Y);
    private static readonly int Boosting = Animator.StringToHash(CONSTANT.ANIMATION_BOOSTING);

    private void Awake()
    {
        animator = GetComponent<Animator>();
        defaulMaterial = GetComponent<SpriteRenderer>().material;
    }

    private void Start()
    {
        input = GameManager.Instant.Player.Input;

        if (input == null)
            Debug.LogWarning($"{nameof(AnimationController)}: No PlayerInput found on {gameObject.name}.");

        Observer.AddListener(CONSTANT.OBSERVER_PLAYERHIT, OnChangeMaterialWhenHit);
    }

    private void OnDestroy()
    {
        Observer.RemoveListener(CONSTANT.OBSERVER_PLAYERHIT, OnChangeMaterialWhenHit);
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
        this.GetComponent<SpriteRenderer>().material = hitMaterial;
        StartCoroutine(ResetMaterial());
    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.2f);
        this.GetComponent <SpriteRenderer>().material = defaulMaterial;
    }
}
