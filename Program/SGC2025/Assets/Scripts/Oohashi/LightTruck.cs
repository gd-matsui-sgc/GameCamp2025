using UnityEngine;

public class LightTruck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Egg"))
        {
            Destroy(other.gameObject);
        }
    }
}
