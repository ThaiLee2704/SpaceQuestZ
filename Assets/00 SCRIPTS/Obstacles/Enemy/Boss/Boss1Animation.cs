using System.Collections;
using UnityEngine;

public class Boss1Animation : MonoBehaviour
{
    public Material defaulMaterial;
    private Boss1 boss1;
    private Animator animator;
    private FlashWhite flashWhite;

    private static readonly int Charging = Animator.StringToHash(CONSTANT.ANIMATION_BOSS_CHARGING);


    private void Start()
    {
        boss1 = GetComponentInParent<Boss1>();
        animator = GetComponent<Animator>();
        defaulMaterial = GetComponent<SpriteRenderer>().material;
        flashWhite = GetComponent<FlashWhite>();
    }

    private void Update()
    {
        animator.SetBool(Charging, boss1.IsCharging);
    }

    public void ChangeMaterialWhenHit()
    {
        flashWhite.Flash();
    }
}
