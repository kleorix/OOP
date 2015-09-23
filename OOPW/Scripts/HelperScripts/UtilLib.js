function round(value, decimals) {
    return Number(Math.round(value + 'e' + decimals) + 'e-' + decimals);
}


function PaddingZero(str, max) {
    /// <summary>
    /// Paddings the leading zero.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="max">The maximum.</param>
    /// Example:
    /// PaddingZero("3", 3);    // => "003"
    /// PaddingZero("123", 3);  // => "123"
    /// PaddingZero("1234", 3); // => "1234"
    /// <returns></returns>
    str = str.toString();
    return str.length < max ? PaddingZero("0" + str, max) : str;
}
