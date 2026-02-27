using UnityEngine;

public class MiniBossAvatar : MonoBehaviour
{
    public Material defaulMaterial;
    private FlashWhite flashWhite;

    void Start()
    {
        defaulMaterial = GetComponent<SpriteRenderer>().material;
        flashWhite = GetComponent<FlashWhite>();
    }
    public void ChangeMaterialWhenHit()
    {
        flashWhite.Flash();
    }
}
