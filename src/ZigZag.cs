namespace Mapbox.Vector.Tile
{
    public static class ZigZag
    {
        public static long Decode(long n)
        {
            return (n >> 1) ^ (-(n & 1));
        }

        public static long Encode(long n)
        {
            return (n << 1) ^ (n >> 31);
        }
   }
}
