using UnityEngine;

public class Destroyaftertime : MonoBehaviour
{
    public float time;
    void Start()
    {
        Destroy(gameObject, time);
    }
}
