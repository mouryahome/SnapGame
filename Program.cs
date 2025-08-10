// See https://aka.ms/new-console-template for more information

Random rng = new Random();
string[] FacesOfCards = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
string[] TypeOfCards = { "Heart", "Diamond", "Club", "Spade" };
//bool isGameOver = true;

while (true)
{
    Console.WriteLine("\nWelcome to the Card Game!");
    Console.WriteLine("1 - Start Game");
    Console.WriteLine("2 - Exit");
    string? choice = Console.ReadLine();
    if (choice == "1")
    {
        //isGameOver = false;
        StartCardGame();
        
    }
    else if (choice == "2")
    {
        Console.WriteLine("Exiting the game.");
        return;
    }
    else
    {
        Console.WriteLine("Invalid option, please try again.");
        continue;
    }
}


void StartCardGame()
{
    Console.Write("Enter number of packs (N): ");
    int numPacks;
    while (!int.TryParse(Console.ReadLine(), out numPacks) || numPacks < 1)
    {
        Console.Write("Invalid number, try again: ");
    }

    Console.WriteLine("Select matching condition:");
    Console.WriteLine("1 - Match by Face Value");
    Console.WriteLine("2 - Match by Suit");
    Console.WriteLine("3 - Match by Both Face & Suit");
    MatchingOptions matchType;
    while (!Enum.TryParse(Console.ReadLine(), out matchType) ||
           !Enum.IsDefined(typeof(MatchingOptions), matchType))
    {
        Console.Write("Invalid choice, try again: ");
    }

    List<PlayCard> deck = CreateDeck(numPacks);
    Shuffle(deck);

    List<PlayCard> pile = new List<PlayCard>();
    int player1Score = 0, player2Score = 0;

    for (int i = 0; i < deck.Count; i++)
    {
        PlayCard currentCard = deck[i];
        pile.Add(currentCard);
        Console.WriteLine($"Played: {currentCard}");

        if (pile.Count >= 2)
        {
            PlayCard prevCard = pile[pile.Count - 2];
            if (IsMatch(prevCard, currentCard, matchType))
            {
                // Randomly choose player who wins this Snap
                int winner = rng.Next(1, 3);
                Console.WriteLine($"SNAP! Player {winner} wins {pile.Count} cards!");
                if (winner == 1)
                    player1Score += pile.Count;
                else
                    player2Score += pile.Count;

                pile.Clear();
            }
        }
    }

    // Remaining pile is ignored
    Console.WriteLine("\nGame Over!");
    Console.WriteLine($"Player 1 total cards: {player1Score}");
    Console.WriteLine($"Player 2 total cards: {player2Score}");

    if (player1Score > player2Score)
        Console.WriteLine("Player 1 Wins!");
    else if (player2Score > player1Score)
        Console.WriteLine("Player 2 Wins!");
    else
        Console.WriteLine("It's a Draw!");

}

List<PlayCard> CreateDeck(int numPacks)
{    
    List<PlayCard> deck = new List<PlayCard>();

    for (int n = 0; n < numPacks; n++)
    {
        foreach (string face in FacesOfCards)
        {
            foreach (string type in TypeOfCards)
            {
                deck.Add(new PlayCard { CardFaceValue = face, CardType = type });
            }
        }
    }
    return deck;
}

void Shuffle(List<PlayCard> deck)
{
    for (int i = deck.Count - 1; i > 0; i--)
    {
        int j = rng.Next(i + 1);
        PlayCard temp = deck[i];
        deck[i] = deck[j];
        deck[j] = temp;
    }
}

bool IsMatch(PlayCard c1, PlayCard c2, MatchingOptions option)
{
    switch (option)
    {
        case MatchingOptions.CardType:
            return c1.CardType == c2.CardType;
        case MatchingOptions.FaceValue:
            return c1.CardFaceValue == c2.CardFaceValue;
        case MatchingOptions.Both:
            return c1.CardType == c2.CardType && c1.CardFaceValue == c2.CardFaceValue;
        default:
            return false;
    }
}