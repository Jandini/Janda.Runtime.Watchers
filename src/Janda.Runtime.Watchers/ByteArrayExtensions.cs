namespace Janda.Runtime.Watchers
{
    public static class ByteArrayExtensions
    {

        public static string ToHexBuffer(this byte[] bytes)
        {            
            string result = "var buffer = ";

            if (bytes == null)
                return result + "null;";

            result += "new byte[] {";

            int count = bytes.Length;
            char[] hex = new char[count * 2];

            byte b;

            for (int y = 0, x = 0; y < count; ++y, ++x)
            {
                b = ((byte)(bytes[y] >> 4));

                hex[x] = (char)(b > 9
                    ? b + 0x37
                    : b + 0x30);

                b = ((byte)(bytes[y] & 0xF));

                hex[++x] = (char)(b > 9
                    ? b + 0x37
                    : b + 0x30);
            }

            for (int i = 0; i < hex.Length; i += 2)
            {
                result += "0x" + hex[i] + hex[i + 1];

                if (i < hex.Length - 2)
                    result += ", ";
            }

            return result + "};";
        }
    }
}
