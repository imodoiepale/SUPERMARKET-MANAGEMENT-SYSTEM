Imports System.Data.SqlClient

Public Class Items

    Dim Con = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\Documents\Supermarket.mdf;Integrated Security=True;Connect Timeout=30")

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ItNameTb.Text = "" Or QtyTb.Text = "" Or CateCb.SelectedIndex = -1 Or PriceTb.Text = "" Then
            MsgBox("Missing Information")
        Else
            Try
                Con.Open()
                Dim query = "insert into itemTbl values('" & ItNameTb.Text & "', " & QtyTb.Text & ", " & PriceTb.Text & ", '" & CateCb.SelectedItem.ToString() & "')"
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("Item Saved Successfully")
                Con.Close()
                DisplayItem()
                Clear()

            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub Clear()
        ItNameTb.Text = ""
        QtyTb.Text = ""
        PriceTb.Text = ""
        CateCb.SelectedIndex = 0

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Clear()
    End Sub

    Private Sub DisplayItem()

        Con.Open()
        Dim query = "select * from ItemTbl"
        Dim cmd = New SqlCommand(query, Con)
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(cmd)
        Dim builder As New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        ItemDGV.DataSource = ds.Tables(0)
        Con.Close()

    End Sub

    Private Sub FilterByCat()

        Con.Open()
        Dim query = "select * from ItemTbl where ItCat = '" & SearchCb.SelectedItem.ToString() & "'"
        Dim cmd = New SqlCommand(query, Con)
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(cmd)
        Dim builder As New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        ItemDGV.DataSource = ds.Tables(0)
        Con.Close()

    End Sub
    Dim key = 0

    Private Sub ItemDGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles ItemDGV.CellMouseClick
        Dim row As DataGridViewRow = ItemDGV.Rows(e.RowIndex)
        ItNameTb.Text = row.Cells(1).Value.ToString
        QtyTb.Text = row.Cells(2).Value.ToString
        PriceTb.Text = row.Cells(3).Value.ToString
        CateCb.SelectedItem = row.Cells(4).Value.ToString

        If ItNameTb.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If key = 0 Then
            MsgBox("Select Item to delete")
        Else
            Try
                Con.Open()
                Dim query = "delete from ItemTbl where ItId= " & key & ""
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("Item Deleted Successfully")
                Con.Close()
                DisplayItem()
                Clear()

            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ItNameTb.Text = "" Or QtyTb.Text = "" Or SearchCb.SelectedIndex = -1 Or PriceTb.Text = "" Then
            MsgBox("Missing Information")
        Else
            Try
                Con.Open()
                Dim query = "Update ItemTbl set ItName '" & ItNameTb.Text & "' , ItQty " & QtyTb.Text & ", ItPrice " & PriceTb.Text & "ItCat '" & CateCb.SelectedItem.ToString() & "' where ItId " & key & ""
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("Item Updated Successfully")
                Con.Close()
                DisplayItem()
                Clear()

            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub ItemDGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles ItemDGV.CellContentClick
        DisplayItem()
    End Sub

    Private Sub Items_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DisplayItem()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim Obj = New Login
        Obj.Show()
        Me.Hide()
    End Sub

    Private Sub SearchCb_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles SearchCb.SelectionChangeCommitted
        FilterByCat()
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        DisplayItem()
    End Sub
End Class