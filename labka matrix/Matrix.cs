using System;

public class Matrix
{
    private double[,] values;

    public Matrix(double[,] values)
    {
        this.values = values;
    }

    public double this[int i, int j] => values[i, j];
    public int Rows => values.GetLength(0);
    public int Columns => values.GetLength(1);

    public Matrix Transpose()
    {
        double[,] transposedValues = new double[Columns, Rows];
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                transposedValues[j, i] = values[i, j];
            }
        }
        return new Matrix(transposedValues);
    }

    public override string ToString()
    {
        string result = "";
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                result += values[i, j] + "\t";
            }
            result += "\n";
        }
        return result;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Matrix other = (Matrix)obj;
        if (Rows != other.Rows || Columns != other.Columns)
            return false;

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                if (values[i, j] != other[i, j])
                    return false;
            }
        }
        return true;
    }


    private int? hashCode;
    public override int GetHashCode()
    {
        if (hashCode == null)
        {
            int hash = 17;
            unchecked
            {
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        hash = hash * 23 + values[i, j].GetHashCode();
                    }
                }
            }
            hashCode = hash;
        }
        return hashCode.Value;
    }


    public static Matrix Zero(int rows, int columns)
    {
        double[,] zeroValues = new double[rows, columns];
        return new Matrix(zeroValues);
    }


    public static Matrix Zero(int n)
    {
        return Zero(n, n);
    }

    public static Matrix Identity(int n)
    {
        double[,] identityValues = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            identityValues[i, i] = 1;
        }
        return new Matrix(identityValues);
    }

    public static Matrix operator +(Matrix a, Matrix b)
    {
        if (a.Rows != b.Rows || a.Columns != b.Columns)
        {
            throw new ArgumentException("Matrix dimensions must agree.");
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

    public static Matrix operator -(Matrix a, Matrix b)
    {
        if (a.Rows != b.Rows || a.Columns != b.Columns)
        {
            throw new ArgumentException("Matrix dimensions must agree.");
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

    public static Matrix operator *(Matrix a, Matrix b)
    {
        if (a.Columns != b.Rows)
        {
            throw new ArgumentException("Number of columns of the first matrix must equal the number of rows of the second matrix.");
        }

        double[,] resultValues = new double[a.Rows, b.Columns];
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < b.Columns; j++)
            {
                double sum = 0;
                for (int k = 0; k < a.Columns; k++)
                {
                    sum += a[i, k] * b[k, j];
                }
                resultValues[i, j] = sum;
            }
        }
        return new Matrix(resultValues);
    }

    public static Matrix operator *(Matrix matrix, double scalar)
    {
        double[,] resultValues = new double[matrix.Rows, matrix.Columns];
        for (int i = 0; i < matrix.Rows; i++)
        {
            for (int j = 0; j < matrix.Columns; j++)
            {
                resultValues[i, j] = matrix[i, j] * scalar;
            }
        }
        return new Matrix(resultValues);
    }

    public static Matrix operator *(double scalar, Matrix matrix)
    {
        return matrix * scalar;
    }

    public static Matrix operator -(Matrix matrix)
    {
        double[,] resultValues = new double[matrix.Rows, matrix.Columns];
        for (int i = 0; i < matrix.Rows; i++)
        {
            for (int j = 0; j < matrix.Columns; j++)
            {
                resultValues[i, j] = -matrix[i, j];
            }
        }
        return new Matrix(resultValues);
    }
}
