namespace CodeFusion
{
    public readonly struct Matrix3x3
    {
        public float this[int i, int j]
        {
            get => _matrix[i, j];
            set => _matrix[i, j] = value;
        }

        private readonly float[,] _matrix;

        public Matrix3x3(float a, float b, float c, float d,
            float e, float f, float g, float h, float i)
        {
            _matrix = new float[3, 3];
            _matrix[0, 0] = a;
            _matrix[0, 1] = b;
            _matrix[0, 2] = c;
            _matrix[1, 0] = d;
            _matrix[1, 1] = e;
            _matrix[1, 2] = f;
            _matrix[2, 0] = g;
            _matrix[2, 1] = h;
            _matrix[2, 2] = i;
        }

        public static Matrix3x3 operator +(Matrix3x3 m1, Matrix3x3 m2)
        {
            Matrix3x3 result = new();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result[i, j] = m1[i, j] + m2[i, j];
                }
            }

            return new Matrix3x3(result[0, 0], result[0, 1], result[0, 2], result[1, 0],
                result[1, 1], result[1, 2], result[2, 0], result[2, 1], result[2, 2]);
        }

        public static Matrix3x3 operator -(Matrix3x3 m1, Matrix3x3 m2)
        {
            Matrix3x3 result = new();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result[i, j] = m1[i, j] - m2[i, j];
                }
            }

            return new Matrix3x3(result[0, 0], result[0, 1], result[0, 2], result[1, 0],
                result[1, 1], result[1, 2], result[2, 0], result[2, 1], result[2, 2]);
        }

        public static Matrix3x3 operator *(Matrix3x3 m1, Matrix3x3 m2)
        {
            Matrix3x3 result = new();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        result[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }

            return new Matrix3x3(result[0, 0], result[0, 1], result[0, 2], result[1, 0],
                result[1, 1], result[1, 2], result[2, 0], result[2, 1], result[2, 2]);
        }

        public static Matrix3x3 operator /(Matrix3x3 m, float scalar)
        {
            Matrix3x3 result = new();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result[i, j] = m[i, j] / scalar;
                }
            }

            return new Matrix3x3(result[0, 0], result[0, 1], result[0, 2], result[1, 0],
                result[1, 1], result[1, 2], result[2, 0], result[2, 1], result[2, 2]);
        }
        
        public override string ToString() =>
            $"[{this[0, 0]} {this[0, 1]} {this[0, 2]}]\n" +
            $"[{this[1, 0]} {this[1, 1]} {this[1, 2]}]\n" +
            $"[{this[2, 0]} {this[2, 1]} {this[2, 2]}]";
    }
}
