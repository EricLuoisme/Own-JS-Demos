using System;
using System.Text;
using System.Numerics;
using System.Buffers.Binary;

public sealed class SecurityUtil
{
    public static string UuidToBinarySum(string uuid)
    {
        try
        {
            // 去掉 UUID 中的连字符
            string cleanedUuid = uuid.Replace( "-", "" );
            // 验证 UUID 是否有效
            if( cleanedUuid.Length != 32 )
            {
                throw new ArgumentException( "Invalid UUID format" );
            }

            // 将 UUID 转换为字节数组
            if( cleanedUuid.Length % 2 != 0 )
            {
                throw new ArgumentException( "Invalid cleanedUuid string" );
            }
            byte[] buffer = GetBufferFromHexString( cleanedUuid );

            // 确保字节数组的长度足够
            if( buffer.Length < 16 )
            {
                throw new ArgumentException( "Buffer length is insufficient" );
            }
            // Read the most and least significant bits
            ulong msb = BinaryPrimitives.ReadUInt64BigEndian(buffer);
            ulong lsb = BinaryPrimitives.ReadUInt64BigEndian(buffer.AsSpan(8));

            // 读取最不显著的 64 位无符号整数
            string mostSignificantBitsBin = BigIntegerToBinaryString( mostSignificantBits );
            string leastSignificantBitsBin = BigIntegerToBinaryString( leastSignificantBits );

            // 拼接二进制字符串
            return mostSignificantBitsBin + leastSignificantBitsBin;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine( "Error in UuidToBinarySum方法报错: " + ex.Message );
            return string.Empty; // 或者选择其他适当的返回值
        }
    }

    public static string ParseLongXOR(string a, string b)
    {
        try
        {
            // 验证输入是否为有效的二进制字符串
            if( !IsBinaryString( a ) || !IsBinaryString( b ) )
            {
                throw new ArgumentException( "Invalid binary string" );
            }

            // 确保两个字符串长度相同
            if( a.Length != b.Length )
            {
                throw new ArgumentException( "Binary strings must have the same length" );
            }

            ulong msb = BinaryPrimitives.ReadUInt64BigEndian(buffer);
            ulong lsb = BinaryPrimitives.ReadUInt64BigEndian(buffer.AsSpan(8));

            string mostSignificantBitsBin = ULongToBinaryString(msb);
            string leastSignificantBitsBin = ULongToBinaryString(lsb);

            // 返回二进制字符串
            return mostSignificantBitsBin + leastSignificantBitsBin;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error in ParseLongXOR: " + ex.Message);
            return string.Empty; // 或者选择其他适当的返回值
        }
    }

    private static string ULongToBinaryString(ulong value)
    {
        char[] bits = new char[64];
        for (int i = 63; i >= 0; i--)
        {
            bits[63 - i] = ((value & (1UL << i)) != 0) ? '1' : '0';
        }
        return new string(bits);
    }

    /// <summary>
    /// BigInteger转换为二进制字符串
    /// </summary>
    /// <param name="bigInt"></param>
    /// <returns></returns>
    public static string BigIntegerToBinaryString(BigInteger bigInt)
    {
        if (bigInt == 0)
            return "0";

        StringBuilder binaryStringBuilder = new();
        BigInteger temp = bigInt;

        while (temp > 0)
        {
            int bit = (int)(temp & 1);
            binaryStringBuilder.Insert(0, bit);
            temp >>= 1;
        }

        return binaryStringBuilder.ToString();
    }
    
    /// <summary>
    /// 字符串转换为十六进制字节数组
    /// </summary>
    /// <param name="hexString"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private static byte[] GetBufferFromHexString(string hexString)
    {
        if (hexString.Length % 2 != 0)
            throw new ArgumentException("Hex string must have even length.");

        byte[] buffer = new byte[hexString.Length / 2];
        for (int i = 0; i < buffer.Length; i++)
        {
            buffer[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
        }
        return buffer;
    }

    /// <summary>
    /// 字符串转换为十六进制字节数组
    /// </summary>
    /// <param name="hexString"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private static BigInteger BinaryStringToBigInteger(string b)
    {
        BigInteger decimalValue = 0;
        foreach (char c in b)
        {
            decimalValue <<= 1;
            decimalValue += c == '1' ? 1 : 0;
        }
        return decimalValue;
    }

    /// <summary>
    /// 辅助方法：验证字符串是否为有效的二进制字符串
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private static bool IsBinaryString(string str)
    {
        foreach (char c in str)
        {
            if (c != '0' && c != '1')
                return false;
        }
        return true;
    }
}
