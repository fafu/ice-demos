' **********************************************************************
'
' Copyright (c) 2003-2015 ZeroC, Inc. All rights reserved.
'
' **********************************************************************

Imports Demo

Public Class PrinterI
    Inherits Printer

    Public Overloads Overrides Sub printBackwards(ByVal current As Ice.Current)
        Dim arr() As Char = message.ToCharArray()
        For i As Integer = 0 To arr.Length / 2 - 1
            Dim tmp As Char = arr(arr.Length - i - 1)
            arr(arr.Length - i - 1) = arr(i)
            arr(i) = tmp
        Next
        System.Console.Out.WriteLine(New String(arr))
    End Sub

End Class
