
Imports System.Net.Http
Imports System.Web
Imports System.Text
Imports System.Xml
Imports AADE_Legal_Entities_Information_dll


Public Class Form1
    Private Async Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        'GetVersionInfo()

        GetVersionInfoEx()


    End Sub

    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'GetIdentityData()

        GetIdentityDataEX()

    End Sub




    Private Async Sub GetVersionInfo()

        Dim soapStr As String = My.Resources.request_version
        Dim conf As New AppConfig()
        Dim xmlConf As New XmlConfig()


        Dim client = New HttpClient()
        Dim queryString = HttpUtility.ParseQueryString(String.Empty)

        Dim responseHeader As String
        Dim xmlResponce As String

        Dim xmlDoc As New XmlDocument()
        Dim nsManager As XmlNamespaceManager

        Dim response As HttpResponseMessage

        Dim byteData As Byte() = Encoding.UTF8.GetBytes(soapStr)



        Using content = New ByteArrayContent(byteData)

            response = Await client.PostAsync(conf.uri, content)
            'MsgBox(content.ToString)
            responseHeader = response.ToString
            xmlResponce = Await response.Content.ReadAsStringAsync()

        End Using


        'MsgBox(responseHeader)

        'MsgBox(xmlResponce)

        xmlDoc.LoadXml(xmlResponce)
        nsManager = New XmlNamespaceManager(xmlDoc.NameTable)

        nsManager.AddNamespace("env", xmlConf._env_xsdNameSpace)
        nsManager.AddNamespace("srvc", xmlConf._srvc_xmlnsNameSpace)
        'nsManager.AddNamespace("res", xmlConf._res_xmlnsNameSpace)

        Dim lst As XmlNodeList = xmlDoc.SelectNodes("//env:Body/srvc:rgWsPublic2VersionInfoResponse/srvc:result", nsManager)

        Dim node As XmlNode = lst(0)
        Dim ele As XmlElement = CType(node, XmlElement)
        'MsgBox(ele.InnerXml)
        MsgBox(ele.InnerText,, "ΑΑΔΕ - Πληροφορίες έκδοσης")

    End Sub


    Private Async Sub GetVersionInfoEx()

        Dim rgWsService As New RgWsPublic2(Me.TextBox1.Text, Me.TextBox2.Text, Me.TextBox5.Text, Me.TextBox3.Text, Me.TextBox4.Text)

        rgWsService.soapStr = My.Resources.request_version


        Await rgWsService.GetVersion()


        MsgBox(rgWsService.result, , "ΑΑΔΕ - Πληροφορίες έκδοσης")

    End Sub


    Private Async Sub GetIdentityDataEX()

        Dim rgWsService As New RgWsPublic2(Me.TextBox1.Text, Me.TextBox2.Text, Me.TextBox5.Text, Me.TextBox3.Text, Me.TextBox4.Text)

        rgWsService.soapStr = My.Resources.request_afm


        Await rgWsService.GetIdentityInformation()


        Me.RichTextBox1.Text = rgWsService.result


        MsgBox(rgWsService.basicRecordData.afm)
        MsgBox(rgWsService.basicRecordData.doy_descr)


    End Sub

    Private Async Sub GetIdentityData()

        Dim soapStr As String = My.Resources.request_afm
        Dim response As HttpResponseMessage
        Dim client = New HttpClient()
        Dim conf As New AppConfig()
        Dim xmlConf As New XmlConfig()
        Dim responseHeader As String
        Dim xmlResponce As String

        Dim xmlDoc As New XmlDocument()
        Dim nsManager As XmlNamespaceManager

        Dim lst As XmlNodeList
        Dim entityData As String = ""

        Dim node As XmlNode
        Dim ele As XmlElement


        soapStr = Replace(soapStr, "<!--{USERNAME}-->", Me.TextBox1.Text)
        soapStr = Replace(soapStr, "<!--{PASSWORD}-->", Me.TextBox2.Text)
        soapStr = Replace(soapStr, "<!--{CALLED_BY}-->", Me.TextBox5.Text)
        soapStr = Replace(soapStr, "<!--{CALLED_FOR}-->", Me.TextBox3.Text)
        soapStr = Replace(soapStr, "<!--{DATE}-->", Me.TextBox4.Text)


        Dim byteData As Byte() = Encoding.UTF8.GetBytes(soapStr)

        Using content = New ByteArrayContent(byteData)

            response = Await client.PostAsync(conf.uri, content)
            'MsgBox(content.ToString)
            responseHeader = response.ToString
            xmlResponce = Await response.Content.ReadAsStringAsync()

        End Using

        'Dim tgPfx As String = "res:"
        'Dim xmlTagsBasicRec = New String() {"afm", "doy", "doy_descr", "i_ni_flag_descr", "deactivation_flag", "deactivation_flag_descr", "firm_flag_descr", "onomasia", "legal_status_descr", "postal_address", "postal_address_no", "postal_zip_code", "postal_area_description", "regist_date", "normal_vat_system_flag"}
        'Dim xmlTagsFirmAct = New String() {"firm_act_code", "firm_act_descr", "firm_act_kind", "firm_act_kind_descr"}


        'MsgBox(xmlResponce)
        xmlDoc.LoadXml(xmlResponce)
        nsManager = New XmlNamespaceManager(xmlDoc.NameTable)

        nsManager.AddNamespace("env", xmlConf._env_xsdNameSpace)
        nsManager.AddNamespace("srvc", xmlConf._srvc_xmlnsNameSpace)
        nsManager.AddNamespace("res", xmlConf._res_xmlnsNameSpace)





        For Each x As String In xmlConf.xmlTagsBasicRec

            lst = xmlDoc.SelectNodes(xmlConf._basicRegPath & xmlConf.tgPfx & x, nsManager)

            node = lst(0)
            ele = CType(node, XmlElement)
            'MsgBox(ele.InnerXml)
            'MsgBox(ele.InnerText)


            entityData &= ele.InnerText & vbCrLf
        Next

        entityData &= vbCrLf & "--------------------------------------------------------------" & vbCrLf & vbCrLf

        lst = xmlDoc.SelectNodes("//env:Body/srvc:rgWsPublic2AfmMethodResponse/srvc:result/res:rg_ws_public2_result_rtType/res:firm_act_tab/res:item", nsManager)

        If lst.Count > 0 Then
            For i As Integer = 0 To lst.Count - 1
                For Each x As String In xmlConf.xmlTagsFirmAct

                    lst = xmlDoc.SelectNodes("//env:Body/srvc:rgWsPublic2AfmMethodResponse/srvc:result/res:rg_ws_public2_result_rtType/res:firm_act_tab/res:item/" & xmlConf.tgPfx & x, nsManager)

                    node = lst(i)
                    ele = CType(node, XmlElement)
                    'MsgBox(ele.InnerXml)
                    'MsgBox(ele.InnerText)


                    entityData &= ele.InnerText & vbCrLf
                Next

                entityData &= vbCrLf & "--------------------------------------------------------------" & vbCrLf & vbCrLf

            Next
        End If



        Me.RichTextBox1.Text = entityData

    End Sub
End Class
