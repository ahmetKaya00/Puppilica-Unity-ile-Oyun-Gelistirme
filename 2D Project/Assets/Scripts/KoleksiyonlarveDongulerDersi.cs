using System.Collections.Generic;
using UnityEngine;

public class KoleksiyonlarveDongulerDersi : MonoBehaviour
{
    private string[] silahEnvanteri = new string[3] { "Kılıç", "Yay", "Hançer" };

    private List<string> iksirCantasi = new List<string>();

    private Dictionary<string, int> marketFiyatlari = new Dictionary<string, int>();
    void Start()
    {
        for(int i = 0; i < silahEnvanteri.Length; i++)
        {
            Debug.Log($"Silah Envanteri {i + 1}: {silahEnvanteri[i]}");
        }

        iksirCantasi.Add("Can İksiri");
        iksirCantasi.Add("Mana İksiri");
        iksirCantasi.Add("Hız İksiri");

        foreach (string iksir in iksirCantasi)
        {
            Debug.Log($"İksir Çantası: {iksir}");
        }

        marketFiyatlari.Add("Kılıç", 100);
        marketFiyatlari.Add("Yay", 150);
        marketFiyatlari.Add("Hançer", 75);

        if(marketFiyatlari.ContainsKey("Kılıç"))
        {
            int fiyat = marketFiyatlari["Kılıç"];
            Debug.Log($"Kılıç Fiyatı: {fiyat} Altın");
        }

        int kalanMermi = 3;
        while(kalanMermi > 0)
        {
            Debug.Log($"Ateş Edildi! Kalan Mermi: {kalanMermi}");
            kalanMermi--;
        }
    }

    void Update()
    {
        
    }
}
