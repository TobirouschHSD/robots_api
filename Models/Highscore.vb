Namespace Models
    Public Class Highscore 'a single Highscore object
        Public Property name As String
        Public Property score As Integer

    End Class

    Public Class FullHighscore 'A highscore object with the extra information of the level (used for GET all highscores)
        Inherits Highscore
        Public Property level As Integer

    End Class

End Namespace
