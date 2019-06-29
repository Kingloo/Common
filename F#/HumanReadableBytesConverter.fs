module HumanReadableBytesConverter

    open System
    open System.Globalization

    let oneKiB = 1024m
    let oneMiB = oneKiB * oneKiB
    let oneGiB = oneKiB * oneMiB
    let oneTiB = oneKiB * oneGiB
    let onePiB = oneKiB * oneTiB
    let oneEiB = oneKiB * onePiB
    let oneZiB = oneKiB * oneEiB
    let oneYiB = oneKiB * oneZiB

    let humanReadableBytes64 (bytes: int64) : string =
        let cc = CultureInfo.CurrentCulture
        let decBytes = System.Convert.ToDecimal(bytes)
        if decBytes < oneKiB then sprintf "%i bytes" bytes
        else if decBytes < oneMiB then String.Format(cc, "{0:0.##} KiB", decBytes / oneKiB)
        else if decBytes < oneGiB then String.Format(cc, "{0:0.##} MiB", decBytes / oneMiB)
        else if decBytes < oneTiB then String.Format(cc, "{0:0.###} GiB", decBytes / oneGiB)
        else if decBytes < onePiB then String.Format(cc, "{0:0.###} TiB", decBytes / oneTiB)
        else if decBytes < oneEiB then String.Format(cc, "{0:0.###} PiB", decBytes / onePiB)
        else if decBytes < oneZiB then String.Format(cc, "{0:0.###} EiB", decBytes / oneEiB)
        else if decBytes < oneYiB then String.Format(cc, "{0:0.###} ZiB", decBytes / oneZiB)
        else String.Format(cc, "{0:0.###} YiB", decBytes / oneYiB)

    let humanReadableBytes32 (bytes: int) : string = humanReadableBytes64 (int64(bytes))