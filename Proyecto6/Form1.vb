Imports MySql.Data.MySqlClient
'Código de Manuel Diu
Public Class Form1
    Dim str As String = "server=localhost; Uid=root; pwd=; database=veterinaria;"
    Dim conexion As New MySqlConnection(str)

    Sub cargar()
        conexion.Open()
        Dim consulta As String = "SELECT * FROM perros"
        Dim adaptador As New MySqlDataAdapter(consulta, conexion)
        Dim ds As New DataSet()
        adaptador.Fill(ds, "Grid")
        grdVeterinaria.DataSource = ds.Tables(0)
        conexion.Close()

        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
    End Sub

    Private Sub Form1_cargar(sender As Object, e As EventArgs) Handles MyBase.Load
        cargar()
    End Sub

    Private Sub grdVeterinaria_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdVeterinaria.CellContentClick
        Dim fila As DataGridViewRow = grdVeterinaria.CurrentRow
        Try
            TextBox1.Text = fila.Cells(0).Value.ToString()
            TextBox2.Text = fila.Cells(1).Value.ToString()
            TextBox3.Text = fila.Cells(2).Value.ToString()
            TextBox4.Text = fila.Cells(3).Value.ToString()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdVeterinaria_SelectionChanged(sender As Object, e As EventArgs) Handles grdVeterinaria.SelectionChanged
        If (grdVeterinaria.SelectedRows.Count > 0) Then
            TextBox1.Text = grdVeterinaria.Item("nombre", grdVeterinaria.SelectedRows(0).Index).Value
            TextBox2.Text = grdVeterinaria.Item("raza", grdVeterinaria.SelectedRows(0).Index).Value
            TextBox3.Text = grdVeterinaria.Item("color", grdVeterinaria.SelectedRows(0).Index).Value
            TextBox4.Text = grdVeterinaria.Item("id", grdVeterinaria.SelectedRows(0).Index).Value
        End If

    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Dim cmd As MySqlCommand
        conexion.Open()
        Try
            cmd = conexion.CreateCommand
            cmd.CommandText = "INSERT INTO perros(id, nombre, raza, color) VALUES('', @nombre, @raza, @color);"
            cmd.Parameters.AddWithValue("@nombre", TextBox1.Text)
            cmd.Parameters.AddWithValue("@raza", TextBox2.Text)
            cmd.Parameters.AddWithValue("@color", TextBox3.Text)
            cmd.ExecuteNonQuery()
            conexion.Close()
            cargar()
        Catch ex As Exception

        End Try



    End Sub

    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        Dim cmd As MySqlCommand
        conexion.Open()
        Try
            cmd = conexion.CreateCommand()
            cmd.CommandText = "UPDATE perros SET nombre=@nombre, raza=@raza, color=@color WHERE id=@id"
            cmd.Parameters.AddWithValue("@nombre", TextBox1.Text)
            cmd.Parameters.AddWithValue("@raza", TextBox2.Text)
            cmd.Parameters.AddWithValue("@color", TextBox3.Text)
            cmd.Parameters.AddWithValue("@id", TextBox4.Text)
            cmd.ExecuteNonQuery()
            conexion.Close()
            cargar()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click

        Dim cmd As MySqlCommand
        Dim resultado As DialogResult = MessageBox.Show("¿Está seguro de que desea eliminar los datos seleccionados?",
                              "Confirmación", MessageBoxButtons.OKCancel)

        If (resultado = DialogResult.OK) Then
            conexion.Open()
            cmd = conexion.CreateCommand()
            cmd.CommandText = "DELETE FROM perros WHERE id=@id"
            cmd.Parameters.AddWithValue("@id", TextBox4.Text)
            cmd.ExecuteNonQuery()
            conexion.Close()
            cargar()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
    End Sub
End Class