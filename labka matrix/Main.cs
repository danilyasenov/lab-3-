using System;
using System.IO;
using System.Threading.Tasks;

public class Program
{
    public static void Main(string[] args)
    {
        // Примеры использования методов
        TestCreateRandomMatrix(3, 3);
        TestMultiplyMatrices();
        TestScalarProduct();
        TestWriteReadMatrices();
        TestCompareMatrices();
    }

    public static void TestCreateRandomMatrix(int rows, int columns)
    {
        Console.WriteLine("Creating random matrix...");
        var randomMatrix = Matrix.Zero(rows, columns); // Используем статический метод Zero для создания матрицы с нулевыми значениями
        Console.WriteLine(randomMatrix);
    }

    public static void TestMultiplyMatrices()
    {
        Console.WriteLine("Multiplying matrices...");
        var firstMatrix = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
        var secondMatrix = new Matrix(new double[,] { { 5, 6 }, { 7, 8 } });
        var result = MatrixOperations.Multiply(firstMatrix, secondMatrix);
        Console.WriteLine(result);
    }

    public static void TestScalarProduct()
    {
        Console.WriteLine("Calculating scalar product...");
        var firstMatrix = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
        var secondMatrix = new Matrix(new double[,] { { 5, 6 }, { 7, 8 } });
        var scalarProduct = MatrixOperations.ScalarProduct(firstMatrix, secondMatrix);
        Console.WriteLine($"Scalar product: {scalarProduct}");
    }

    public static async Task TestWriteReadMatrices()
    {
        Console.WriteLine("Writing and reading matrices...");
        var matrices = new Matrix[]
        {
        new Matrix(new double[,] { { 1, 2 }, { 3, 4 } }),
        new Matrix(new double[,] { { 5, 6 }, { 7, 8 } }),
        new Matrix(new double[,] { { 9, 10 }, { 11, 12 } }),
        new Matrix(new double[,] { { 13, 14 }, { 15, 16 } }),
        new Matrix(new double[,] { { 17, 18 }, { 19, 20 } }),
        new Matrix(new double[,] { { 21, 22 }, { 23, 24 } }),
        new Matrix(new double[,] { { 25, 26 }, { 27, 28 } }),
        new Matrix(new double[,] { { 29, 30 }, { 31, 32 } }),
        new Matrix(new double[,] { { 33, 34 }, { 35, 36 } }),
        new Matrix(new double[,] { { 37, 38 }, { 39, 40 } })
        };

        // Синхронная запись и чтение
        // Запись матриц в файлы
        MatrixIO.WriteToFileAsync("Matrices", "matrix", ".txt", matrices).Wait();

        // Чтение матриц из файлов
        var readMatrices = MatrixIO.ReadFromFileAsync("Matrices", "matrix", ".txt").Result;

        // Вывод количества прочитанных матриц
        Console.WriteLine($"Read {readMatrices.Length} matrices synchronously.");



        // Синхронная запись и чтение
        // Синхронная запись и чтение
        // Запись матриц в файлы
        await MatrixIO.WriteToFileAsync("Matrices", "matrix", ".txt", matrices);

        // Чтение матриц из файлов
        var readdMatrices = await MatrixIO.ReadFromFileAsync("Matrices", "matrix", ".txt");

        // Вывод количества прочитанных матриц
        Console.WriteLine($"Read {readMatrices.Length} matrices asynchronously.");



    }

    public static void TestCompareMatrices()
    {
        Console.WriteLine("Comparing matrices...");
        var firstMatrix = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
        var secondMatrix = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
        var thirdMatrix = new Matrix(new double[,] { { 5, 6 }, { 7, 8 } });

        Console.WriteLine($"First matrix equals second matrix: {firstMatrix.Equals(secondMatrix)}");
        Console.WriteLine($"First matrix equals third matrix: {firstMatrix.Equals(thirdMatrix)}");
    }
}
