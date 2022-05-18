Public MustInherit Class aade_afm

    Private userName As String
    Public Property U_UserName() As String
        Get
            Return userName
        End Get
        Set(ByVal value As String)
            userName = value
        End Set
    End Property


    Private password As String

    Public Property U_Password() As String
        Get
            Return password
        End Get
        Set(ByVal value As String)
            password = value
        End Set
    End Property

    Private afmCalledBy As String
    Public Property U_AfmCalledBy() As String
        Get
            Return afmCalledBy
        End Get
        Set(ByVal value As String)
            afmCalledBy = value
        End Set
    End Property


    Private afmCalledFor As String
    Public Property U_AfmCalledFor() As String
        Get
            Return afmCalledFor
        End Get
        Set(ByVal value As String)
            afmCalledFor = value
        End Set
    End Property


    Private asOnDate As String

    Public Property U_AsOnDate() As String
        Get
            Return asOnDate
        End Get
        Set(ByVal value As String)
            asOnDate = value
        End Set
    End Property


    Public Sub New()

    End Sub

    Public Sub New(ByVal un As String, ByVal pwd As String, ByVal calledBy As String, ByVal calledFor As String, ByVal onDate As String)

        Me.userName = un
        Me.password = pwd
        Me.afmCalledBy = calledBy
        Me.afmCalledFor = calledFor
        Me.asOnDate = onDate

    End Sub



    Public MustOverride Async Function GetVersion() As Task(Of Boolean)
    Public MustOverride Async Function GetIdentityInformation() As Task(Of Boolean)

End Class
