using UnityEngine;

public class ScriptAnatomisi : MonoBehaviour
{
    [SerializeField] private int altinSayisi = 50;
    [SerializeField] private float yurumeHizi = 6.25f;
    [SerializeField] private string oyuncuAdi = "Astra";
    [SerializeField] private bool zehirlendiMi = false;


    void Start()
    {
        Debug.Log($"Oyuncu Adı: {oyuncuAdi}, Altın Sayısı: {altinSayisi}, Yürüme Hızı: {yurumeHizi}, Zehirlendi Mi: {zehirlendiMi}");
    }

    void Update()
    {
        
    }
}
