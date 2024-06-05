using System;
using System.IO;
using System.Threading.Tasks;

class Program2
{
    static async Task Main2(string[] args)
    {
        string directory = "MatrixResults";
        Directory.CreateDirectory(directory);

        await Task.Run(async () =>
        {
            Matrix[] aMatrices = new Matrix[50];
            Matrix[] bMatrices = new Matrix[50];

            for (int i = 0; i < 50; i++)
            {
                aMatrices[i] = Matrix.Random(500, 100);
                bMatrices[i] = Matrix.Random(100, 500);
            }

            await Task.WhenAll(
                Task.Run(async () =>
                {
                    await SaveOperationsResultsAsync(directory, aMatrices, bMatrices);
                }),
                Task.Run(async () =>
                {
                    await SaveMatricesAsync(directory, "StringFormat", "StringMatrix", ".txt", aMatrices, bMatrices);
                }),
                Task.Run(async () =>
                {
                    await SaveMatricesAsync(directory, "JsonFormat", "JsonMatrix", ".json", aMatrices, bMatrices);
                })
            );
        });

        Console.WriteLine("All tasks completed.");
    }

    static async Task SaveOperationsResultsAsync(string directory, Matrix[] aMatrices, Matrix[] bMatrices)
    {
        string resultsDirectory = Path.Combine(directory, "OperationsResults");
        Directory.CreateDirectory(resultsDirectory);

        for (int i = 0; i < aMatrices.Length; i++)
        {
            Matrix resultMatrix = aMatrices[i].Multiply(bMatrices[i]);
            string fileName = $"ResultMatrix_{i}.txt";
            string filePath = Path.Combine(resultsDirectory, fileName);
            await MatrixIO.WriteToFileAsync(filePath, resultMatrix);
        }

        Console.WriteLine("Operation results saved.");
    }

    static async Task SaveMatricesAsync(string directory, string formatFolder, string fileNamePrefix, string fileExtension, Matrix[] aMatrices, Matrix[] bMatrices)
    {
        string formatDirectory = Path.Combine(directory, formatFolder);
        Directory.CreateDirectory(formatDirectory);

        await Task.WhenAll(
            MatrixIO.WriteToFileAsync(formatDirectory, fileNamePrefix + "A", fileExtension, aMatrices),
            MatrixIO.WriteToFileAsync(formatDirectory, fileNamePrefix + "B", fileExtension, bMatrices)
        );

        Console.WriteLine($"Matrices saved in {formatFolder} format.");
    }
}
