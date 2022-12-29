using System.Runtime.CompilerServices;

namespace AngryWasp.Random
{
    public class XoShiRo256PlusPlus : XoShiRo256
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ulong Next()
        {
            ulong result = Rotl(state[0] + state[3], 23) + state[0];
            Scramble();
            return result;
        }
    }
}