using System.Runtime.CompilerServices;

namespace AngryWasp.Random
{
    public class XoShiRo128StarStar : XoShiRo128
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override uint Next()
        {
            uint result = Rotl(state[1] * 5, 7) * 9;
            Scramble();
            return result;
        }
    }
}