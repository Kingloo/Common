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
        match System.Convert.ToDecimal(bytes) with
            | b when b < oneKiB -> String.Format(cc, "{0} bytes", b)
            | b when b < oneMiB -> String.Format(cc, "{0:0.##} KiB", b / oneKiB)
            | b when b < oneGiB -> String.Format(cc, "{0:0.##} MiB", b / oneMiB)
            | b when b < oneTiB -> String.Format(cc, "{0:0.###} GiB", b / oneGiB)
            | b when b < onePiB -> String.Format(cc, "{0:0.###} TiB", b / oneTiB)
            | b when b < oneEiB -> String.Format(cc, "{0:0.###} PiB", b / onePiB)
            | b when b < oneZiB -> String.Format(cc, "{0:0.###} EiB", b / oneEiB)
            | b when b < oneYiB -> String.Format(cc, "{0:0.###} ZiB", b / oneZiB)
            | b -> String.Format(cc, "{0:0.###} YiB", b / oneYiB)

    let humanReadableBytes32 (bytes: int) : string = humanReadableBytes64 (int64(bytes))