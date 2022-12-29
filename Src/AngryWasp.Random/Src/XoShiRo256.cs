using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace AngryWasp.Random
{
    public abstract class XoShiRo256
    {
        protected ulong[] state = null;

        public XoShiRo256()
        {
            var r = new byte[32];
            RandomNumberGenerator.Create().GetNonZeroBytes(r);
            SetState(r);
        }

        public void Reseed(ulong seed) => SetState(Helper.KeccakHash(ULongToByte(seed), 256));

        private void SetState(byte[] r)
        {
            var start = 0;
            state = new ulong[] {
                BytesToULong(r, ref start),
                BytesToULong(r, ref start),
                BytesToULong(r, ref start),
                BytesToULong(r, ref start)
            };
        }

        protected ulong Rotl(ulong x, int k) => (x << k) | (x >> (64 - k));

        public abstract ulong Next();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void Scramble()
        {
            ulong t = state[1] << 17;

            state[2] ^= state[0];
            state[3] ^= state[1];
            state[1] ^= state[2];
            state[0] ^= state[3];

            state[2] ^= t;
            state[3] = Rotl(state[3], 45);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long Next(long minValue, long maxValue) => (long)(Next() / ((double)ulong.MaxValue / (maxValue - minValue)) + minValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double NextDouble() => (double)Next() / ulong.MaxValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double NextDouble(double min, double max) => (double)((max - min) * NextDouble() + min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte NextByte() => (byte)Next(0, 255);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NextBytes(byte[] buffer)
        {
            for (int idx = 0; idx < buffer.Length; idx++)
                buffer[idx] = (byte)(Next() / (ulong.MaxValue / byte.MaxValue));
        }

        private ulong BytesToULong(byte[] value, ref int start)
        {
            uint num = (uint)(value[start++] | value[start++] << 8 | value[start++] << 16 | value[start++] << 24);
            uint num2 = (uint)(value[start++] | value[start++] << 8 | value[start++] << 16 | value[start++] << 24);
            return (ulong)num2 << 32 | num;
        }

        private static byte[] ULongToByte(ulong value) =>
            new byte[] {
                (byte)value,
                (byte)(value >> 8),
                (byte)(value >> 16),
                (byte)(value >> 24),
                (byte)(value >> 32),
                (byte)(value >> 40),
                (byte)(value >> 48),
                (byte)(value >> 56)};
    }
}