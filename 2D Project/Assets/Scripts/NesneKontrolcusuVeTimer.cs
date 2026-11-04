using UnityEngine;

public class NesneKontrolcusuVeTimer : MonoBehaviour
{
    [Header("Hareket Parametreleri")]
    [Tooltip("Objenin saniyedeki hareket hızı")]
    [SerializeField] private float hareketHizi = 4f;

    [Tooltip("Devriye atılacak maksimum mesafe")]
    [SerializeField] private float devriyeMesafesi = 3f;

    [Header("Zamanlayıcı Parametreleri")]
    [Tooltip("Renk değişimi aralığı (saniye)")]
    [SerializeField] private float renkDegisimAraligi = 2f;


    private Renderer nesneRenderer;
    private Vector3 baslangicPozisyonu;

    private float gecenZaman = 0f;
    private int hareketYonu = 1; // 1: ileri, -1: geri

    private void Awake()
    {
        nesneRenderer = GetComponent<Renderer>();

        baslangicPozisyonu = transform.position;
    }

    private void Update()
    {
        HareketEt();
        ZamanlayiciyiGuncelle();
    }

    void HareketEt()
    {
        Vector3 hareketMiktari = Vector3.right * (hareketHizi * hareketYonu * Time.deltaTime);

        transform.Translate(hareketMiktari, Space.World);

        float gidilenMesafe = transform.position.x - baslangicPozisyonu.x;
        if (gidilenMesafe >= devriyeMesafesi)
        {
            hareketYonu = -1; // Geri dön
        }
        else if (gidilenMesafe <= -devriyeMesafesi)
        {
            hareketYonu = 1; // İleri dön
        }

    }
    private void ZamanlayiciyiGuncelle()
    {
        gecenZaman += Time.deltaTime;

        if(gecenZaman >= renkDegisimAraligi)
        {

            RastgeleRenkAta();
            gecenZaman = 0f;
        }
    }

    void RastgeleRenkAta()
    {
        Color yeniRenk = new Color(Random.value, Random.value, Random.value);

        nesneRenderer.material.color = yeniRenk;
    }
}
