using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour
{
    // alle variabelen
    private Sprite[] diceSides;  
    private SpriteRenderer rend;  
    private int whosTurn = 1;  
    private bool coroutineAllowed = true; 

    // Start wordt gebruikt voor initialisatie
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");  
        rend.sprite = diceSides[5];  
    }

    // Wordt uitgevoerd wanneer er op de dobbelsteen wordt geklikt
    private void OnMouseDown()
    {
        if (!GameControl.gameOver && coroutineAllowed)
            StartCoroutine("RollTheDice");  // Start het rollen van de dobbelsteen
    }

    // Coroutine voor het rollen van de dobbelsteen
    private IEnumerator RollTheDice()
    {
        coroutineAllowed = false;  // Zorgt ervoor dat de coroutine niet meerdere keren kan worden gestart
        int randomDiceSide = 0;  // Variabele voor de willekeurige dobbelsteen zijde
        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 6);  // Genereer een willekeurig getal tussen 0 en 5
            rend.sprite = diceSides[randomDiceSide];  // Verander de sprite van de dobbelsteen naar de willekeurige zijde
            yield return new WaitForSeconds(0.05f);  // Wacht een korte tijd voordat de volgende zijde wordt getoond
        }

        GameControl.diceSideThrown = randomDiceSide + 1;  // Sla het resultaat van de dobbelsteenworp op
        if (whosTurn == 1)
        {
            GameControl.MovePlayer(1);  // Beweeg speler 1 volgens het aantal ogen van de dobbelsteen
        }
        else if (whosTurn == -1)
        {
            GameControl.MovePlayer(2);  // Beweeg speler 2 volgens het aantal ogen van de dobbelsteen
        }
        whosTurn *= -1;  // Wissel van beurt
        coroutineAllowed = true;  // Sta toe dat de coroutine opnieuw kan worden gestart
    }
}
