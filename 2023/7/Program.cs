class Program
{
    public static void Main(string[] args)
    {
        StreamReader sr = new(args[0]);
        string? line;

        var hands = new List<Hand>();
        while ((line = sr.ReadLine()) != null)
        {
            string[] parts = line.Split(' ');
            string hand = parts[0];
            long bid = long.Parse(parts[1]);
            hands.Add(new Hand(hand, bid));
        }
        hands.Sort();
        hands.Reverse();
        long res = 0;
        for (int i = 0; i < hands.Count; i++)
        {
            res += hands[i].Bid * (i + 1);
        }
        foreach (var c in hands)
        {
            Console.WriteLine($"{c}");
        }
        Console.WriteLine($"DONE: {res}");
    }
}

class Hand : IComparable<Hand>
{
    public string Cards { get; }
    public long Bid { get; }
    public Hand(string cards, long bid)
    {
        Cards = cards;
        Bid = bid;
    }

    public HandTypes getType()
    {
        Dictionary<char, int> counts = new();
        foreach (var c in Cards)
        {
            if (!counts.ContainsKey(c))
            {
                counts[c] = 0;
            }
            counts[c] += 1;
        }
        char? maxKey = null;
        foreach ((char key, int val) in counts)
        {
            if (maxKey == null && key != 'J')
            {
                maxKey = key;
            }
            else if (key != 'J' && maxKey != null && val > counts[(char)maxKey])
            {
                maxKey = key;
            }
        }
        if (maxKey != null && counts.ContainsKey('J'))
        {
            counts[(char)maxKey] += counts['J'];
            counts.Remove('J');
        }
        if (counts.Count == 1)
        {
            return HandTypes.FIVE_OF_KIND;
        }
        else if (counts.Count == 2 && counts.Values.Contains(4))
        {
            return HandTypes.FOUR_OF_KIND;
        }
        else if (counts.Count == 2)
        {
            return HandTypes.FULL_HOUSE;
        }
        else if (counts.Count == 3 && counts.Values.Contains(3))
        {
            return HandTypes.THREE_OF_KIND;
        }
        else if (counts.Count == 3)
        {
            return HandTypes.TWO_PAIR;
        }
        else if (counts.Count == 4)
        {
            return HandTypes.ONE_PAIR;
        }
        else
        {
            return HandTypes.HIGH_CARD;
        }
    }

    public int CompareTo(Hand? other)
    {
        if (other == null) return -1;

        if (getType() == other.getType())
        {
            for (int i = 0; i < Cards.Length; i++)
            {
                if (CardRank(Cards[i]) != other.CardRank(other.Cards[i]))
                {
                    return CardRank(Cards[i]) - other.CardRank(other.Cards[i]);
                }
            }
        }
        return getType() - other.getType();
    }

    public static Dictionary<char, int> CardRanks = new() {
        {'A', 0},
        {'K', 1},
        {'Q', 2},
        {'T', 3},
        {'9', 4},
        {'8', 5},
        {'7', 6},
        {'6', 7},
        {'5', 8},
        {'4', 9},
        {'3', 10},
        {'2', 11},
        {'J', 12},
    };
    private int CardRank(char v)
    {
        return CardRanks[v];
    }

    public override string ToString()
    {
        return $"({Cards}, {Bid}, {getType()})";
    }
}

/**
Five of a kind, where all five cards have the same label: AAAAA
Four of a kind, where four cards have the same label and one card has a different label: AA8AA
Full house, where three cards have the same label, and the remaining two cards share a different label: 23332
Three of a kind, where three cards have the same label, and the remaining two cards are each different from any other card in the hand: TTT98
Two pair, where two cards share one label, two other cards share a second label, and the remaining card has a third label: 23432
One pair, where two cards share one label, and the other three cards have a different label from the pair and each other: A23A4
High card, where all cards' labels are distinct: 23456
*/
enum HandTypes
{
    FIVE_OF_KIND,
    FOUR_OF_KIND,
    FULL_HOUSE,
    THREE_OF_KIND,
    TWO_PAIR,
    ONE_PAIR,
    HIGH_CARD
}