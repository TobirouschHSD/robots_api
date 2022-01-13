Imports MySql.Data.MySqlClient

Public Class HighscorePersinstence

    Private mysql As New MySql.Data.MySqlClient.MySqlConnection

    'creates a new instance of the Highscore persistence and initiates the db connection
    Public Sub New()
        Dim DatabaseName As String = "robots"
        Dim server As String = "localhost"
        Dim userName As String = "robot"
        Dim password As String = "robot"

        If Not mysql Is Nothing Then mysql.Close()
        mysql.ConnectionString = String.Format("server={0}; user id={1}; password={2}; database={3}; pooling=false; SslMode=none", server, userName, password, DatabaseName)

        Try
            mysql.Open()

        Catch ex As Exception
            Dim log As New Logging.AspLog
            log.WriteException(ex)
            log.WriteEntry(ex.Message)
        End Try
        mysql.Close()
    End Sub

    'adds a new highscore to a level in the db
    Public Function add(ByVal level As Integer, ByRef p As Models.Highscore) As Boolean
        Dim result As Boolean = False

        Try
            mysql.Open()
        Catch ex As Exception
        End Try

        'first we search if the player already has a score in the db
        Dim q As String = "SELECT COUNT(*) FROM `random` WHERE `name` = '" & p.name & "' AND `level` = " & level
        Dim cmd As MySqlCommand = New MySqlCommand(String.Format(q), mysql)
        Dim count As Int64 = CType(cmd.ExecuteScalar(), Int64)

        If count = 1 Then 'if yes then we update his score
            Dim q1 As String = "UPDATE `random` SET `score` = " & p.score & " WHERE `name` = '" & p.name & "' AND `level` = " & level
            Dim cmd1 As MySqlCommand = New MySqlCommand(String.Format(q1), mysql)
            Dim sqlresult1 As Integer = cmd1.ExecuteNonQuery()

            If sqlresult1 = 1 Then
                result = True
            End If
        Else 'if not then we insert it
            Dim q2 As String = "INSERT INTO `random`(`name`, `score`, `level`) VALUES ('" & p.name & "'," & p.score & "," & level & ")"
            Dim cmd2 As MySqlCommand = New MySqlCommand(String.Format(q2), mysql)
            Dim sqlresult2 As Integer = cmd2.ExecuteNonQuery()

            If sqlresult2 = 1 Then
                result = True
            End If
        End If

        mysql.Close()
        Return result
    End Function

    'get's all highscores and returns them
    Public Function getlist() As List(Of Models.Highscore)
        Dim pl As New List(Of Models.Highscore)
        Try
            mysql.Open()
        Catch ex As Exception
        End Try

        Dim q As String = "Select `name`, `score`, `level` FROM `random` ORDER BY `score` DESC"
        Dim cmd2 As New MySqlCommand(String.Format(q), mysql)
        Dim rd As MySqlDataReader = cmd2.ExecuteReader

        While rd.Read
            Dim p As New Models.FullHighscore
            p.name = rd.GetString(0)
            p.score = rd.GetInt32(1)
            p.level = rd.GetInt32(2)
            pl.Add(p)
        End While
        rd.Close()

        mysql.Close()
        Return pl
    End Function

    'get's all highscores from a specific level and returns them
    Public Function getlist(ByVal level As Integer) As List(Of Models.Highscore)
        Dim pl As New List(Of Models.Highscore)
        Try
            mysql.Open()
        Catch ex As Exception
        End Try

        Dim q As String = "Select `name`, `score`, `level` FROM `random` WHERE level=" & level & " ORDER BY `score` DESC"
        Dim cmd2 As New MySqlCommand(String.Format(q), mysql)
        Dim rd As MySqlDataReader = cmd2.ExecuteReader

        While rd.Read
            Dim p As New Models.Highscore
            p.name = rd.GetString(0)
            p.score = rd.GetInt32(1)
            pl.Add(p)
        End While
        rd.Close()

        mysql.Close()
        Return pl
    End Function

End Class
