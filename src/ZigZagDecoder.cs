namespace mapbox.vector.tile
{
    public class ZigZagDecoder
    {
        public static long ZigZagDecode(long n)
        {
            return (n >> 1) ^ (-(n & 1));
        }
   }
}
