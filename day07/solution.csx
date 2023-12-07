#load "../utils/utils.csx"

public class Day7
{
    bool logToConsole = true;
    bool logToFile = true;

    public void Part1(string[] lines)
    {
        var players = new List<Details>();

        foreach(var line in lines)
        {
            var details = ParseLine(line);
            players.Add(details);
        }

        var orderedPlayers = players.OrderByDescending(p => p.InitialRanking).ToList();
        players = RecalculateHands(orderedPlayers);
        players.ForEach(Console.WriteLine);  
        
        Utils.Log($"", logToConsole, logToFile);

        orderedPlayers  = players.OrderByDescending(p => p.Rank).ToList();
        orderedPlayers.ForEach(p => Utils.Log($"{p}", logToConsole, logToFile));

        var distinctRanks = players.Distinct().Count() == players.Count();
        Utils.Info($"Distinct Ranks: {distinctRanks}", logToConsole, logToFile);

        var total = CalculateTotal(orderedPlayers);
        Utils.Answer($"{total}", true, logToFile);
    }

    public long CalculateTotal(List<Details> players)
    {
        var total = 0L;
        
        foreach (var player in players)
        {
            var playerTotal = player.Rank * player.Bid;
            Utils.Log($"Player Total: {playerTotal}", logToConsole, logToFile);
            total += playerTotal;
        }

        return total;
    }

    public List<Details> RecalculateHands(List<Details> players)
    {
        Utils.Log($"--- --- --- ---", logToConsole, logToFile);
        var count = players.Count();
        Utils.Log($"# {count}", logToConsole, logToFile);

        players.ForEach(Console.WriteLine);

        var rank = count;

        for (var i = 0; i < count; i++)
        {
            Utils.Log($"i {i}", logToConsole, logToFile);
            
            var p1 = players[i];
            Utils.Log($"P1 IR: {p1.InitialRanking} (Hand: {string.Join(",", p1.Hand)})", logToConsole, logToFile);

            if (i == count - 1)
            {
                Utils.Log($"Last one", logToConsole, logToFile);
                p1.Rank = rank;
                break;
            }

            var p2 = players[i+1];
            Utils.Log($"P2 IR: {p2.InitialRanking} (Hand: {string.Join(",", p2.Hand)})", logToConsole, logToFile);

            // // Set first player to count;
            // p1.Rank = count;
            // p2.Rank = count--;

            // if InitialRanking match - Compare current player with next.
            if (p1.InitialRanking == p2.InitialRanking)
            {
                // Check which is better.
                var winner = CalculateWinner(p1.Hand, p2.Hand);
                Utils.Log($"Winner {winner}", logToConsole, logToFile);

                // if (winner == 0) // Draw?
                if (winner == 1)
                {
                    Utils.Log($"P1 Greater", logToConsole, logToFile);
                    p1.Rank = rank;
                    p2.Rank = rank - 1;
                }
                else
                {
                    Utils.Log($"P2 Greater", logToConsole, logToFile);
                    p1.Rank = rank - 1;
                    p2.Rank = rank;
                }
                // Jump a player
                i++;
                rank = rank - 2;
            }
            else
            {
                Utils.Log($"else", logToConsole, logToFile);

                if (p1.InitialRanking > p2.InitialRanking)
                {
                    p1.Rank = rank;
                    p2.Rank = rank - 1;
                }
                else
                {
                    p1.Rank = rank - 1;
                    p2.Rank = rank;
                }
                i++;
                rank = rank - 2;
            }

            Utils.Log($"P1 {p1}", logToConsole, logToFile);
            Utils.Log($"P2 {p2}", logToConsole, logToFile);
            Utils.Log($"i {i} | Rank {rank} | Count {count} ", logToConsole, logToFile);

            // rank = rank - 2;
        }

        return players;
    }

    public long CalculateWinner(List<string> hand1, List<string> hand2)
    {
        var winner = 0;
        var count = hand1.Count();

        for (var i = 0; i < count; i++)
        {
            var cardH1 = Strengths[hand1.ElementAt(i)];
            var cardH2 = Strengths[hand2.ElementAt(i)];
            if (cardH1 > cardH2) return 1;
            if (cardH1 < cardH2) return 2;
        }

        return 0;
    }

