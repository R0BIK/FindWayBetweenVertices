List<List<int>> weightMatrix = new List<List<int>>();

Console.WriteLine("Вітаю в програмі пошуку найкоротшого шляху між парою вершин графа\n" +
                  "Оберіть метод задання матриці вагів\n" +
                  "1. Ввести самостійно\n" +
                  "2. Сгенерувати");

int method = int.Parse(Console.ReadLine());

Console.WriteLine("Введіть кількість (вершин) елементів матриці, мінімум 2:");
int n = int.Parse(Console.ReadLine());
if (n < 2)
{
    Console.WriteLine("Помилка");
    return;
}

if (method == 1)
    GetMatrix();
else if (method == 2)
    GenerateMatrix();
else
{
    Console.WriteLine("Помилка!");
}

Console.Clear();
Console.WriteLine("Матриця вагів графу:\n");
WriteCurrentMatrix();

Console.WriteLine("\nВведіть номер початкової вершини: ");
int start = int.Parse(Console.ReadLine());
int current = start - 1;

int inf = Int32.MaxValue;
List<int> distance = new List<int>();
List<bool> visited = new List<bool>();

Dijkstra();
PrintResult();

void PrintResult()
{
    Console.WriteLine($"\nВідстань з вершини {start} до вершини:");

    for (int i = 0; i < n; ++i)
    {
        if (i != start - 1)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(i + 1);
            Console.ResetColor();
            if (distance[i] >= inf)
                Console.Write(" - Дістатися неможливо\n");
            else
            {
                Console.Write($" - {distance[i]}\n");
            }
        }
    }
}

void Dijkstra()
{
    for (int i = 0; i < n; ++i)
    {
        if (i == current)
            distance.Add(0);
        else
        {
            distance.Add(inf);
        }
        visited.Add(false);
    }

    for (int j = 0; j < n; ++j)
    {
        for (int i = 0; i < n; ++i)
        {
            if (weightMatrix[current][i] != 0 && !visited[i])
            {
                if (distance[i] > distance[current] + weightMatrix[current][i])
                    distance[i] = distance[current] + weightMatrix[current][i];
            }
        }

        visited[current] = true;
        current = FindMinVertex();
    }
}

int FindMinVertex()
{
    int min = inf;
    int minIndex = 0;
    
    for (int i = 0; i < n; ++i)
    {
        if (distance[i] < min && !visited[i])
        {
            minIndex = i;
            min = distance[i];
        }
        
    }

    return minIndex;
}

void GetMatrix()
{
    Console.WriteLine("Почніть вводити елементи матриці:\n");
    for (int i = 0; i < n; ++i)
    {
        List<int> list = new List<int>();
        
        for (int j = 0; j < n; ++j)
        {
            Console.Clear();
            WriteCurrentMatrix();
            WriteCurrList(list);
            list.Add(Int32.Parse(Console.ReadLine()));
        }

        weightMatrix.Add(list);
    }
}

void WriteCurrList(List<int> list)
{
    if (list.Count == 0)
        return;
    for (int i = 0; i < list.Count; ++i)
    {
        Console.Write(list[i]);
        Console.Write("\t");
    }
}

void WriteCurrentMatrix()
{
    int matrixSize = weightMatrix.Count;
    if (matrixSize != 0)
    {
        int listSize = weightMatrix[matrixSize - 1].Count;
        if (listSize < n)
            --matrixSize;
    }
    
    for (int i = -1; i < matrixSize; ++i)
    {
        if (matrixSize == n && i != -1)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(i + 1 + "\t");
            Console.ResetColor();
        }
        for (int j = 0; j < n; ++j)
        {
            if (i == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"\t{j + 1}");
                Console.ResetColor();
            }
            else
            {
                Console.Write(weightMatrix[i][j]);
                Console.Write("\t");
            }
        }
        if (i < matrixSize)
            Console.Write("\n");
        if (matrixSize < n)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(i + 2 + "\t");
            Console.ResetColor();
        }
    }
}

void GenerateMatrix()
{
    Random random = new Random();
    for (int i = 0; i < n; ++i)
    {
        List<int> list = new List<int>();

        for (int j = 0; j < n; ++j)
        {
            int g = random.Next(-n, n);
            if (g < 0)
                list.Add(0);
            else
            {
                list.Add(g);
            }
        }
        
        weightMatrix.Add(list);
    }
}


