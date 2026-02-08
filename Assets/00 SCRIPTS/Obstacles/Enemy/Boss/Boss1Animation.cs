using System.Collections;
using UnityEngine;

public class Boss1Animation : MonoBehaviour
{
    public Material defaulMaterial;
    public Material hitMaterial;

    private Boss1 boss1;
    private Animator animator;

    private static readonly int Charging = Animator.StringToHash(CONSTANT.ANIMATION_BOSS_CHARGING);

    private void Start()
    {
        boss1 = GetComponentInParent<Boss1>();
        animator = GetComponent<Animator>();
        defaulMaterial = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        animator.SetBool(Charging, boss1.Charging);
    }

    public void OnChangeMaterialWhenHit()
    {
        this.GetComponent<SpriteRenderer>().material = hitMaterial;
        StartCoroutine(ResetMaterial());
    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<SpriteRenderer>().material = defaulMaterial;
    }
}
