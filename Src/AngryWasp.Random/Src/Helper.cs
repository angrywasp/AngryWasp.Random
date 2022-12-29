using Org.BouncyCastle.Crypto.Digests;

namespace AngryWasp.Random
{
    public class Helper
    {
        public static byte[] KeccakHash(byte[] input, int digestSize)
        {
            var digest = new KeccakDigest(digestSize);
            var output = new byte[digest.GetDigestSize()];
            digest.BlockUpdate(input, 0, input.Length);
            digest.DoFinal(output, 0);
            return output;
        }
    }
}