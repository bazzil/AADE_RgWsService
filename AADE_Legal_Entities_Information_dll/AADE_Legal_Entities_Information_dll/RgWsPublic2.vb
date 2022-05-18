Imports System.Net.Http
Imports System.Web
Imports System.Text
Imports System.Xml


Public Class RgWsPublic2
    Inherits aade_afm


    Public Property soapStr As String
    Public Property client = New HttpClient()
    Public Property queryString = HttpUtility.ParseQueryString(String.Empty)

    Public Property xmlDoc As New XmlDocument()
    Public Property nsManager As XmlNamespaceManager

    Public Property node As XmlNode
    Public Property ele As XmlElement

    Public Property lst As XmlNodeList

    Public Property response As HttpResponseMessage

    Private Property xmlConf As New XmlConfig()

    Public Property responseHeader As String
    Public Property xmlResponce As String

    Public Property result As String

    Public Property basicRecordData As New BasicRec

    Public Property firmActTab As New List(Of FirmActTab)


    Public Sub New(ByVal un As String, ByVal pwd As String, ByVal calledBy As String, ByVal calledFor As String, ByVal onDate As String)


        MyBase.New(un, pwd, calledBy, calledFor, onDate)


    End Sub

    Public Overrides Async Function GetVersion() As Task(Of Boolean)
        'Throw New NotImplementedException()

        Dim byteData As Byte() = Encoding.UTF8.GetBytes(Me.soapStr)


        Using content = New ByteArrayContent(byteData)

            Me.response = Await client.PostAsync(Me.xmlConf.uri, content)
            'MsgBox(content.ToString)

            Me.responseHeader = Me.response.ToString
            Me.xmlResponce = Await Me.response.Content.ReadAsStringAsync()

        End Using


        Me.xmlDoc.LoadXml(Me.xmlResponce)

        Me.nsManager = New XmlNamespaceManager(Me.xmlDoc.NameTable)

        Me.nsManager.AddNamespace("env", Me.xmlConf._env_xsdNameSpace)
        Me.nsManager.AddNamespace("srvc", Me.xmlConf._srvc_xmlnsNameSpace)


        Me.lst = xmlDoc.SelectNodes(Me.xmlConf._versionPath, Me.nsManager)


        Me.node = Me.lst(0)
        Me.ele = CType(node, XmlElement)

        Me.result = Me.ele.InnerText

        Return True
    End Function

    Public Overrides Async Function GetIdentityInformation() As Task(Of Boolean)
        'Throw New NotImplementedException()



        Me.soapStr = Replace(soapStr, "<!--{USERNAME}-->", Me.U_UserName)
        Me.soapStr = Replace(soapStr, "<!--{PASSWORD}-->", Me.U_Password)
        Me.soapStr = Replace(soapStr, "<!--{CALLED_BY}-->", Me.U_AfmCalledBy)
        Me.soapStr = Replace(soapStr, "<!--{CALLED_FOR}-->", Me.U_AfmCalledFor)
        Me.soapStr = Replace(soapStr, "<!--{DATE}-->", Me.U_AsOnDate)



        Dim byteData As Byte() = Encoding.UTF8.GetBytes(Me.soapStr)

        Using content = New ByteArrayContent(byteData)

            Me.response = Await client.PostAsync(Me.xmlConf.uri, content)

            Me.responseHeader = Me.response.ToString
            Me.xmlResponce = Await Me.response.Content.ReadAsStringAsync()

        End Using


        Me.xmlDoc.LoadXml(Me.xmlResponce)
        Me.nsManager = New XmlNamespaceManager(Me.xmlDoc.NameTable)

        Me.nsManager.AddNamespace("env", Me.xmlConf._env_xsdNameSpace)
        Me.nsManager.AddNamespace("srvc", Me.xmlConf._srvc_xmlnsNameSpace)
        Me.nsManager.AddNamespace("res", Me.xmlConf._res_xmlnsNameSpace)


        Me.result = ""

        For Each x As String In Me.xmlConf.xmlTagsBasicRec

            Me.lst = xmlDoc.SelectNodes(Me.xmlConf._basicRegPath & Me.xmlConf.tgPfx & x, Me.nsManager)

            Me.node = Me.lst(0)
            Me.ele = CType(Me.node, XmlElement)


            Me.result &= Me.ele.InnerText & vbCrLf

            SetBaseRecordData(x, Me.ele.InnerText)
        Next

        Me.result &= vbCrLf & "--------------------------------------------------------------" & vbCrLf & vbCrLf



        Me.lst = Me.xmlDoc.SelectNodes(Me.xmlConf._firmPath, nsManager)

        If lst.Count > 0 Then
            For i As Integer = 0 To lst.Count - 1
                For Each x As String In xmlConf.xmlTagsFirmAct

                    Me.lst = Me.xmlDoc.SelectNodes(Me.xmlConf._firmPath & "/" & xmlConf.tgPfx & x, nsManager)

                    Me.node = Me.lst(i)
                    Me.ele = CType(Me.node, XmlElement)


                    Me.result &= Me.ele.InnerText & vbCrLf

                    firmActTab.Add(SetFirmActTabData(x, Me.ele.InnerText))
                Next

                Me.result &= vbCrLf & "--------------------------------------------------------------" & vbCrLf & vbCrLf

            Next
        End If



        Return True

    End Function


    Private Function SetFirmActTabData(ByVal fieldName As String, ByVal v As String) As FirmActTab

        Dim fatObj As New FirmActTab()

        Select Case fieldName
            Case "firm_act_code"
                fatObj.firm_act_code = v

            Case "firm_act_descr"
                fatObj.firm_act_descr = v

            Case "firm_act_kind"
                fatObj.firm_act_kind = v

            Case "firm_act_kind_descr"
                fatObj.firm_act_kind_descr = v
        End Select

        Return fatObj

    End Function

    Private Sub SetBaseRecordData(ByVal fieldName As String, ByVal v As String)

        Select Case fieldName
            Case "afm"
                Me.basicRecordData.afm = v

            Case "doy"
                Me.basicRecordData.doy = v

            Case "doy_descr"
                Me.basicRecordData.doy_descr = v

            Case "i_ni_flag_descr"
                Me.basicRecordData.i_ni_flag_descr = v

            Case "deactivation_flag"
                Me.basicRecordData.deactivation_flag = v

            Case "deactivation_flag_descr"
                Me.basicRecordData.deactivation_flag_descr = v

            Case "firm_flag_descr"
                Me.basicRecordData.firm_flag_descr = v

            Case "onomasia"
                Me.basicRecordData.onomasia = v

            Case "commer_title"
                Me.basicRecordData.commer_title = v

            Case "legal_status_descr"
                Me.basicRecordData.legal_status_descr = v

            Case "postal_address"
                Me.basicRecordData.postal_address = v

            Case "postal_address_no"
                Me.basicRecordData.postal_address_no = v

            Case "postal_zip_code"
                Me.basicRecordData.postal_zip_code = v

            Case "postal_area_description"
                Me.basicRecordData.postal_area_description = v

            Case "regist_date"
                Me.basicRecordData.regist_date = v

            Case "stop_date"
                Me.basicRecordData.stop_date = v

            Case "normal_vat_system_flag"
                Me.basicRecordData.normal_vat_system_flag = v
        End Select

    End Sub


    Public Overrides Function ToString() As String

        Me.GetIdentityInformation()

        Return Me.result

    End Function
End Class
