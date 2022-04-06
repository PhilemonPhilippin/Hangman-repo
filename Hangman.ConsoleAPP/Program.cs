
namespace Hangman.ConsoleAPP
{
    class Program
    {
        static int playerChance;
        static string playerYesOrNo;
        static void Main(string[] args)
        {
            do
            {
                playerChance = 11;
                string wordToGuess = RandomWord();
                Console.WriteLine("Le jeu (re)commence! Nouveau mot à trouver!");
                Console.WriteLine($"Longueur du mot : {wordToGuess.Length}");
                bool isWordFound = false;
                char[] riddle = CreateRiddle(wordToGuess.Length);
                do
                {
                    ShowRiddle(riddle);
                    char letter = AskLetter();
                    SearchForLetter(riddle, letter, wordToGuess);
                    if (!riddle.Contains('_'))
                    {
                        Console.WriteLine($"Bravo! Vous avez trouvé le mot: {wordToGuess}!");
                        isWordFound = true;
                    }
                    else if (playerChance == 0)
                        Console.WriteLine($"Raté! Vous n'avez plus d'essai. Le mot était: {wordToGuess}");
                } while (!isWordFound && playerChance > 0);

                AskToPlayAgain();
            } while (playerYesOrNo == "oui");
        }
        static char[] CreateRiddle(int wordLength)
        {
            char[] riddleArray = new char[wordLength];
            for (int i = 0; i < riddleArray.Length; i++)
            {
                riddleArray[i] = '_';
            }
            return riddleArray;
        }

        static void ShowRiddle(char[] riddleArray)
        {
            Console.WriteLine("=================================");
            Console.WriteLine("Voici la devinette: trouvez chaque lettre ou soyez pendu(e)!");
            for (int i = 0; i < riddleArray.Length; i++)
            {
                Console.Write(riddleArray[i] + " ");
            }
            Console.WriteLine();
        }

        static char AskLetter()
        {
            Console.WriteLine("Proposez une lettre pour deviner le mot:");
            char letter = Char.ToUpper(Console.ReadLine()[0]);
            return letter;
        }
        static void SearchForLetter(char[] riddleArray, char letter, string wordToGuess)
        {
            if (wordToGuess.Contains(letter))
            {
                Console.WriteLine($"Bien joué! Lettre {letter} trouvée!");
                UpdateRiddle(riddleArray, letter, wordToGuess);
            }
            else
            {
                Console.WriteLine($"Raté! Lettre {letter} non trouvée.");
                playerChance--;
                Console.WriteLine($"Le nombre d'essai qu'il vous reste: {playerChance}");
            }
        }

        static void UpdateRiddle(char[] riddleArray, char letter, string wordToGuess)
        {
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                if (wordToGuess[i] == letter)
                {
                    riddleArray[i] = letter;
                    Console.WriteLine($"La lettre {letter} était à la position: {i + 1}.");
                }
            }
        }

        static void AskToPlayAgain()
        {
            Console.WriteLine("Voulez-vous rejouer? Répondre par oui ou non");
            do
            {
                playerYesOrNo = Console.ReadLine().ToLower();
                if (playerYesOrNo == "oui")
                    Console.WriteLine("On continue !");
                else if (playerYesOrNo == "non")
                    Console.WriteLine("Vous décidez d'arrêter la partie.");
                else
                    Console.WriteLine("Veuillez répondre par oui ou non");
            } while (playerYesOrNo != "oui" && playerYesOrNo != "non");
        }

        static string RandomWord()
        {
            string word = "";
            using (StreamReader file = new StreamReader("mots.txt"))
            {
                int random = new Random().Next(0, 834);
                for (int i = 0; i < random; i++)
                {
                    word = file.ReadLine();
                }
                return word;
            }
        }
    }
}