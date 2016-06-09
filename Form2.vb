Public Class Form2
    Dim dgvr As DataGridViewRow
    Public Property SelectedRows As DataGridViewSelectedRowCollection
   


    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim total As Decimal
        Dim intLen As Integer
        Dim tax As Decimal
        Dim orderNum As Integer = 1
        Dim subTotal As Decimal
        orderNum = Val(Form3.Label10.Text)
        btnPay.Focus()
        TextBox1.Text = "                  A1 Carwash" & vbCrLf
        TextBox1.Text = TextBox1.Text & "     You've tried the rest now try the best"
        TextBox1.Text = TextBox1.Text & "                     " & Date.Now() & vbCrLf & vbCrLf

        TextBox1.Text = TextBox1.Text & "Product                          Qty    Price" & vbCrLf
        TextBox1.Text = TextBox1.Text & "-------                          ---    -----" & vbCrLf
        For i As Integer = 0 To 20
            If Products(i).ProductName <> "" Then
                intLen = Products(i).ProductName.Length
                If intLen > 30 Then intLen = 30
                TextBox1.Text = TextBox1.Text & Products(i).ProductName.Substring(0, intLen).PadRight(30) & "    " & Products(i).ProductQty.ToString.PadRight(2) & "  @  " & Products(i).ProductPrice.ToString.PadRight(6) & vbCrLf
                total += Products(i).ProductQty * Products(i).ProductPrice
            End If
            
        Next
        tax = 0.05125
        subTotal = Math.Round((tax * total) + total, 2)
        TextBox1.Text = TextBox1.Text & " " & vbCrLf
        TextBox1.Text = TextBox1.Text & "===================" & vbCrLf
        TextBox1.Text = TextBox1.Text & "SubTotal       $" & total & vbCrLf
        TextBox1.Text = TextBox1.Text & "Tax         $" & tax & vbCrLf
        TextBox1.Text = TextBox1.Text & "Total    $" & subTotal & vbCrLf & vbCrLf

        TextBox2.Text = subTotal
        Form3.Label2.Text = TextBox2.Text

        btnPay.Focus()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        For i As Integer = 0 To 20
            Products(i) = Nothing
        Next
        Me.Close()


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnModify.Click
        Me.Visible = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnPay.Click
        Dim frmForm3 As New Form3

        My.Forms.Form3.ShowDialog()
    End Sub
End Class