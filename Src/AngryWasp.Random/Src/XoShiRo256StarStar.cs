using System.Runtime.CompilerServices;

namespace AngryWasp.Random
{
    public class XoShiRo256StarStar : XoShiRo256
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ulong Next()
        {
            ulong result = Rotl(state[1] * 5, 7) * 9;
            Scramble();
            return result;
        }
    }
}