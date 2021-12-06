using Microsoft.AspNetCore.Components;

namespace AoCBlazor.Pages._2021;

[Route("/2021/Day4/Part2")]
public partial class Day4Part2Of2021 : ConsoleComponentBase
{
    protected override async Task Main()
    {
        Console.WriteLine("2021 - Day 4 - Part 2", ConsoleColor.DarkMagenta);
        Console.WriteLine("Paste or drag input data");

        string[] lines = null;

        while (lines == null)
        {
            var data = await Console.ReadLine();
            lines = data.Replace("\r", "").Split('\n');
        }

        Console.WriteLine("Running...");
        StateHasChanged();

        var drawnNumbers = lines[0].Split(',').ToList();

        var boards = new List<Board>();
        int lineIndex = 2;
        while (lineIndex + 5 <= lines.Length)
        {
            var board = ReadBoard(lines, lineIndex);
            //PrintBoard(board);
            //Console.WriteLine();
            lineIndex += 6;
            boards.Add(board);
        }


        while (true)
        {
            var number = DrawNumber(drawnNumbers);
            ApplyNumber(number, boards);
            var winningBoards = GetWinners(boards).ToList();

            foreach (var win in winningBoards)
            {
                boards.Remove(win);

                if (boards.Count == 0)
                {
                    var score = GetBoardScore(win, number);

                    PrintBoard(win);

                    Console.WriteLine($"Winning board score: {score}");
                    return;
                }
            }
        }
    }

    static int GetBoardScore(Board board, int lastNumber)
    {
        var sum = 0;
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                if (board.Squares[x, y].Drawn == false)
                {
                    sum += board.Squares[x, y].Number;
                }
            }
        }

        return sum * lastNumber;
    }

    static IEnumerable<Board> GetWinners(List<Board> boards)
    {

        foreach (var board in boards)
        {
            var rowWin = new bool[5] { true, true, true, true, true };
            var colWin = new bool[5] { true, true, true, true, true };

            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (board.Squares[x, y].Drawn == false)
                    {
                        rowWin[y] = false;
                        colWin[x] = false;
                    }

                }
            }

            if (rowWin.Any(n => n) || colWin.Any(n => n))
                yield return board;
        }
    }

    static void ApplyNumber(int number, List<Board> boards)
    {
        foreach (var board in boards)
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (board.Squares[x, y].Number == number)
                        board.Squares[x, y].Drawn = true;
                }
            }
        }
    }

    static int DrawNumber(List<string> drawnNumbers)
    {
        var entry = drawnNumbers[0];
        drawnNumbers.RemoveAt(0);
        return int.Parse(entry);
    }

    static Board ReadBoard(string[] lines, int startIndex)
    {
        var board = new Board
        {
            Squares = new BoardSquare[5, 5]
        };

        for (int y = 0; y < 5; y++)
        {
            var line = lines[startIndex + y];
            var nums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            for (int x = 0; x < 5; x++)
            {
                board.Squares[x, y] = new BoardSquare
                {
                    Number = int.Parse(nums[x]),
                    Drawn = false
                };
            }
        }

        return board;
    }

    void PrintBoard(Board board)
    {
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                var color = Console.ForegroundColor;

                if (board.Squares[x, y].Drawn)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.Write($"{board.Squares[x, y].Number,3}");

                Console.ForegroundColor = color;
            }

            Console.WriteLine("");
        }
    }

    class Board
    {
        public BoardSquare[,] Squares { get; set; }
    }

    class BoardSquare
    {
        public int Number { get; set; }

        public bool Drawn { get; set; }
    }
}
