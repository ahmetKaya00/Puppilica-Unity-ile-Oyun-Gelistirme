using UnityEngine;

public class KararYapilariDersi : MonoBehaviour
{
    [SerializeField] private int can = 75;
    [SerializeField] private bool kalkanVarMi = true;
    [SerializeField] private int karakterSinifi = 2; // 1: Savaşçı, 2: Büyücü, 3: Okçu
    void Start()
    {
        if(can > 0 && kalkanVarMi)
        {
            Debug.Log("Karakter hayatta ve kalkanı var.");
        }
        else if(can > 0 && !kalkanVarMi)
        {
            Debug.Log("Karakter hayatta ama kalkanı yok.");
        }
        else
        {
            Debug.Log("Karakter ölü.");
        }

        switch(karakterSinifi)
        {
            case 1:
                Debug.Log("Karakter Savaşçı sınıfında.");
                break;
            case 2:
                Debug.Log("Karakter Büyücü sınıfında.");
                break;
            case 3:
                Debug.Log("Karakter Okçu sınıfında.");
                break;
            default:
                Debug.Log("Geçersiz karakter sınıfı.");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
