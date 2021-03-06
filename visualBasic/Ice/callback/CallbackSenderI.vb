' **********************************************************************
'
' Copyright (c) 2003-2015 ZeroC, Inc. All rights reserved.
'
' **********************************************************************

Imports Demo

Public NotInheritable Class CallbackSenderI
    Inherits CallbackSenderDisp_

    Public Overloads Overrides Sub initiateCallback(ByVal proxy As CallbackReceiverPrx, ByVal current As Ice.Current)
        System.Console.Out.WriteLine("initiating callback")
        Try
            proxy.callback()
        Catch ex As System.Exception
            System.Console.Error.WriteLine(ex)
        End Try
    End Sub

    Public Overloads Overrides Sub shutdown(ByVal current As Ice.Current)
        System.Console.Out.WriteLine("Shutting down...")
        Try
            current.adapter.getCommunicator().shutdown()
        Catch ex As System.Exception
            System.Console.Error.WriteLine(ex)
        End Try
    End Sub

End Class
