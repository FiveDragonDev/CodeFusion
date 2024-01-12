namespace CodeFusion
{
    public readonly struct Matrix2x2
    {
        public readonly float this[int i, int j]
        {
            get { return _matrix[i, j]; }
            set { _matrix[i, j] = value; }
        }

        private readonly float[,] _matrix;

        public Matrix2x2(float a, float b, float c, float d)
        {
            _matrix = new float[2, 2];
            _matrix[0, 0] = a;
            _matrix[0, 1] = b;
            _matrix[1, 0] = c;
            _matrix[1, 1] = d;
        }

        public static Matrix2x2 operator +(Matrix2x2 m1, Matrix2x2 m2)
        {
            float a = m1[0, 0] + m2._matrix[0, 0];
            float b = m1[0, 1] + m2._matrix[0, 1];
            float c = m1[1, 0] + m2._matrix[1, 0];
            float d = m1[1, 1] + m2._matrix[1, 1];

            return new Matrix2x2(a, b, c, d);
        }
        public static Matrix2x2 operator -(Matrix2x2 m1, Matrix2x2 m2)
        {
            float a = m1[0, 0] - m2._matrix[0, 0];
            float b = m1[0, 1] - m2._matrix[0, 1];
            float c = m1[1, 0] - m2._matrix[1, 0];
            float d = m1[1, 1] - m2._matrix[1, 1];

            return new Matrix2x2(a, b, c, d);
        }
        public static Matrix2x2 operator *(Matrix2x2 m1, Matrix2x2 m2)
        {
            float a = m1[0, 0] * m2._matrix[0, 0] + m1[0, 1] * m2._matrix[1, 0];
            float b = m1[0, 0] * m2._matrix[0, 1] + m1[0, 1] * m2._matrix[1, 1];
            float c = m1[1, 0] * m2._matrix[0, 0] + m1[1, 1] * m2._matrix[1, 0];
            float d = m1[1, 0] * m2._matrix[0, 1] + m1[1, 1] * m2._matrix[1, 1];

            return new Matrix2x2(a, b, c, d);
        }
        public static Matrix2x2 operator /(Matrix2x2 m1, float scalar)
        {
            float a = m1[0, 0] / scalar;
            float b = m1[0, 1] / scalar;
            float c = m1[1, 0] / scalar;
            float d = m1[1, 1] / scalar;

            return new Matrix2x2(a, b, c, d);
        }

        public override readonly string ToString() =>
            $"[{_matrix[0, 0]} {_matrix[0, 1]}]\n[{_matrix[1, 0]} {_matrix[1, 1]}]";
    }

}
