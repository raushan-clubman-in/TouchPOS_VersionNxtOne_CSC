Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Data.SqlClient
Imports System.Web
Imports System.Net
Namespace ModecardVB
    Public Class cardread
        Dim regexp As Regex
        Public boolexp As Boolean = False
        Public boolexp1 As Boolean = False
        Public boolexp2 As Boolean = False
        Public G_dout(15) As Byte
        Public SendBuff(262), RecvBuff(262) As Byte
        Public hContext, hcard As Long
        Public connActive, validATS As Boolean
        Public SendLen, RecvLen, nBytesRet, reqType, Aprotocol As Integer
        Public dwProtocol As Integer
        Public cbPciLength As Integer
        Public dwState, dwActProtocol, Protocol As Long
        Public pioSendRequest, pioRecvRequest As SCARD_IO_REQUEST
        Public rHandle As Integer
        Public retcode As Integer
        Public cardprent As Boolean
        'Dim GPort As New GlobalClass
        'Public Function CloseSmartDevicePort()
        '    Dim RETVALUE, HANDLE As Integer
        '    RETVALUE = ACR120U.ACR120_Close(HANDLE)
        'End Function

        Public Function CloseSmartDevicePort(ByVal greadertype As String) As Boolean
            'Dim RETVALUE, HANDLE As Integer
            'RETVALUE = ACR120U.ACR120_Close(HANDLE)
            Dim RETVALUE, HANDLE As Integer

            If greadertype = "ACR120U" Then
                RETVALUE = ACR120U.ACR120_Close(HANDLE)
            Else
                If connActive Then

                    retcode = ModWinsCard1.SCardDisconnect(hcard, ModWinsCard1.SCARD_UNPOWER_CARD)

                End If

                '' Shared Connection
                'retcode = ModWinsCard.SCardConnect(hContext, greadertype.ToString(), ModWinsCard.SCARD_SHARE_SHARED, ModWinsCard.SCARD_PROTOCOL_T0 Or ModWinsCard.SCARD_PROTOCOL_T1, hCard, Protocol)

                'If retcode <> ModWinsCard.SCARD_S_SUCCESS Then
                '    Exit Function


                'Else

                '    ' Call displayOut(0, 0, "Successful connection to " & cbReader.Text)

                'End If

                connActive = False
            End If
            Return connActive
        End Function
        Public Function GetSMARTDEVICEPORT(greadertype As String)
            Try
                Dim RETVALUE As Integer

                If greadertype = "ACR120U" Then
                    RETVALUE = ACR120U.ACR120_Open(ACR120U.PORTS.ACR120_USB1)
                    'RETVALUE = ACR120U.ACR120_Open(CInt(GBL_SMARTDEVICEPORT))
                    If RETVALUE < 0 Then
                        If cardprent = False Then
                            cardprent = True
                            MsgBox("PROBLEM IN SMART CARD DEVICE CONNECTION", MsgBoxStyle.Critical, "NOT CONNECTED")

                        End If
                    Else
                        cardprent = False
                        '  MsgBox("SMART CARD DEVICE CONNECTION SUCCESSFUL", MsgBoxStyle.Information, "CONNECTED")
                    End If
                Else
                    retcode = ModWinsCard1.SCardEstablishContext(ModWinsCard1.SCARD_SCOPE_USER, 0, 0, hContext)
                    If connActive Then
                        retcode = ModWinsCard1.SCardDisconnect(hcard, ModWinsCard1.SCARD_UNPOWER_CARD)
                    End If
                    ' Shared Connection
                    retcode = ModWinsCard1.SCardConnect(hContext, greadertype.ToString(), ModWinsCard1.SCARD_SHARE_SHARED, ModWinsCard1.SCARD_PROTOCOL_T0 Or ModWinsCard1.SCARD_PROTOCOL_T1, hcard, Protocol)

                    If retcode <> ModWinsCard1.SCARD_S_SUCCESS Then
                        retcode = ModWinsCard1.SCardDisconnect(hcard, ModWinsCard1.SCARD_UNPOWER_CARD)
                        retcode = ModWinsCard1.SCardReleaseContext(hContext)
                        Exit Function
                    Else
                        ' Call displayOut(0, 0, "Successful connection to " & cbReader.Text)
                    End If
                    connActive = True
                End If
                Return connActive
            Catch ex As Exception
                MsgBox(ex.ToString)
                ''MsgBox("PROBLEM IN SMART CARD DEVICE CONNECTION", MsgBoxStyle.Critical, "NOT CONNECTED")
            End Try

        End Function

        Public Function GetSMART_CARDID(ByVal greadertype As String) As String
            Dim GBL_SMARTCARDSNO As String
            If greadertype = "ACR120U" Then
                Call CloseSmartDevicePort("ACR120U")
                Call GetSMARTDEVICEPORT("ACR120U")
                Dim RETVALUE, HANDLE As Integer
                'Variable Declarations
                Dim ResultSN(11) As Byte
                Dim ResultTag As Byte
                Dim ctr As Integer
                Dim TagType(50) As Byte

                RETVALUE = ACR120U.ACR120_Select(HANDLE, TagType(0), ResultTag, ResultSN(0))

                GBL_SMARTCARDSNO = ""
                For ctr = 0 To ResultTag - 1
                    GBL_SMARTCARDSNO = GBL_SMARTCARDSNO + Format_Hex2(ResultSN(ctr))
                Next
            Else
                Dim tmpStr As String
                Dim indx As Integer
                GBL_SMARTCARDSNO = ""
                tmpStr = ""
                validATS = False
                Call ClearBuffers()
                SendBuff(0) = &HFF                              ' CLA
                SendBuff(1) = &HCA                              ' INS

                'If cbIso14443A.Checked Then

                '    SendBuff(2) = &H1                           ' P1 : ISO 14443 A Card

                'Else

                SendBuff(2) = &H0                           ' P1 : Other cards

                'End If

                SendBuff(3) = &H0                               ' P2
                SendBuff(4) = &H0                               ' Le : Full Length

                SendLen = SendBuff(4) + 5
                RecvLen = &HFF

                retcode = SendAPDUandDisplay(3)

                If retcode <> ModWinsCard1.SCARD_S_SUCCESS Then

                    Exit Function

                End If


                ' Interpret and display return values
                If validATS Then

                    'If cbIso14443A.Checked Then

                    '    tmpStr = "UID: "

                    'End If


                    For indx = 0 To (RecvLen - 3)
                        If indx < 0 Then
                            tmpStr = ""
                            Exit For
                        Else
                            tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2)
                        End If
                        ''tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2)
                    Next indx

                    ' displayOut(3, 0, tmpStr.Trim)
                    GBL_SMARTCARDSNO = ""
                    'For ctr = 0 To ResultTag - 1
                    GBL_SMARTCARDSNO = tmpStr
                    'Next
                End If

            End If
            Return GBL_SMARTCARDSNO
        End Function
        Private Sub ClearBuffers()

            Dim indx As Long

            For indx = 0 To 262

                RecvBuff(indx) = &H0
                SendBuff(indx) = &H0

            Next indx

        End Sub
        Private Function SendAPDUandDisplay(ByVal reqType As Integer) As Integer

            Dim indx As Integer
            Dim tmpStr As String

            pioSendRequest.dwProtocol = 2 '2Aprotocol
            pioSendRequest.cbPciLength = Len(pioSendRequest)

            ' Display Apdu In
            tmpStr = ""
            For indx = 0 To SendLen - 1

                tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(SendBuff(indx)), 2) + " "
                'tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(SendBuff(indx)), 2) + ""
            Next indx

            'displayOut(2, 0, tmpStr)
            retcode = ModWinsCard1.SCardTransmit(hcard, pioSendRequest, SendBuff(0), SendLen, pioSendRequest, RecvBuff(0), RecvLen)

            If retcode <> ModWinsCard1.SCARD_S_SUCCESS Then

                'displayOut(1, retCode, "")
                SendAPDUandDisplay = retcode
                Exit Function

            Else
                If indx < 2 Then
                    SendAPDUandDisplay = retcode
                    Exit Function
                End If
                tmpStr = ""
                Select Case reqType


                    Case 0  '  Display SW1/SW2 value
                        For indx = (RecvLen - 2) To (RecvLen - 1)

                            tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "
                            'tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + ""

                        Next indx

                        If Trim(tmpStr) <> "90 00" Then

                            'displayOut(4, 0, "Return bytes are not acceptable.")

                        End If

                    Case 1  ' Display ATR after checking SW1/SW2

                        For indx = (RecvLen - 2) To (RecvLen - 1)

                            tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "
                            'tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + ""

                        Next indx

                        If tmpStr.Trim() <> "90 00" Then

                            tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "
                            'tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + ""

                        Else

                            tmpStr = "ATR : "
                            For indx = 0 To (RecvLen - 3)

                                tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "
                                'tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + ""
                            Next indx

                        End If

                    Case 2  ' Display all data

                        For indx = 0 To (RecvLen - 1)

                            tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "
                            'tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + ""

                        Next indx

                    Case 3  ' Interpret SW1/SW2

                        For indx = (RecvLen - 2) To (RecvLen - 1)
                            If indx < 0 Then
                                tmpStr = ""
                                Exit For
                            Else
                                tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "
                            End If

                            'tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + ""

                        Next indx

                        If tmpStr.Trim = "6A 81" Then

                            'displayOut(4, 0, "The function is not supported.")
                            SendAPDUandDisplay = retcode
                            Exit Select

                        End If

                        If tmpStr.Trim = "63 00" Then

                            'displayOut(4, 0, "The operation failed.")
                            SendAPDUandDisplay = retcode
                            Exit Select

                        End If

                        validATS = True

                End Select

                'displayOut(3, 0, tmpStr.Trim())

            End If

            SendAPDUandDisplay = retcode

        End Function

        Public Function ConvertHexToString(ByVal HexValue As String) As String
            Dim inputString As String = HexValue
            inputString = inputString.Replace(" ", "")
            Dim StrValue As String = ""
            While inputString.Length > 0
                StrValue += System.Convert.ToChar(System.Convert.ToUInt32(inputString.Substring(0, 2), 16)).ToString()
                inputString = inputString.Substring(2, inputString.Length - 2)

            End While
            Return StrValue
        End Function

        Public Function Format_Hex2(ByVal NUM As Integer) As String
            Try
                'Format Byte into two-digit Hex
                Format_Hex2 = Microsoft.VisualBasic.Right("00" & Hex(NUM), 2)
            Catch ex As Exception
                Exit Function
            End Try
        End Function

      

    End Class
End Namespace