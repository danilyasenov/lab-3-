using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

public static class MatrixIO
{
    public static async Task WriteTextAsync(Matrix matrix, Stream stream, string sep = " ")
    {
        using (StreamWriter writer = new StreamWriter(stream))
        {
            await writer.WriteLineAsync($"{matrix.Rows} {matrix.Columns}");
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    await writer.WriteAsync(matrix[i, j].ToString());
                    if (j < matrix.Columns - 1)
                        await writer.WriteAsync(sep);
                }
                await writer.WriteLineAsync();
            }
        }
    }

    public static async Task<Matrix> ReadTextAsync(Stream stream, string sep = " ")
    {
        using (StreamReader reader = new StreamReader(stream))
        {
            string[] dimensions = (await reader.ReadLineAsync()).Split(' ');
            int rows = int.Parse(dimensions[0]);
            int columns = int.Parse(dimensions[1]);

            double[,] values = new double[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                string[] lineValues = (await reader.ReadLineAsync()).Split(new[] { sep }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < columns; j++)
                {
                    values[i, j] = double.Parse(lineValues[j]);
                }
            }

            return new Matrix(values);
        }
    }

    public static void WriteBinary(Matrix matrix, Stream stream)
    {
        using (BinaryWriter writer = new BinaryWriter(stream))
        {
            writer.Write(matrix.Rows);
            writer.Write(matrix.Columns);
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    writer.Write(matrix[i, j]);
                }
            }
        }
    }

    public static Matrix ReadBinary(Stream stream)
    {
        using (BinaryReader reader = new BinaryReader(stream))
        {
            int rows = reader.ReadInt32();
            int columns = reader.ReadInt32();

            double[,] values = new double[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    values[i, j] = reader.ReadDouble();
                }
            }

            return new Matrix(values);
        }
    }

    public static async Task WriteJsonAsync(Matrix matrix, Stream stream)
    {
        double[][] jsonValues = new double[matrix.Rows][];
        for (int i = 0; i < matrix.Rows; i++)
        {
            jsonValues[i] = new double[matrix.Columns];
            for (int j = 0; j < matrix.Columns; j++)
            {
                jsonValues[i][j] = matrix[i, j];
            }
        }

        using (StreamWriter writer = new StreamWriter(stream))
        {
            await writer.WriteAsync(JsonConvert.SerializeObject(jsonValues));
        }
    }

    public static async Task<Matrix> ReadJsonAsync(Stream stream)
    {
        using (StreamReader reader = new StreamReader(stream))
        {
            string json = await reader.ReadToEndAsync();
            double[][] jsonValues = JsonConvert.DeserializeObject<double[][]>(json);

            double[,] values = new double[jsonValues.Length, jsonValues[0].Length];
            for (int i = 0; i < jsonValues.Length; i++)
            {
                for (int j = 0; j < jsonValues[0].Length; j++)
                {
                    values[i, j] = jsonValues[i][j];
                }
            }

            return new Matrix(values);
        }
    }

    public static void WriteToFile(string directory, string fileName, Matrix matrix, Action<Matrix, Stream> writeMethod)
    {
        string filePath = Path.Combine(directory, fileName);
        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            writeMethod(matrix, stream);
        }
    }

    public static async Task WriteToFileAsync(string directory, string fileName, string fileExtension, Matrix[] matrices)
    {
        for (int i = 0; i < matrices.Length; i++)
        {
            string filePath = Path.Combine(directory, $"{fileName}{i}{fileExtension}");
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                await WriteTextAsync(matrices[i], stream);
            }
        }
    }

    public static async Task<Matrix[]> ReadFromFileAsync(string directory, string fileName, string fileExtension)
    {
        List<Matrix> matrices = new List<Matrix>();
        string[] filePaths = Directory.GetFiles(directory, $"{fileName}*{fileExtension}");
        foreach (string filePath in filePaths)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                Matrix matrix = await ReadTextAsync(stream);
                matrices.Add(matrix);
            }
        }
        return matrices.ToArray();
    }
}
