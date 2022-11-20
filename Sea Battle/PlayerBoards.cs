using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Sea_Battle
{
    public class PlayerBoards
    {
        private bool _readyPlayer1 = false;
        private bool _readyPlayer2 = false;
        public bool readyPlayer1
        {
            get { return _readyPlayer1; }
            set
            {
                _readyPlayer1 = value;
                if (_readyPlayer2) gameStarted = true;
            }
        }

        public bool readyPlayer2
        {
            get { return _readyPlayer2; }
            set
            {
                _readyPlayer2 = value;
                if (_readyPlayer1) gameStarted = true;
            }
        }

        public bool gameStarted = false;
        public int winner = 0;
        public bool turnPlayer1 = true;
        //We can have these states for each slot:
        //0 - empty slot
        //1 - slot with ship
        //2 - hit slot with ship
        //3 - hit empty slot
        //4 - fully destroyed ship tile
        //5 - fog (not revealed) same texture as 0

        ObservableCollection<int> _boardPlayer1;
        ObservableCollection<int> _boardPlayer2;

        ObservableCollection<int> _enemyPlayer1;
        ObservableCollection<int> _enemyPlayer2;

        List<int[]> battleshipsP1 = new List<int[]>();
        List<int[]> cruisersP1 = new List<int[]>();
        List<int[]> submarinesP1 = new List<int[]>();
        List<int[]> destroyersP1 = new List<int[]>();

        List<int[]> battleshipsP2 = new List<int[]>();
        List<int[]> cruisersP2 = new List<int[]>();
        List<int[]> submarinesP2 = new List<int[]>();
        List<int[]> destroyersP2 = new List<int[]>();

        public PlayerBoards()
        {
            _boardPlayer1 = fillWithZeros();
            _boardPlayer2 = fillWithZeros();
            _enemyPlayer1 = fillWithZeros();
            _enemyPlayer2 = fillWithZeros();
        }

        public ObservableCollection<int> BoardPlayer1 {
            get
            {
                return _boardPlayer1;
            }
        }
        public ObservableCollection<int> BoardPlayer2 {
            get
            {
                return _boardPlayer2;
            }
        }

        public ObservableCollection<int> EnemyPlayer1
        {
            get
            {
                return _enemyPlayer1;
            }
        }
        public ObservableCollection<int> EnemyPlayer2
        {
            get
            {
                return _enemyPlayer2;
            }
        }

        public void Shoot(ObservableCollection<int> target, ObservableCollection<int> display, int slot, int player)
        {
            if(target[slot] == 1)
            {
                target[slot] = 2;
                display[slot] = 2;

                if(!target.Where(x => x == 1).Any())
                {
                    winner = player;
                }
            }
            else if(target[slot] == 0)
            {
                target[slot] = 3;
                display[slot] = 3;
                turnPlayer1 = !turnPlayer1;
            }
        }

        public void Set(ObservableCollection<int> target, int slot)
        {
            if (target[slot] == 0) target[slot] = 1;
            else target[slot] = 0;
        }

        public string DisplayP1Board()
        {
            string result = "";

            foreach (int val in _boardPlayer1){
                result += " " + val;
            }

            return result;
        }

        private ObservableCollection<int> fillWithZeros()
        {
            ObservableCollection<int> result = new ObservableCollection<int>();
            for(int i = 0; i < 100; i++)
            {
                result.Add(0);
            }
            return result;
        }

        private string convertSelectedShipList(List<int[]> list)
        {
            string res = "";
            foreach (int[] item in list)
            {
                res += "{";
                foreach (int item2 in item)
                {
                    res += $"{item2} ";
                }
                res += "} ";
            }
            return res;
        }

        public string getShipTilesLocation(bool player1) 
        {
            if (player1) return $"Battleship: {convertSelectedShipList(battleshipsP1)}\n Cruisers: {convertSelectedShipList(cruisersP1)}\n Submarines: {convertSelectedShipList(submarinesP1)}\n Destroyers: {convertSelectedShipList(destroyersP1)}";
            else return $"Battleship: {convertSelectedShipList(battleshipsP2)}\n Cruisers: {convertSelectedShipList(cruisersP2)}\n Submarines: {convertSelectedShipList(submarinesP2)}\n Destroyers: {convertSelectedShipList(destroyersP2)}";
        }

        public string LayoutIsValid(ObservableCollection<int> layout, bool player1)
        {
            string success = "";

            //int[,] battleships = new int[1,4];
            //int[,] cruisers = new int[2, 3];
            //int[,] submarines = new int[3, 2];
            //int[,] destroyers = new int[4, 1];

            //Chech 4 positions:
            //+10
            //-10
            //+1
            //-1

            //Diagonal 4 positions:
            //+11
            //-11
            //+9
            //-9

            List<LeftOverTile> leftOverTiles = new List<LeftOverTile>();

            if (layout.Sum() != 20) 
            {
                return $"Number of ship tiles should be equal to 20 ({layout.Sum()} selected)";
            }


            for (int i = 0; i < layout.Count; i++)
            {

                if(layout[i] == 1)
                {
                    int checkLayout(int input)
                    {
                        if (i % 10 == 0)
                        {
                            if (input == i - 11 || input == i - 1 || input == i + 9) return 0;
                        }
                        else if(i % 10 == 9)
                        {
                            if (input == i + 11 || input == i + 1 || input == i - 9) return 0;
                        }

                        if (input > 0 && input < layout.Count())
                        {
                            return layout[input];
                        }
                        else return 0;
                    }

                    int _selSlotsCount = 0;
                    int[] envDiagonal = new int[] { i - 9, i + 11, i + 9, i - 11 };
                    int[] envStraight = new int[] { i - 10, i + 1, i + 10, i - 1 };

                    foreach (int j in envDiagonal)
                    {
                        if (checkLayout(j) == 1)
                        {
                            return $"Diagonal tiles too close! (tile: {i}, too close: {j})";
                        }
                    }

                    foreach (int j in envStraight)
                    {

                        if (checkLayout(j) == 1)
                        {
                            _selSlotsCount++;
                        }
                    }

                    if (_selSlotsCount == 0)
                    {
                        if (player1)
                        {
                            if (destroyersP1.Count < 4)
                            {
                                destroyersP1.Add(new int[] { i });
                                continue;
                            }
                            else
                            {
                                return $"More than 4 destroyers are placed! (selected tiles: {convertSelectedShipList(destroyersP1)}, current tile: {i})";
                            }
                        }
                        else
                        {
                            if (destroyersP2.Count < 4)
                            {
                                destroyersP2.Add(new int[] { i });
                                continue;
                            }
                            else
                            {
                                return $"More than 4 destroyers are placed! (selected tiles: {convertSelectedShipList(destroyersP2)}, current tile: {i})";
                            }
                        }
                    }

                    if (_selSlotsCount > 2 || checkLayout(envStraight[0]) + checkLayout(envStraight[1]) == 2 || checkLayout(envStraight[1]) + checkLayout(envStraight[2]) == 2 || checkLayout(envStraight[2]) + checkLayout(envStraight[3]) == 2 || checkLayout(envStraight[3]) + checkLayout(envStraight[0]) == 2) //Check for bending

                    {
                        return $"Ships cannot bend! (tile: {i})";
                    }

                    LeftOverTile prevTile = leftOverTiles.Find(tile => tile.target == i);

                    int _target = -1;
                    string _direction;
                    bool _isLast = false;
                    int _num;

                    if(prevTile != null)
                    {
                        _direction = prevTile.direction;
                        _num = prevTile.num + 1;
                        if(prevTile.direction == "horisontal")
                        {
                            if(checkLayout(envStraight[1]) == 1)
                            {
                                _target = envStraight[1];
                            }
                            else
                            {
                                _isLast = true;
                                string res = lastTileFuncReturn(_num, "horisontal");
                                if (res != "") return res;

                            }
                        }
                        else 
                        {
                            if (checkLayout(envStraight[2]) == 1)
                            {
                                _target = envStraight[2];
                            }
                            else
                            {
                                _isLast = true;
                                string res = lastTileFuncReturn(_num, "vertical");
                                if (res != "") return res;
                            }
                        }
                    }
                    else
                    {
                        if (checkLayout(envStraight[1]) == 1)
                        {
                            _direction = "horisontal";
                            _target = envStraight[1];
                        }
                        else
                        {
                            _direction = "vertical";
                            _target = envStraight[2];
                        }
                        _num = 1;
                    }

                    string lastTileFuncReturn(int __num, string dir)
                    {
                        int offset;

                        if(dir == "horisontal")
                        {
                            offset = -1;
                        }
                        else
                        {
                            offset = -10;
                        }

                        switch (__num)
                        {
                            case 2:
                                if (player1)
                                {
                                    if (submarinesP1.Count < 3)
                                    {
                                        submarinesP1.Add(new int[] { i, i + offset });
                                    }
                                    else
                                    {
                                        return $"More than 3 submarines are placed! (selected tiles: {convertSelectedShipList(submarinesP1)}, current tile: {i})";
                                    }
                                }
                                else
                                {
                                    if (submarinesP2.Count < 3)
                                    {
                                        submarinesP2.Add(new int[] { i, i + offset });
                                    }
                                    else
                                    {
                                        return $"More than 3 submarines are placed! (selected tiles: {convertSelectedShipList(submarinesP2)}, current tile: {i})";
                                    }
                                }
                                break;
                            case 3:
                                if (player1)
                                {
                                    if (cruisersP1.Count < 2)
                                    {
                                        cruisersP1.Add(new int[] { i, i + offset, i + 2 * offset });
                                    }
                                    else
                                    {
                                        return $"More than 2 cruisers are placed! (selected tiles: {convertSelectedShipList(cruisersP1)}, current tile: {i})";
                                    }
                                }
                                else
                                {
                                    if (cruisersP2.Count < 2)
                                    {
                                        cruisersP2.Add(new int[] { i, i + offset, i + 2 * offset });
                                    }
                                    else
                                    {
                                        return $"More than 2 cruisers are placed! (selected tiles: {convertSelectedShipList(cruisersP2)}, current tile: {i})";
                                    }
                                }
                                break;
                            case 4:
                                if (player1)
                                {
                                    if (battleshipsP1.Count < 2)
                                    {
                                        battleshipsP1.Add(new int[] { i, i + offset, i + 2 * offset, i + 3 * offset });
                                    }
                                    else
                                    {
                                        return $"More than 1 battleship is placed! (selected tiles: {convertSelectedShipList(battleshipsP1)}, current tile: {i})";
                                    }
                                }
                                else
                                {
                                    if (battleshipsP2.Count < 2)
                                    {
                                        battleshipsP2.Add(new int[] { i, i + offset, i + 2 * offset, i + 3 * offset });
                                    }
                                    else
                                    {
                                        return $"More than 1 battleship is placed! (selected tiles: {convertSelectedShipList(battleshipsP2)}, current tile: {i})";
                                    }
                                }
                                break;
                            default: return $"Error: switch default";
                        }
                        return "";
                    }
                    leftOverTiles.Add(new LeftOverTile(i, _target, _direction, _isLast, _num));
                }
            }

            return success;
        }
    }
}
