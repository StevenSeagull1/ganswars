using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour
{

    private Sprite[] diceSides;  // Array of dice sides (sprites)
    private SpriteRenderer rend;  // Reference to the sprite renderer component
    private int whosTurn = 1;  // Indicates whose turn it is (player 1 or player 2)
    private bool coroutineAllowed = true;  // Indicates whether the coroutine is allowed to execute

    // Start wordt gebruikt voor initialisatie
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");  // Laden van alle sprites van de dobbelstenen
        rend.sprite = diceSides[5];  // Standaard sprite voor de dobbelsteen
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
