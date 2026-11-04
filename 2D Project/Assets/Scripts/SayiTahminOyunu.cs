using UnityEngine;

public class SayiTahminOyunu : MonoBehaviour
{
    int gizliSayi;
    int tahminHakki = 5;

    private void Start()
    {
        gizliSayi = Random.Range(1, 101);
        Debug.Log("Gizli sayı 1 ile 100 arasında seçildi. Tahmin etmeye çalışın!");

        int[] yapilanTahminler = new int[] { 50, 75, 62, 58 };

        for (int i = 0; i < yapilanTahminler.Length; i++)
        {
            if (tahminHakki <= 0)
            {
                Debug.Log("Tahmin hakkınız kalmadı. Oyunu kaybettiniz!");
                break;
            }
            int tahmin = yapilanTahminler[i];
            if (tahmin == gizliSayi)
            {
                Debug.Log($"Tebrikler! {tahmin} sayısını doğru tahmin ettiniz!");
                break;
            }
            else if (tahmin < gizliSayi)
            {
                Debug.Log($"Tahmininiz {tahmin} sayısı gizli sayıdan küçük. Daha büyük bir sayı deneyin.");
            }
            else
            {
                Debug.Log($"Tahmininiz {tahmin} sayısı gizli sayıdan büyük. Daha küçük bir sayı deneyin.");
            }

        }
    }
}
