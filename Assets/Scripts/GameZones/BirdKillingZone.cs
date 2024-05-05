using UnityEngine;

public class BirdKillingZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Bird>() == false)
            return;

        if (other.TryGetComponent(out IKillable hearts) == false)
            return;

        hearts.Kill();
    }
}
