Option Explicit On
Option Strict Off
Option Infer On

Imports System.Data.Odbc
Public Class Form1
    Dim intCount As Integer
    Dim ds As New DataSet
    Dim da As New OdbcDataAdapter
    Dim conn As New System.Data.Odbc.OdbcConnection
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim intStoreLocation As Integer

        conn.ConnectionString = "DRIVER={MySQL ODBC 5.3 UNICODE Driver};" & _
                "Server=dev1.cis220.hfcc.edu;" & _
                "DATABASE=a1carwashdev;" & _
                "USER=desktop;" & _
                "PASSWORD=heisenberg123;"
        Dim sqlStatement As String

        sqlStatement = "SELECT product_id, product_name, product_price FROM products"

        Try
            conn.Open()

        Catch ex As OdbcException
            MessageBox.Show(ex.Message)
            conn.Close()
            Me.Close()


        End Try
        ds = New DataSet()
        da = New OdbcDataAdapter(sqlStatement, conn)
        da.Fill(ds, "Products")
        DataGridView1.DataSource = ds.Tables("Products")


        Do While intStoreLocation < 1417 Or intStoreLocation > 1421
            intStoreLocation = InputBox("Enter Store number:", "Store number", MessageBoxButtons.OK)
            If (intStoreLocation < 1417 Or intStoreLocation > 1421) Then
                MessageBox.Show("Invalid store number", "Error", MessageBoxButtons.OK)
            End If
        Loop

        btnPay.Focus()
        Label2.Text = intStoreLocation




    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim search As String
        search = txtSearch.Text
      
        Dim sqlStatement As String

        sqlStatement = "SELECT product_id, product_name, product_price FROM products WHERE product_name LIKE '%" + search + "%'"

        da = New OdbcDataAdapter(sqlStatement, conn)
        DataGridView1.DataSource = Nothing
        ds.Tables.Clear()

        da.Fill(ds, "Products")
        DataGridView1.DataSource = ds.Tables("Products")
      

    End Sub
    Public Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Dim x As String
        Dim intIndex As Integer
        Dim Row As DataGridViewRow

        If e.RowIndex = -1 Then Exit Sub

        intIndex = e.RowIndex
        Row = DataGridView1.Rows(intIndex)

        If (MessageBox.Show("Add item?", "Add item", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
            x = InputBox("How many would you like to purchase?", "Quantity")
            If Val(x) <> 0 Then
                Products(intCount).ProductID = Row.Cells(0).Value
                Products(intCount).ProductName = Row.Cells(1).Value
                Products(intCount).ProductPrice = Row.Cells(2).Value
                Products(intCount).ProductQty = Val(x)
                intCount += 1
            End If

        End If
        Label3.Text = intCount
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        txtSearch.Text = ""
        da = New OdbcDataAdapter("Select product_id, product_name, product_price FROM products", conn)
        da.Fill(ds, "Products")
        DataGridView1.DataSource = ds.Tables("Products")
    End Sub

    Private Sub btnPay_Click(sender As Object, e As EventArgs) Handles btnPay.Click
        Dim frmForm2 As New Form2

        My.Forms.Form2.ShowDialog()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If (MessageBox.Show("Remove last added item?", "Remove item", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
            Products(intCount - 1).ProductName = String.Empty
            Products(intCount - 1).ProductPrice = 0.0
            Products(intCount - 1).ProductID = 0
            Products(intCount - 1).ProductQty = 0
            intCount = intCount - 1

            Label3.Text = intCount

        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button1_Click_2(sender As Object, e As EventArgs) Handles btnOverride.Click
        Dim newPrice As Double
        If (MessageBox.Show("Over ride last entered item's price?", "Price Override", MessageBoxButtons.YesNo)) = Windows.Forms.DialogResult.Yes Then
            newPrice = InputBox("Enter new price", "Override Price")
            Products(intCount - 1).ProductPrice = newPrice

        End If


    End Sub

    
End Class