    public Details ParseLine(string line)
    {
        Utils.Log($"{line}", logToConsole, logToFile);
        (List<string> hand, long bid) camelCards = line.Split(' ') switch { var n => ( (n[0].ToCharArray().ToList()?.Select(c => c.ToString())?.ToList()), long.Parse(n[1]) ) };
        Utils.Log($"{camelCards.bid} {string.Join(",", camelCards.hand)}", logToConsole, logToFile);
        
        var details = new Details();
        details.Bid = camelCards.bid;
        details.Hand = camelCards.hand;

        details = CalculateInitialRanking(details);
        Utils.Log($"{details}", logToConsole, logToFile);

        return details;
    }

    public Details CalculateInitialRanking(Details details)
    {
        var hand = details.Hand;

        // Texas holdem Rank Card Evaluator
        // https://github.com/danielpaz6/Poker-Hand-Evaluator
        var initialRanking = 0;

        var sortedList = hand.GroupBy(c => c)
                            .OrderByDescending(g => g.Count())
                            // .SelectMany(x => x).ToList();
                            .Select(group => Tuple.Create(group.Key, group.Count()));
        
        foreach (var item in sortedList)
        {
            Console.WriteLine($"{item.Item1} {item.Item2}");
        }

        // var isFiveOfAKind = hand.All(c => c);
        var isFiveOfAKind = hand.Distinct().Count() == 1;
        Utils.Log($"Five Of A Kind: {isFiveOfAKind}", logToConsole, logToFile);
        if (isFiveOfAKind) initialRanking = 7;

        var isFourOfAKind = (sortedList.Where(g => g.Item2 == 4).Count() == 1);
        Utils.Log($"Four Of A Kind: {isFourOfAKind}", logToConsole, logToFile);
        if (isFourOfAKind) initialRanking = 6;

        var isFullHouse = (sortedList.Where(g => g.Item2 == 3).Count() == 1 && sortedList.Where(g => g.Item2 == 2).Count() == 1);
        Utils.Log($"Full House: {isFullHouse}", logToConsole, logToFile);
        if (isFullHouse) initialRanking = 5;

        var isThreeOfAKind = (sortedList.Where(g => g.Item2 == 3).Count() == 1 && sortedList.Where(g => g.Item2 == 1).Count() == 2);
        Utils.Log($"Three Of A Kind: {isThreeOfAKind}", logToConsole, logToFile);
        if (isThreeOfAKind) initialRanking = 4;

        var isTwoPair = (sortedList.Where(g => g.Item2 == 2).Count() == 2);
        Utils.Log($"Two Pair: {isTwoPair}", logToConsole, logToFile);
        if (isTwoPair) initialRanking = 3;
        
        var isOnePair = (sortedList.Where(g => g.Item2 == 2).Count() == 1 && sortedList.Where(g => g.Item2 == 1).Count() == 3);
        Utils.Log($"One Pair: {isOnePair}", logToConsole, logToFile);
        if (isOnePair) initialRanking = 2;

        var isHighCard = (sortedList.Where(g => g.Item2 == 1).Count() == 5);
        Utils.Log($"High Card: {isHighCard}", logToConsole, logToFile);
        if (isOnePair) initialRanking = 1;

        // Utils.Info($"Initial Ranking: {initialRanking}");
        Utils.Log($"Initial Ranking: {initialRanking}", logToConsole, logToFile);

        details.InitialRanking = initialRanking;

        return details;
    }

    // public void Part2(string[] lines)
    // {
    // }

    // --- --- --- ---

    public string[] Rankings = new string[] { "Five of a kind", "Four of a kind", "Full house", "Three of a kind", "Two pair", "One pair", "High card" };
    public Dictionary<string, long> Strengths = new Dictionary<string, long>() {
        { "A", 14 },
        { "K", 13 },
        { "Q", 12 },
        { "J", 11 },
        { "T", 10 },
        { "9", 9 },
        { "8", 8 },
        { "7", 7 },
        { "6", 6 },
        { "5", 5 },
        { "4", 4 },
        { "3", 3 },
        { "2", 2 },
    };
}

public class Details
{
    public List<string> Hand = new List<string>();
    public long Bid = 0;
    public long InitialRanking = 0;
    public long Rank = 0;

    public override string ToString()
    {
        return $"Hand: {string.Join(",", Hand)} |  Bid: {Bid} | Rank: {Rank} ({InitialRanking})";
    }
}

Utils.Log("-- Day 7 --", true, true);
Utils.Log("-----------", true, true);

var day7 = new Day7();

// string fileName = @"input-sample.txt";
string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Utils.Log("Part 1", true, true);
day7.Part1(lines);

// Part 2
// Utils.Log("Part 2", true, true);
// day7.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();