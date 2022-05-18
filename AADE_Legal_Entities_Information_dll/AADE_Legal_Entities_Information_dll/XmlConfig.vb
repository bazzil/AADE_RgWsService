Public Class XmlConfig

    Public Property uri As String = "https://www1.gsis.gr/wsaade/RgWsPublic2/RgWsPublic2?WSDL"


    Public Property _env_xsdNameSpace As String = "http://www.w3.org/2003/05/soap-envelope"
    Public Property _srvc_xmlnsNameSpace As String = "http://rgwspublic2/RgWsPublic2Service"
    Public Property _res_xmlnsNameSpace As String = "http://rgwspublic2/RgWsPublic2"


    Public Property tgPfx As String = "res:"
    Public Property xmlTagsBasicRec = New String() {"afm", "doy", "doy_descr", "i_ni_flag_descr", "deactivation_flag", "deactivation_flag_descr", "firm_flag_descr", "onomasia", "legal_status_descr", "postal_address", "postal_address_no", "postal_zip_code", "postal_area_description", "regist_date", "normal_vat_system_flag"}
    Public Property xmlTagsFirmAct = New String() {"firm_act_code", "firm_act_descr", "firm_act_kind", "firm_act_kind_descr"}

    Public Property _basicRegPath As String = "//env:Body/srvc:rgWsPublic2AfmMethodResponse/srvc:result/res:rg_ws_public2_result_rtType/res:basic_rec/"

    Public Property _firmPath As String = "//env:Body/srvc:rgWsPublic2AfmMethodResponse/srvc:result/res:rg_ws_public2_result_rtType/res:firm_act_tab/res:item"
    Public Property _versionPath As String = "//env:Body/srvc:rgWsPublic2VersionInfoResponse/srvc:result"
End Class
