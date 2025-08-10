/// <summary>
/// This class represents a card that can be played in a game.
/// </summary>
public class PlayCard
{
    public string CardFaceValue { get; set; }
    public string CardType { get; set; }

    public override string ToString() => $"{CardFaceValue} of {CardType}";
}

enum MatchingOptions
{
    FaceValue = 1,
    CardType = 2,
    Both = 3
}