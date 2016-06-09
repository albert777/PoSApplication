Imports System.Data.Odbc
Public Class Form3
    Dim cashPaid As Decimal
    Dim change As Decimal
    Dim orderNum As Integer = 1
    Dim storeLocation As Integer
    Dim total As Decimal
    Dim conn As New System.Data.Odbc.OdbcConnection



    Private Sub cbPayment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPayment.SelectedIndexChanged
        If cbPayment.SelectedIndex = 0 Then
            GroupBox1.Visible = False
            GroupBox2.Visible = True
        End If
        If cbPayment.SelectedIndex = 1 Then
            GroupBox1.Visible = True
            GroupBox2.Visible = False
            txtName.Focus()
        End If



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim payment As String = ""

        If cbPayment.SelectedIndex = 0 Then
            cashPaid = Decimal.Parse(txtCash.Text)
            If (cashPaid < Val(Label2.Text)) Then
                MessageBox.Show("Insufficient amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            change = Math.Round(cashPaid - Val(Label2.Text), 2)
            payment = "Cash"

        End If
        If cbPayment.SelectedIndex = 1 Then
            payment = "Credit/Debit"
            If (txtCCNum.Text.Length <> 16) Then
                MessageBox.Show("Invalid CC number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCCNum.Focus()
                Exit Sub
            End If
            If (txtSCode.Text.Length <> 3) Then
                MessageBox.Show("Invalid Security Code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtSCode.Focus()
                Exit Sub
            End If
            change = 0

        End If
        MessageBox.Show("Change due: $" & change, "Change", MessageBoxButtons.OK)
        If (MessageBox.Show("Print Receipt?", "Receipt", MessageBoxButtons.YesNo)) = Windows.Forms.DialogResult.Yes Then

        End If

        Form2.TextBox1.Text = Form2.TextBox1.Text + "Method of payment: " + payment.ToString + vbCrLf
        Form2.TextBox1.Text = Form2.TextBox1.Text + "Change due: $" + change.ToString + vbCrLf & vbCrLf
        Form2.TextBox1.Text = Form2.TextBox1.Text + "          Thanks for shopping with us!"
        txtCash.Clear()

        Me.Close()
        Form2.Close()


        
        Label10.Text = orderNum
        orderNum += 1
        'orderId += 1
        'To upload orders to order database
        storeLocation = Val(Form1.Label2.Text)
        total = Val(Label2.Text)

        conn.ConnectionString = "DRIVER={MySQL ODBC 5.3 UNICODE Driver};" & _
                "Server=dev1.cis220.hfcc.edu;" & _
                "DATABASE=a1carwashdev;" & _
                "USER=desktop;" & _
                "PASSWORD=heisenberg123;"

        'here would be code to insert new order into "orders" table
        Dim insertOrder As String = "insert into orders (customer_id, order_totalprice, order_shipmethod) values (" + storeLocation.ToString() + ", " + total.ToString() + ", 'pickup');"
        Dim comm As New OdbcCommand()
        comm.Connection = conn
        conn.Open()
        comm.CommandText = insertOrder
        comm.ExecuteNonQuery()
        Dim sqlStatementID As String

        'sqlStatementID = "SELECT LAST_INSERT_ID() FROM orders"
        sqlStatementID = "SELECT order_id FROM orders Order by order_id DESC Limit 1;"

        comm.CommandText = sqlStatementID
        Dim orderId = Convert.ToInt32(comm.ExecuteScalar)
        'MessageBox.Show(orderId.ToString)
        'here would be code to retrieve the data from that insert statement to figure out what the order id is


        For i As Integer = 0 To Val(Form1.Label3.Text) - 1

            'here would be code to insert new order_item into orderitem table
            Dim insertOrderItem As String = "insert into orderitem (order_id, product_id, orderitem_priceperunit, orderitem_quantityordered) values (" + orderId.ToString() +
                ", " + Products(i).ProductID.ToString() + ", " + Products(i).ProductPrice.ToString() + ", " + Products(i).ProductQty.ToString() + ");"
            comm.CommandText = insertOrderItem
            comm.ExecuteNonQuery()

        Next
        conn.Close()

        For x As Integer = 0 To 20
            Products(x) = Nothing
        Next


    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles Me.Load
        cbPayment.SelectedIndex = 0
        txtCash.Focus()
    End Sub
End Class