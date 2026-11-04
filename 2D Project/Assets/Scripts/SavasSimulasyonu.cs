using UnityEngine;

public class SavasSimulasyonu : MonoBehaviour
{
    [SerializeField] private int kahramanCan = 100;
    [SerializeField] private int canavarCan = 80;

    private void Start()
    {
        Debug.Log("Zindan Savaşı Başladı");
        int turSayaci = 1;

        while(kahramanCan > 0 && canavarCan > 0)
        {
            Debug.Log($"--- Tur {turSayaci} ---");
            int oyuncuHasari = Random.Range(10, 25);
            canavarCan -= oyuncuHasari;
            Debug.Log($"Kahraman {oyuncuHasari} hasar verdi. Canavarın kalan canı: {canavarCan}");
            
            if(canavarCan <= 0)
            {
                Debug.Log("Kahraman kazandı!");
                break;
            }

            int canavarHasari = Random.Range(5, 20);
            kahramanCan -= canavarHasari;
            Debug.Log($"Canavar {canavarHasari} hasar verdi. Kahramanın kalan canı: {kahramanCan}");

            if(kahramanCan <= 0)
            {
                Debug.Log("Canavar kazandı!");
                break;
            }
            turSayaci++;
        }

    }
}
