#include <iostream>
#include <random>
#include <stdlib.h>
#include <chrono>

using namespace std;



enum GameEnum
{
    EMPTY = '_',
    CROSS = 'X',
    ZERO = '0',
};

enum GameProgress
{
    IN_PROGRESS,
    HUMAN_WON,
    AI_WON,
    DRAW
};

struct Game
{
    GameEnum** ppField = nullptr;
    const size_t sizeX = 3;
    const size_t sizeY = 3;
    GameEnum human;
    GameEnum ai;
    bool humanFirst;
    GameProgress progress;
};

struct Coord
{
    size_t x, y;
};

void clearScr()
{
    system("cls");
}

int32_t getRandomNum(int32_t min, int32_t max)
{
    const static auto seed = chrono::system_clock::now().time_since_epoch().count();
    static mt19937_64 generator(seed);
    uniform_int_distribution<int32_t> dis(min, max);
    return dis(generator);
}

void printField(Game& rg) 
{
    cout << "   ";
    for (size_t j = 0; j < rg.sizeX; j++)
    {
        cout << " " << j + 1 << "  ";
    }

    cout << endl;

    for (size_t i = 0; i < rg.sizeY; i++)
    {
        cout << i + 1 << " | ";
        for (size_t j = 0; j < rg.sizeX; j++)
        {
            cout << (char)rg.ppField[i][j] << " | ";
        }
        cout << endl;
    }
    cout << endl << "Человек играет: " << (char)rg.human << endl;
    cout << "AI играет: " << (char)rg.ai << endl << endl;
}

void initGame(Game& rg)
{

    // инициализация массива и его заполнения
    rg.ppField = new GameEnum * [rg.sizeY];
    for(int i = 0; i < rg.sizeY; i++)
        rg.ppField[i] = new GameEnum[rg.sizeX];

    for (int i = 0; i < rg.sizeY; i++)
    {
        for (int j = 0; j < rg.sizeX; j++)
        {
            rg.ppField[i][j] = EMPTY;
        }
    }

    //Выбираем кто ходит
    if (getRandomNum(0, 100) > 50)
    {
        rg.human = CROSS;
        rg.ai = ZERO;
        rg.humanFirst = true;

    }
    else
    {
        rg.human = ZERO;
        rg.ai = CROSS;
        rg.humanFirst = false;
    
    }

    rg.progress = IN_PROGRESS;

}

void deinitGame(Game& rg)
{
    for (int i = 0; i < rg.sizeY; i++)
        delete[] rg.ppField[i];

    delete[] rg.ppField;
    rg.ppField = nullptr;
}

Coord getHumanCoor(Game& rg)
{
    Coord c = { 0 };

    do{
        cout << "Введите у: ";
        cin >> c.y;
        cout << "Введите x: ";
        cin >> c.x;
    } while ( c.y <= 0 || c.x <= 0 || c.y > 3 || c.x > 3 || rg.ppField[c.y - 1][c.x - 1] != EMPTY);

    c.x--;
    c.y--;

    return c;
}

Coord GetAICoor(Game& rg)
{
    Coord c = { 0 };

    // центр
    if (rg.ppField[1][1] == EMPTY)
    {
        c = { 1, 1 };
        return c;
    }


    // углы
    if (rg.ppField[0][0] == EMPTY)
    {
        c = { 0, 0 };
        return c;
    }

    if (rg.ppField[0][2] == EMPTY)
    {
        c = { 2, 0 };
        return c;
    }

    if (rg.ppField[2][0] == EMPTY)
    {
        c = { 0, 2 };
        return c;
    }

    if (rg.ppField[2][2] == EMPTY)
    {
        c = { 2, 2 };
        return c;
    }


    // не углы
    if (rg.ppField[0][1] == EMPTY)
    {
        c = { 1, 0 };
        return c;
    } 

    if (rg.ppField[1][0] == EMPTY)
    {
        c = { 0, 1 };
        return c;
    }

    if (rg.ppField[2][1] == EMPTY)
    {
        c = { 1, 2 };
        return c;
    }

    if (rg.ppField[1][2] == EMPTY)
    {
        c = { 2, 1 };
        return c;
    }

}

GameProgress IsWon(Game rg)
{
    // проверка строк
    for(int y = 0; y < rg.sizeY; y++)
    {
        if(rg.ppField[y][0] == rg.ppField[y][1] && rg.ppField[y][1] == rg.ppField[y][2] && rg.ppField[y][1] != EMPTY)
        {
            if (rg.ppField[y][0] == rg.human)
            {
                return HUMAN_WON;
            }
            else
            {
                return AI_WON;
            }
        }
    }
    
    // проверка выигрыша столбцов
    for (int x = 0; x < rg.sizeX; x++)
    {
        if (rg.ppField[1][x] == rg.ppField[0][x] && rg.ppField[1][x] == rg.ppField[2][x] && rg.ppField[1][x] != EMPTY)
        {
            if (rg.ppField[0][x] == rg.human)
            {
                return HUMAN_WON;
            }
            else
            {
                return AI_WON;
            }
        }
    }


    // проверка первой диагонали
    if (rg.ppField[0][0] == rg.ppField[1][1] && rg.ppField[1][1] == rg.ppField[2][2] && rg.ppField[1][1] != EMPTY)
    {
        if (rg.ppField[1][1] == rg.human)
        {
            return HUMAN_WON;
        }
        else
        {
            return AI_WON;
        }
    }

    //проверка второй диагонали
    if (rg.ppField[0][2] == rg.ppField[1][1] && rg.ppField[1][1] == rg.ppField[2][0] && rg.ppField[1][1] != EMPTY)
    {
        if (rg.ppField[1][1] == rg.human)
        {
            return HUMAN_WON;
        }
        else
        {
            return AI_WON;
        }
    }


    bool draw = true;
    for (int y = 0; y < rg.sizeY; y++)
    {
        for (int x = 0; x < rg.sizeX; x++)
        {
            if (rg.ppField[y][x] == EMPTY)
                draw = false;
        }
    }

    if (draw)
        return DRAW;

    return IN_PROGRESS;
}

int main()
{
    setlocale(LC_ALL, "Russian");
    clearScr();
    Game game;
    initGame(game);
    printField(game);
    size_t turn;
    turn = (game.humanFirst) ? 0 : 1;

    while (game.progress == IN_PROGRESS)
    {
        if (turn % 2 == 0)
        {
            Coord c = getHumanCoor(game);
            game.ppField[c.y][c.x] = game.human;
        }
        else
        {
            Coord c = GetAICoor(game);
            game.ppField[c.y][c.x] = game.ai;
        }

        clearScr();
        printField(game);

        turn++;

        game.progress = IsWon(game);
    }

    if (game.progress == DRAW)
    {
        cout << "Ничья";
    }
    else if (game.progress == AI_WON)
        cout << "Выйграл компьютер!" << endl;
    else if (game.progress == HUMAN_WON)
        cout << "Выйграл человек!" << endl;

    deinitGame(game);
}