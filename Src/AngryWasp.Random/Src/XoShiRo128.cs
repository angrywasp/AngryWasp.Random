using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace AngryWasp.Random
{
    public abstract class XoShiRo128
    {
        protected uint[] state = null;

        public XoShiRo128()
        {
            var r = new byte[16];
            RandomNumberGenerator.Create().GetNonZeroBytes(r);
            SetState(r);
        }

        public void Reseed(uint seed) => Helper.KeccakHash(UIntToByte(seed), 128);

        private void SetState(byte[] r)
        {
            var start = 0;

            state = new uint[] {
                BytesToUInt(r, ref start),
                BytesToUInt(r, ref start),
                BytesToUInt(r, ref start),
                BytesToUInt(r, ref start)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected uint Rotl(uint x, int k) => (x << k) | (x >> (32 - k));

        public abstract uint Next();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Scramble()
        {
            uint t = state[1] << 9;

            state[2] ^= state[0];
            state[3] ^= state[1];
            state[1] ^= state[2];
            state[0] ^= state[3];

            state[2] ^= t;
            state[3] = Rotl(state[3], 11);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Next(int minValue, int maxValue) => (int)(Next() / ((float)uint.MaxValue / (maxValue - minValue)) + minValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float NextFloat() => (float)Next() / uint.MaxValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float NextFloat(float min, float max) => (float)((max - min) * NextFloat() + min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte NextByte() => (byte)Next(0, 255);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NextBytes(byte[] buffer)
        {
            for (int idx = 0; idx < buffer.Length; idx++)
                buffer[idx] = (byte)(Next() / (uint.MaxValue / byte.MaxValue));
        }

        private uint BytesToUInt(byte[] value, ref int start) =>
            (uint)(value[start++] | value[start++] << 8 | value[start++] << 16 | value[start++] << 24);

        private static byte[] UIntToByte(uint value) => 
            new byte[] {
                (byte)value,
                (byte)(value >> 8),
                (byte)(value >> 16),
                (byte)(value >> 24)
            };
    }
}