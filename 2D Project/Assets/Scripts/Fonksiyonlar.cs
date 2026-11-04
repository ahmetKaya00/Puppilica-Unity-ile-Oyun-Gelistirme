using UnityEngine;

public class Fonksiyonlar : MonoBehaviour
{
    [SerializeField] private int oyuncuCani = 100;
    void Start()
    {
        HasarVer(25);
    }

    private void HasarVer(int hasarMiktari)
    {
        //100           50          = 50
        oyuncuCani -= hasarMiktari;
        Debug.Log($"Oyuncuya {hasarMiktari} hasar verildi. Kalan can: {oyuncuCani}");
    }
    private int KritikHasarHesapla(int tabanHasar, int carpan)
    {
        int sonuc = tabanHasar * carpan;

        return sonuc;
    }
}