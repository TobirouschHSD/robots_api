Imports System.Net
Imports System.Web.Http
Imports WebApplication3.Models

Namespace Controllers
    Public Class HighscoreController
        Inherits ApiController


        ' GET: api/Highscore
        <Route("api/Highscore")>
        Public Function GetValues() As IEnumerable(Of Models.Highscore)
            Dim pp As New HighscorePersinstence
            Dim ml As List(Of Models.Highscore) = pp.getlist() 'returns a list of all Highscores

            Return ml
        End Function

        ' GET: api/Highscore/5
        Public Function GetValues(ByVal id As Integer) As IEnumerable(Of Models.Highscore) 'id stands for the id of the level
            Dim pp As New HighscorePersinstence
            Dim ml As List(Of Models.Highscore) = pp.getlist(id) 'returns the list off all Highscores for level "id"

            Return ml
        End Function


        ' POST: api/Highscore
        '<Route("api/Highscore")>
        'Public Function PostValue(<FromBody()> ByVal value As Highscore) As Highscore
        'Dim pp As New HighscorePersinstence
        'Dim p As Highscore = value
        'If pp.add(p) Then
        'Return p
        'Else
        'Return New Highscore
        'end If

        ' End Function


        ' PUT: api/Highscore/5
        Public Function PutValue(ByVal id As Integer, <FromBody()> ByVal value As Highscore) As Highscore 'id stands for the id of the level | value contains the name and score
            Dim pp As New HighscorePersinstence
            Dim p As Highscore = value 'gets the new highscore from the request body and safes it in a local variable
            If pp.add(id, p) Then 'tries to add the new score to the database and returns the highscore object if successfull
                Return p
            Else
                Return New Highscore
            End If
        End Function

        ' DELETE: api/Highscore/5
        Public Sub DeleteValue(ByVal id As Integer) 'id stands for the id of the level you want to delete

        End Sub
    End Class
End Namespace