using System;

namespace Rita.System
{
    public class StringUtil
    {
        public static string GenerateID()
        {
            return BitConverter.ToString(Guid.NewGuid().ToByteArray()).Replace("-", string.Empty);
        }
    }
}
