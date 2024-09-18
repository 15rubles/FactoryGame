using UnityEngine;

public class CoinController : MonoBehaviour
{

    [SerializeField] float rotationSpeed = 100f;

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(Constants.playerTag))
        {
            CoinsTextController.instance.UpdateCoins(1);
            Destroy(gameObject);
        }
    }


}