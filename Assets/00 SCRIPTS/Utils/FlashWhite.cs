using System.Collections;
using UnityEngine;

public class FlashWhite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Material defaulMaterial;
    [SerializeField] private Material hitMaterial;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaulMaterial = spriteRenderer.material;
    }

    public void Flash()
    {
        if (!gameObject.activeInHierarchy) return;

        spriteRenderer.material = hitMaterial;
        StartCoroutine(ResetMaterial());
    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defaulMaterial;
    }

    public void ResetMaterialAfterDisable()
    {
        this.spriteRenderer.material = defaulMaterial;
    }
}
