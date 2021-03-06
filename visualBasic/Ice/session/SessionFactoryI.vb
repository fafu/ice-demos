' **********************************************************************
'
' Copyright (c) 2003-2015 ZeroC, Inc. All rights reserved.
'
' **********************************************************************

Imports Demo
Imports System

Public Class SessionFactoryI
    Inherits SessionFactoryDisp_

    Public Sub New(ByVal reapThread As ReapThread)
        _reaper = reapThread
    End Sub

    Public Overloads Overrides Function create(ByVal name As String, ByVal c As Ice.Current) As SessionPrx

        Dim session As SessionI = New SessionI(name)
        Dim proxy As SessionPrx = SessionPrxHelper.uncheckedCast(c.adapter.addWithUUID(session))
        _reaper.add(proxy, session)
        Return proxy
    End Function

    Public Overloads Overrides Sub shutdown(ByVal c As Ice.Current)
        Console.Out.WriteLine("Shutting down...")
        c.adapter.getCommunicator().shutdown()
    End Sub

    Private _reaper As ReapThread
End Class
