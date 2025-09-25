string[][] words =
[
    ["spoon", "ocean", "cloud", "mouse", "table"],
    ["cucumber", "teacher", "blizzard", "pumpkin"],
    ["pineapple", "dictionary", "strawberry", "cheesecake"]
];

bool isDifficultyChoosing = true;

do
{
    Console.WriteLine("\nChoose the difficulty level: easy (0), medium (1) or hard (2)");

    if (int.TryParse(Console.ReadLine(), out int parsedDifficulty) &&
        Enum.IsDefined(typeof(DifficultyLevel), parsedDifficulty))
    {
        var difficultyLevel = (DifficultyLevel)parsedDifficulty;
        Console.WriteLine($"\nYou have chosen {difficultyLevel} difficulty level.");

        Console.WriteLine("\nTry to guess which word is encrypted!");

        Random rnd = new Random();
        int randomIndex = rnd.Next(words[(int)difficultyLevel].Length);
        string randomWord = words[(int)difficultyLevel][randomIndex];

        int wordLength = randomWord.Length;
        Console.WriteLine($"\nNumber of letters in the word is: {wordLength}");

        int attempts = difficultyLevel switch
        {
            DifficultyLevel.Easy => 10,
            DifficultyLevel.Medium => 8,
            DifficultyLevel.Hard => 6
        };
        Console.WriteLine($"\nNumber of failed attempts is: {attempts}");
        Console.WriteLine();

        char[] letters = randomWord.ToCharArray();

        char[] hiddenLetters = new char[wordLength];
        for (int i = 0; i < wordLength; i++)
        {
            hiddenLetters[i] = '_';
            Console.Write($"{hiddenLetters[i]} ");
        }
        Console.WriteLine();

        bool isEnteringLetter = true;

        int leftAttempts = attempts;

        do
        {
            Console.Write("\nEnter your letter: ");
            char letter;

            if (char.TryParse(Console.ReadLine(), out letter) &&
                Char.IsLetter(letter))
            {
                bool isLetterPresent = false;
                
                for (int i = 0; i < wordLength; i++)
                {
                    if (letters[i] == letter)
                    {
                        hiddenLetters[i] = letter;
                        isLetterPresent = true;
                    }
                }
                if (isLetterPresent)
                {
                    if (Array.Exists(hiddenLetters, c => c == '_'))
                    {
                        Console.WriteLine("\nThis word has your letter.");
                        Console.WriteLine();
                        for (int i = 0; i < wordLength; i++)
                        {
                            Console.Write($"{hiddenLetters[i]} ");
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine($"\nCongratulations! You guessed it! The encrypted word was \"{randomWord}\"");
                        isEnteringLetter = false;
                    }
                }
                else
                {
                    leftAttempts--;

                    if (leftAttempts == 0)
                    {
                        Console.WriteLine($"\nYou lost! The encrypted word was \"{randomWord}\"");
                        isEnteringLetter = false;
                    }
                    else
                        Console.WriteLine($"\nThis word doesn't have your letter. Left attempts: {leftAttempts}");
                }
            }
            else
                Console.WriteLine("\nIncorrect input");
        }
        while (isEnteringLetter);

        isDifficultyChoosing = false;
    }
    else
        Console.WriteLine("\nIncorrect input");
}
while (isDifficultyChoosing);




