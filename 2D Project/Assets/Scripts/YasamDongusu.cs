using UnityEngine;

public class YasamDongusu : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Awake: Bu metod, script etkinleştirildiğinde çağrılır ve genellikle değişkenlerin başlatılması için kullanılır.");
    }
    private void Start()
    {
        Debug.Log("Start: Bu metod, script etkinleştirildikten sonra bir kez çağrılır ve genellikle oyun başlatıldığında yapılacak işlemler için kullanılır.");
    }

    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate: Bu metod, fizik hesaplamaları için belirli bir zaman aralığında çağrılır ve genellikle fizik tabanlı hareketler için kullanılır.");
    }

    private void Update()
    {
        Debug.Log("Update: Bu metod, her karede bir kez çağrılır ve genellikle oyun mantığı ve kullanıcı girişleri için kullanılır.");
    }

    private void LateUpdate()
    {
        Debug.Log("Kamera Pozisyonlarında idealdir. LateUpdate: Bu metod, Update metodundan sonra çağrılır ve genellikle kamera hareketleri veya diğer nesnelerin konumlarını güncellemek için kullanılır.");
    }
}
