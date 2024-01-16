namespace CodeFusion.Utils
{
    public abstract class PerlinNoise<GradientType>
    {
        public int Seed { get; set; }

        private readonly Func<float, float> _smoothingFunction;

        protected int[] _permTable;
        private static readonly int[] _permutation = {151,160,137,91,90,15,
           131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,
           190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,
           88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,139,48,27,166,
           77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,40,244,
           102,143,54,65,25,63,161,1,216,80,73,209,76,132,187,208,89,18,169,200,196,
           135,130,116,188,159,86,164,100,109,198,173,186,3,64,52,217,226,250,124,123,
           5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,
           223,183,170,213,119,248,152,2,44,154,163,70,221,153,101,155,167,43,172,9,
           129,22,39,253,19,98,108,110,79,113,224,232,178,185,112,104,218,246,97,228,
           251,34,242,193,238,210,144,12,191,179,162,241,81,51,145,235,249,14,239,107,
           49,192,214,31,181,199,106,157,184,84,204,176,115,121,50,45,127,4,150,254,
           138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,180,
           151,160,137,91,90,15,
           131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,
           190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,
           88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,139,48,27,166,
           77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,40,244,
           102,143,54,65,25,63,161,1,216,80,73,209,76,132,187,208,89,18,169,200,196,
           135,130,116,188,159,86,164,100,109,198,173,186,3,64,52,217,226,250,124,123,
           5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,
           223,183,170,213,119,248,152,2,44,154,163,70,221,153,101,155,167,43,172,9,
           129,22,39,253,19,98,108,110,79,113,224,232,178,185,112,104,218,246,97,228,
           251,34,242,193,238,210,144,12,191,179,162,241,81,51,145,235,249,14,239,107,
           49,192,214,31,181,199,106,157,184,84,204,176,115,121,50,45,127,4,150,254,
           138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,180};

        private readonly GradientType[] _gradients;
        private readonly Func<GradientType, float, float, float, float> _dot;

        protected PerlinNoise(GradientType[] gradients, Func<GradientType, float, float, float, float> dot,
            Func<float, float> smoothingFunction, int seed)
        {
            _gradients = gradients;
            _dot = dot;
            _smoothingFunction = smoothingFunction;
            _permTable = _permutation;

            Seed = seed;
            var random = new System.Random(seed);

            for (int i = 0; i < _permTable.Length; i++)
            {
                _permTable[i] = random.Next(0, 255);
            }
        }
        public float Noise(float x, float y = 0.5f, float z = 0.5f)
        {
            int cubeX = ((int)x) & (_permTable.Length / 2 - 1);
            int cubeY = ((int)y) & (_permTable.Length / 2 - 1);
            int cubeZ = ((int)z) & (_permTable.Length / 2 - 1);

            int XIndex = _permTable[cubeX] + cubeY;
            int X1Index = _permTable[cubeX + 1] + cubeY;
            //indexes for the gradients
            GradientType V000 = _gradients[_permTable[_permTable[XIndex] + cubeZ] % _gradients.Length];
            GradientType V001 = _gradients[_permTable[_permTable[XIndex] + cubeZ + 1] % _gradients.Length];
            GradientType V010 = _gradients[_permTable[_permTable[XIndex + 1] + cubeZ] % _gradients.Length];
            GradientType V011 = _gradients[_permTable[_permTable[XIndex + 1] + cubeZ + 1] % _gradients.Length];
            GradientType V100 = _gradients[_permTable[_permTable[X1Index] + cubeZ] % _gradients.Length];
            GradientType V101 = _gradients[_permTable[_permTable[X1Index] + cubeZ + 1] % _gradients.Length];
            GradientType V110 = _gradients[_permTable[_permTable[X1Index + 1] + cubeZ] % _gradients.Length];
            GradientType V111 = _gradients[_permTable[_permTable[X1Index + 1] + cubeZ + 1] % _gradients.Length];

            x -= MathF.Floor(x);
            y -= MathF.Floor(y);
            z -= MathF.Floor(z);

            float V000Dot = _dot(V000, x, y, z);
            float V001Dot = _dot(V001, x, y, z - 1);
            float V010Dot = _dot(V010, x, y - 1, z);
            float V011Dot = _dot(V011, x, y - 1, z - 1);
            float V100Dot = _dot(V100, x - 1, y, z);
            float V101Dot = _dot(V101, x - 1, y, z - 1);
            float V110Dot = _dot(V110, x - 1, y - 1, z);
            float V111Dot = _dot(V111, x - 1, y - 1, z - 1);

            float smoothedX = _smoothingFunction(x);
            float smoothedY = _smoothingFunction(y);
            float smoothedZ = _smoothingFunction(z);

            float V000V100Val = LinearlyInterpolate(V000Dot, V100Dot, smoothedX);
            float V001V101Val = LinearlyInterpolate(V001Dot, V101Dot, smoothedX);
            float V010V110Val = LinearlyInterpolate(V010Dot, V110Dot, smoothedX);
            float V011V111Val = LinearlyInterpolate(V011Dot, V111Dot, smoothedX);

            float ZZeroPlaneVal = LinearlyInterpolate(V000V100Val, V010V110Val, smoothedY);
            float ZOnePlaneVal = LinearlyInterpolate(V001V101Val, V011V111Val, smoothedY);

            return LinearlyInterpolate(ZZeroPlaneVal, ZOnePlaneVal, smoothedZ);
        }
        public float NoiseTiled(float x, float y = 0.5f, float z = 0.5f, int tileRegion = 2)
        {
            int cubeX = ((int)x) & (_permTable.Length / 2 - 1);
            int cubeY = ((int)y) & (_permTable.Length / 2 - 1);
            int cubeZ = ((int)z) & (_permTable.Length / 2 - 1);
            int XIndex = _permTable[cubeX % tileRegion] + cubeY % tileRegion;
            int X1Index = _permTable[(cubeX + 1) % tileRegion] + cubeY % tileRegion;
            int XIndex1 = _permTable[cubeX % tileRegion] + (cubeY + 1) % tileRegion;
            int X1Index1 = _permTable[(cubeX + 1) % tileRegion] + (cubeY + 1) % tileRegion;
            GradientType V000 = _gradients[_permTable[_permTable[XIndex] + cubeZ % tileRegion] % _gradients.Length];
            GradientType V001 = _gradients[_permTable[_permTable[XIndex] + (cubeZ + 1) % tileRegion] % _gradients.Length];
            GradientType V010 = _gradients[_permTable[_permTable[XIndex1] + cubeZ % tileRegion] % _gradients.Length];
            GradientType V011 = _gradients[_permTable[_permTable[XIndex1] + (cubeZ + 1) % tileRegion] % _gradients.Length];
            GradientType V100 = _gradients[_permTable[_permTable[X1Index] + cubeZ % tileRegion] % _gradients.Length];
            GradientType V101 = _gradients[_permTable[_permTable[X1Index] + (cubeZ + 1) % tileRegion] % _gradients.Length];
            GradientType V110 = _gradients[_permTable[_permTable[X1Index1] + cubeZ % tileRegion] % _gradients.Length];
            GradientType V111 = _gradients[_permTable[_permTable[X1Index1] + (cubeZ + 1) % tileRegion] % _gradients.Length];
            x -= MathF.Floor(x);
            y -= MathF.Floor(y);
            z -= MathF.Floor(z);
            float V000Dot = _dot(V000, x, y, z);
            float V001Dot = _dot(V001, x, y, z - 1);
            float V010Dot = _dot(V010, x, y - 1, z);
            float V011Dot = _dot(V011, x, y - 1, z - 1);
            float V100Dot = _dot(V100, x - 1, y, z);
            float V101Dot = _dot(V101, x - 1, y, z - 1);
            float V110Dot = _dot(V110, x - 1, y - 1, z);
            float V111Dot = _dot(V111, x - 1, y - 1, z - 1);
            float smoothedX = _smoothingFunction(x);
            float smoothedY = _smoothingFunction(y);
            float smoothedZ = _smoothingFunction(z);
            float V000V100Val = LinearlyInterpolate(V000Dot, V100Dot, smoothedX);
            float V001V101Val = LinearlyInterpolate(V001Dot, V101Dot, smoothedX);
            float V010V110Val = LinearlyInterpolate(V010Dot, V110Dot, smoothedX);
            float V011V111Val = LinearlyInterpolate(V011Dot, V111Dot, smoothedX);
            float ZZeroPlaneVal = LinearlyInterpolate(V000V100Val, V010V110Val, smoothedY);
            float ZOnePlaneVal = LinearlyInterpolate(V001V101Val, V011V111Val, smoothedY);
            return LinearlyInterpolate(ZZeroPlaneVal, ZOnePlaneVal, smoothedZ);
        }
        public float NoiseOctaves(float x, float y, float z = 0.5f,
            int numOctaves = 6, float lacunarity = 2f, float persistence = 0.5f)
        {
            float noiseValue = 0;
            float amp = 1;
            float freq = 1;
            float totalAmp = 0;

            for (int i = 0; i < numOctaves; i++)
            {
                noiseValue += amp * Noise(x * freq, y * freq, z * freq);
                totalAmp += amp;
                amp *= persistence;
                freq *= lacunarity;
            }

            return noiseValue / totalAmp;
        }
        public float NoiseTiledOctaves(float x, float y, float z, int tileRegion = 2,
            int numOctaves = 6, float lacunarity = 2f, float persistence = 0.5f)
        {
            float noiseValue = 0f;
            float amp = 1f;
            float freq = 1f;
            float totalAmp = 0f;

            for (int i = 0; i < numOctaves; i++)
            {
                noiseValue += amp * NoiseTiled(x * freq, y * freq, z * freq, tileRegion);
                totalAmp += amp;
                amp *= persistence;
                freq *= lacunarity;
            }

            return noiseValue / totalAmp;
        }

        private static float LinearlyInterpolate(float valueA, float valueB, float t)
        {
            return valueA + t * (valueB - valueA);
        }
        protected static float SmoothToSCurve(float val)
        {
            return val * val * val * (val * (val * 6f - 15f) + 10f);
        }
    }
    public class PerlinNoise : PerlinNoise<Vector>
    {
        private static readonly Vector[] _gradients = {new Vector(1,1,0), new Vector(-1,1,-0),
        new Vector(1,-1,0), new Vector(-1,-1,0), new Vector(1,0,1),
        new Vector(-1,0,1), new Vector(1,0,-1), new Vector(-1,0,-1),
        new Vector(0,1,1), new Vector(0,-1,1), new Vector(0,1,-1),
        new Vector(0,-1,-1)};

        private PerlinNoise(Func<float, float> smoothingFunction, int seed) :
        base(_gradients, Dot, smoothingFunction, seed) { }

        public PerlinNoise(int seed) : this(SmoothToSCurve, seed) { }
        public PerlinNoise() : this(SmoothToSCurve, (int)(DateTime.Now.Ticks * DateTime.UtcNow.Year)) { }

        private static float Dot(Vector gradient, float x, float y, float z)
        {
            return gradient.x * x + gradient.y * y + gradient.z * z;
        }
    }
    public class Vector
    {
        public float x;
        public float y;
        public float z;

        public Vector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
