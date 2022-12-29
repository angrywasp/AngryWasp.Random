using System.Runtime.CompilerServices;

namespace AngryWasp.Random
{
    public class XoShiRo128PlusPlus : XoShiRo128
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override uint Next()
        {
            uint result = Rotl(state[0] + state[3], 7) + state[0];
            Scramble();
            return result;
        }
    }
}