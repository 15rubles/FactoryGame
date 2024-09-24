using UnityEngine;

public class CoinController : MonoBehaviour
{

    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] private AudioClip audioClip;

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(Constants.playerTag))
        {
            CoinsTextController.instance.UpdateCoins(1);
            SoundFXController.instance.PlaySound(audioClip, transform);
            Destroy(gameObject);
        }
    }


}