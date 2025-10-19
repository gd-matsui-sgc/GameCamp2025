using UnityEngine;

public class FriendCollision : MonoBehaviour
{
    [SerializeField] private string obstacleTag = "Obstacle";
    [SerializeField] private string enemyTag = "Enemy";
    [SerializeField] private string lightTruckTag = "LightTruck";
    [SerializeField] private GameObject deathEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(obstacleTag) || other.CompareTag(enemyTag) || other.CompareTag(lightTruckTag))
        {
            // HPを減らす（Playerに通知）
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                var health = player.GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.TakeSmallDamageFromFriend();
                }

                var manager = player.GetComponent<FollowerManager>();
                if (manager != null)
                {
                    manager.RemoveFollower(this.transform);
                }
            }

            if (deathEffect != null)
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            SoundManager.Instance.PlaySE(SoundDefine.SE.CHICKEN_CRY_3);

            Destroy(gameObject);
        }
    }
}
