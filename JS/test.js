function uuidToBinarySum(uuid) {
    const id = uuid;

    try {
        // 去掉 UUID 中的连字符
        const cleanedUuid = id.replace(/-/g, '');

        // 验证 UUID 是否有效
        if (cleanedUuid.length !== 32) {
            throw new Error('Invalid UUID format');
        }

        // 将 UUID 转换为 Buffer
        if (typeof cleanedUuid !== 'string' || cleanedUuid.length % 2 !== 0) {
            throw new Error('Invalid cleanedUuid string');
        }
        const buffer = Buffer.from(cleanedUuid, 'hex');

        // 确保 Buffer 的长度足够
        if (buffer.length < 16) {
            throw new Error('Buffer length is insufficient');
        }

        // 提取最高有效位和最低有效位，并转换为二进制字符串
        console.log(buffer.readBigUInt64BE(0));
        console.log(buffer.readBigUInt64BE(8));

        const mostSignificantBitsBin = buffer.readBigUInt64BE(0).toString(2);
        const leastSignificantBitsBin = buffer.readBigUInt64BE(8).toString(2);

        return mostSignificantBitsBin + leastSignificantBitsBin;
    }
    catch (error) {
        console.error('Error in UuidToBinarySum方法报错:', error.message);
        return null; // 或者选择其他适当的返回值
    }
}

function parseLongXOR(a, b) {
    try {
        // 验证输入是否为有效的二进制字符串
        if (!/^[01]+$/.test(a) || !/^[01]+$/.test(b)) {
            throw new Error('Invalid binary string');
        }

        // 确保两个字符串长度相同
        if (a.length !== b.length) {
            throw new Error('Binary strings must have the same length');
        }
        // 将二进制字符串转换为BigInt
        const bigIntA = BigInt('0b' + a);
        const bigIntB = BigInt('0b' + b);
        // 进行异或操作
        const result = bigIntA ^ bigIntB;
        // 返回二进制字符串
        return result.toString(2).padStart(a.length, '0');
    } catch (error) {
        console.error('Error in parseLongXOR:', error.message);
        return null; // 或者选择其他适当的返回值
    }
}





const uuid = 'f2897ed6-26a7-4a5f-ae88-650e035bf599';
const record = '100011000100,100000000000';
const arr = record.split(',');
const b = uuidToBinarySum(uuid);

if (b !== null) {
    let retval = '';

    for (let i = 0; i < arr.length; i++) {
        const a1 = arr[i];
        const b1 = b.substring(0, a1.length);
        const result = parseLongXOR(a1, b1);
        retval += result;
		retval += ',';
    }

    console.log("result:" + retval);
}