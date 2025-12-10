using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instant = null;
    public static T Instant => instant;

    private void Awake()
    {
        if (instant == null)
        {
            instant = GetComponent<T>();
            return;
        }

        if (instant.gameObject.GetInstanceID() != this.gameObject.GetInstanceID()) 
            Destroy(this.gameObject);
    }
}
