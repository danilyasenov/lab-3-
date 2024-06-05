using System;
using System.Threading.Tasks;

public static class MatrixOperations
{
    public static Matrix Transpose(Matrix matrix)
    {
        double[,] transposedValues = new double[matrix.Columns, matrix.Rows];
        for (int i = 0; i < matrix.Rows; i++)
        {
            for (int j = 0; j < matrix.Columns; j++)
            {
                transposedValues[j, i] = matrix[i, j];
            }
        }
        return new Matrix(transposedValues);
    }

    public static Matrix ScalarProduct(Matrix matrix, Matrix scalarMatrix)
    {
        if (matrix.Rows != scalarMatrix.Rows || matrix.Columns != scalarMatrix.Columns)
        {
            throw new InvalidOperationException("Matrices must have the same dimensions.");
        }

        double[,] resultValues = new double[matrix.Rows, matrix.Columns];
        for (int i = 0; i < matrix.Rows; i++)
        {
            for (int j = 0; j < matrix.Columns; j++)
            {
                resultValues[i, j] = matrix[i, j] * scalarMatrix[i, j];
            }
        }
        return new Matrix(resultValues);
    }


    public static Matrix Add(Matrix a, Matrix b)
    {
        if (a.Rows != b.Rows || a.Columns != b.Columns)
        {
            throw new MatrixOperationException("Matrix dimensions must agree for addition.");
        }

        double[,] resultValues = new double[a.Rows, a.Columns];
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < a.Columns; j++)
            {
                resultValues[i, j] = a[i, j] + b[i, j];
            }
        }
        return new Matrix(resultValues);
    }

    public static Matrix Subtract(Matrix a, Matrix b)
    {
        if (a.Rows != b.Rows || a.Columns != b.Columns)
        {
            throw new MatrixOperationException("Matrix dimensions must agree for subtraction.");
        }

        double[,] resultValues = new double[a.Rows, a.Columns];
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < a.Columns; j++)
            {
                resultValues[i, j] = a[i, j] - b[i, j];
            }
        }
        return new Matrix(resultValues);
    }

    public static Matrix Multiply(Matrix a, Matrix b)
    {
        if (a.Columns != b.Rows)
        {
            throw new MatrixOperationException("Number of columns of the first matrix must equal the number of rows of the second matrix for multiplication.");
        }

        b = Transpose(b);

        double[,] resultValues = new double[a.Rows, b.Rows];
        Parallel.For(0, a.Rows, i =>
        {
            for (int j = 0; j < b.Rows; j++)
            {
                double sum = 0;
                for (int k = 0; k < a.Columns; k++)
                {
                    sum += a[i, k] * b[j, k];
                }
                resultValues[i, j] = sum;
            }
        });

        return new Matrix(resultValues);
    }
}

public class MatrixOperationException : Exception
{
    public MatrixOperationException(string message) : base(message) { }
}
