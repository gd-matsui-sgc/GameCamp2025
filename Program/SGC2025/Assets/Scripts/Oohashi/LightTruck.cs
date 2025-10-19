using UnityEngine;

public class LightTruck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Egg"))
        {
            SoundManager.Instance.PlaySE(SoundDefine.SE.JAIL_DOOR_CLOSE);
            Destroy(other.gameObject);
        }
    }
}
