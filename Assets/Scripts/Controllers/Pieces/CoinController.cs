using UnityEngine;

public class CoinController : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(Constants.playerTag))
        {
            CoinsTextController.instance.UpdateCoins(1);
            Destroy(gameObject);
        }
    }
}