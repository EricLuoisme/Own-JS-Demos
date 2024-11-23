using System;
using System.Text;
using System.Numerics;
using System.Buffers.Binary;

public sealed class SecurityUtil
{
    public static string UuidToBinarySum( string uuid )
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

            BigInteger mostSignificantBits = new ( BinaryPrimitives.ReadUInt64BigEndian( buffer ) );
            BigInteger leastSignificantBits = new ( BinaryPrimitives.ReadUInt64BigEndian( buffer.AsSpan( 8 ) ));

            // 读取最不显著的 64 位无符号整数
            string mostSignificantBitsBin = BigIntegerToBinaryString( mostSignificantBits );
            string leastSignificantBitsBin = BigIntegerToBinaryString( leastSignificantBits );

            // 拼接二进制字符串
            return mostSignificantBitsBin + leastSignificantBitsBin;
        }
        catch( Exception ex )
        {
            Console.Error.WriteLine( "Error in UuidToBinarySum方法报错: " + ex.Message );
            return string.Empty; // 或者选择其他适当的返回值
        }
    }

    public static string ParseLongXOR( string a, string b )
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

            // 将二进制字符串转换为BigInteger
            BigInteger bigIntA = BinaryStringToBigInteger( a );
            BigInteger bigIntB = BinaryStringToBigInteger( b );

            // 进行异或操作
            BigInteger result = bigIntA ^ bigIntB;

            // 返回二进制字符串
            return BigIntegerToBinaryString( result ).PadLeft( a.Length, '0' );
        }
        catch( Exception ex )
        {
            Console.Error.WriteLine( "Error in ParseLongXOR: " + ex.Message );
            return string.Empty; // 或者选择其他适当的返回值
        }
    }

    /// <summary>
    /// BigInteger转换为二进制字符串
    /// </summary>
    /// <param name="bigInt"></param>
    /// <returns></returns>
    public static string BigIntegerToBinaryString( BigInteger bigInt )
    {
        if( bigInt == 0 )
            return "0";

        StringBuilder binaryStringBuilder = new ();
        BigInteger temp = bigInt;

        while( temp > 0 )
        {
            // 获取当前位的值（0或1）
            int bit = ( int ) ( temp & 1 );
            binaryStringBuilder.Insert( 0, bit ); // 插入到最前面
            temp >>= 1; // 右移一位
        }

        return binaryStringBuilder.ToString();
    }

    /// <summary>
    /// 字符串转换为十六进制字节数组
    /// </summary>
    /// <param name="hexString"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private static byte[] GetBufferFromHexString( string hexString )
    {
        if( hexString.Length % 2 != 0 )
            throw new ArgumentException( "十六进制字符串的长度必须是偶数。" );

        byte[] buffer = new byte[hexString.Length / 2];
        for( int i = 0; i < buffer.Length; i++ )
        {
            buffer[i] = Convert.ToByte( hexString.Substring( i * 2, 2 ), 16 );
        }
        return buffer;
    }

    /// <summary>
    /// 二进制字符串转换为BigInteger
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    private static BigInteger BinaryStringToBigInteger( string b )
    {
        BigInteger Decimalvalue = 0;
        foreach( char c in b )
        {
            Decimalvalue <<= 1;
            Decimalvalue += c == '1' ? 1 : 0;
        }
        return Decimalvalue;
    }

    /// <summary>
    /// 辅助方法：验证字符串是否为有效的二进制字符串
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private static bool IsBinaryString( string str )
    {
        foreach( char c in str )
        {
            if( c != '0' && c != '1' )
                return false;
        }
        return true;
    }
}