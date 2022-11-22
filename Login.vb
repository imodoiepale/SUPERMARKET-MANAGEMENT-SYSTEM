Public Class Login
    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Application.Exit()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If UsernameTb.Text = "" Or PasswordTb.Text = "" Then
            MsgBox("Input Username and Password")
        ElseIf UsernameTb.Text = "Admin" And PasswordTb.Text = "SMSystem" Then
            Dim Obj = New Items
            Obj.Show()
            Me.Hide()
        Else
            MsgBox(" You have entered the Wrong Username or Password")
            UsernameTb.Text = ""
            PasswordTb.Text = ""

        End If
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Dim Obj = New Billing
        Obj.Show()
        Me.Hide()
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class