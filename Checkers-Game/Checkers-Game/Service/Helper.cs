using Checkers_Game.Model;
using Checkers_Game.Properties;
using Checkers_Game.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Checkers_Game.Service
{
    public class Helper
    {
        Helper() { }

        static Helper()
        {
            moveIndicesForWhite = new List<Tuple<int, int>>
            {
                Tuple.Create(1, 1),
                Tuple.Create(1, -1)
            };

            moveIndicesForBlack = new List<Tuple<int, int>>
            {
                Tuple.Create(-1, 1),
                Tuple.Create(-1, -1)
            };

            moveIndicesForKings = new List<Tuple<int, int>>();
            foreach (var item in moveIndicesForWhite)
                moveIndicesForKings.Add(item);
            foreach (var item in moveIndicesForBlack)
                moveIndicesForKings.Add(item);
        }

        public static int numberOfRows = 8;
        public static int numberOfColumns = 8;

        public static string WhitePawnPath = "/Checkers-Game;component/Resource/whitepiece.png";
        public static string BlackPawnPath = "/Checkers-Game;component/Resource/blackpiece.png";
        public static string WhiteKingPath = "/Checkers-Game;component/Resource/whiteking.png";
        public static string BlackKingPath = "/Checkers-Game;component/Resource/blackking.png";
        public static string TransparentPath = "/Checkers-Game;component/Resource/transparent.png";

        public static string BlackColor = "#d18b47";
        public static string WhiteColor = "#ffce9e";
        public static string BlueColor  = "#0000ff";
        public static string GreenColor = "#33cc33";

        public static List<Tuple<int, int>> moveIndicesForWhite;
        public static List<Tuple<int, int>> moveIndicesForBlack;
        public static List<Tuple<int, int>> moveIndicesForKings;

        public static ObservableCollection<ObservableCollection<Cell>> GetNewStandardBoard(int nrRows=0, int nrColumns=0)
        {
            if (nrRows == 0)
            {
                nrRows = numberOfRows;
                nrColumns = numberOfColumns;
            }
            else
            {
                numberOfRows = nrRows;
                numberOfColumns = nrColumns;
            }

            ObservableCollection<ObservableCollection<Cell>> board = new ObservableCollection<ObservableCollection<Cell>>();
            for (int i = 0; i < nrRows; ++i)
            {
                ObservableCollection<Cell> row = new ObservableCollection<Cell>();
                for (int j = 0; j < nrColumns; ++j)
                {
                    PieceColorEnum backgroundColor = (i + j) % 2 != 0 ? PieceColorEnum.BLACK : PieceColorEnum.WHITE;
                    Cell cell = new Cell(i, j, backgroundColor);

                    if (i < 3 && (i + j) % 2 != 0)
                    {
                        cell.Piece = new Piece(PieceTypeEnum.PAWN, PieceColorEnum.WHITE);
                    }
                    else if (i > 4 && (i + j) % 2 != 0)
                    {
                        cell.Piece = new Piece(PieceTypeEnum.PAWN, PieceColorEnum.BLACK);
                    }

                    row.Add(cell);
                }
                board.Add(row);
            }
            return board;
        }

        public static ObservableCollection<ObservableCollection<Cell>> GetNewEmptyBoard(int nrRows = 0, int nrColumns = 0)
        {
            if (nrRows == 0)
            {
                nrRows = numberOfRows;
                nrColumns = numberOfColumns;
            }
            else
            {
                numberOfRows = nrRows;
                numberOfColumns = nrColumns;
            }

            ObservableCollection<ObservableCollection<Cell>> board = new ObservableCollection<ObservableCollection<Cell>>();
            for (int i = 0; i < nrRows; ++i)
            {
                ObservableCollection<Cell> row = new ObservableCollection<Cell>();
                for (int j = 0; j < nrColumns; ++j)
                {
                    PieceColorEnum backgroundColor = (i + j) % 2 != 0 ? PieceColorEnum.BLACK : PieceColorEnum.WHITE;
                    Cell cell = new Cell(i, j, backgroundColor);
                    row.Add(cell);
                }
                board.Add(row);
            }
            return board;
        }

        public static GameState LoadSavedGame(string filePath)
        {
            string jsonContent = File.ReadAllText(filePath);
            GameState game = JsonConvert.DeserializeObject<GameState>(jsonContent, new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            });
            return game;
        }

        public static void SaveGame(string filePath, GameState game)
        {
            if (game == null) return;
            string jsonContent = JsonConvert.SerializeObject(game);
            File.WriteAllText(filePath, jsonContent);
        }

        public static GameSettings LoadSettings(string filePath)
        {
            string jsonContent = File.ReadAllText(filePath);
            GameSettings settings = JsonConvert.DeserializeObject<GameSettings>(jsonContent, new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            });
            return settings;
        }

        public static void SaveSettings(string filePath, GameSettings settings)
        {
            if (settings == null) return;
            string jsonContent = JsonConvert.SerializeObject(settings);
            File.WriteAllText(filePath, jsonContent);

        }

        public static List<GameStat> LoadStats(string filePath)
        {
            string jsonContent = File.ReadAllText(filePath);
            List<GameStat> stats = JsonConvert.DeserializeObject<List<GameStat>>(jsonContent, new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            });
            return stats;
        }

        public static void UpdateStatsInFile(string filePath, GameStat stat)
        {
            List<GameStat> statsInFile = LoadStats(filePath);
            if (statsInFile == null) return;
            foreach(var item in statsInFile) 
            {
                if(item.Color == stat.Color)
                {
                    if(item.MaxScore < stat.MaxScore)
                    {
                        item.MaxScore = stat.MaxScore;
                    }
                    item.NumberOfWins++;
                    break;
                }
            }
            SaveStatsInFile(filePath, statsInFile);
        }

        public static void SaveStatsInFile(string filePath, List<GameStat> stats)
        {
            if (stats == null || stats.Count==0) return;
            string jsonContent = JsonConvert.SerializeObject(stats);
            File.WriteAllText(filePath, jsonContent);
        }

        public static void ResetColor(Cell obj)
        {
            PieceColorEnum color = (obj.Row + obj.Column) % 2 != 0 ? PieceColorEnum.BLACK : PieceColorEnum.WHITE;
            switch(color)
            {
                case PieceColorEnum.BLACK:
                    obj.BackgroundColor = nameof(PieceColorEnum.BLACK);
                    break;
                case PieceColorEnum.WHITE:
                    obj.BackgroundColor = nameof(PieceColorEnum.WHITE);
                    break;
            }
        }

        public static bool IsInBoard(int row, int column)
        {
            return ((row >= 0 && row < numberOfRows) && (column >= 0 && column < numberOfColumns));
        }
    }
}
