using UnityEngine;

public class Boom : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        //Destroy anim khi mà anim chiều hết
        Destroy(gameObject, animator.GetCurrentAnimatorClipInfo(0).Length);
    }
}
