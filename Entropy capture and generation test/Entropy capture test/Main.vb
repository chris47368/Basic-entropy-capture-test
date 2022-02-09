Imports System.Text
Imports System.Threading
Public Class Main

    Dim Mouse_cords As String = "00"
    'Dim Ticks(7) As Byte
    Dim Ticks As UInt64 = 0
    Dim entropy_pool(31) As Byte
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim en As New Threading.Thread(AddressOf milliTick)
        en.Priority = Threading.ThreadPriority.Highest
        en.Start()
        Dim ent As New Threading.Thread(AddressOf Entropy_generator)
        ent.Priority = Threading.ThreadPriority.Highest
        ent.Start()
    End Sub

    Private Sub Main_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove

        Mouse_cords = e.X & e.Y

    End Sub


    Sub Entropy_generator()
        While True
            Dim tick_String As String = Ticks.ToString
        Dim entropy_string_byte As Byte() = Encoding.UTF8.GetBytes(Mouse_cords & tick_String & (DateTime.Now - New DateTime(1970, 1, 1)).TotalMilliseconds)
        Dim entropy_bytes(31 + entropy_string_byte.Length) As Byte
        entropy_pool.CopyTo(entropy_bytes, 0)
        entropy_string_byte.CopyTo(entropy_bytes, 32)
        Dim new_hash As New ARX_hash_function
        new_hash.Generate_New_SLASH_HASH(entropy_bytes)
            entropy_pool = new_hash.hash
            Dim output As String = "Main 256 bit entropy pool: " & bytetohex(entropy_pool) & Environment.NewLine & Environment.NewLine & "############# Live Entropy Data ################" & Environment.NewLine & "Internal 64 bit hex millisecond counter: " & bytetohex(BitConverter.GetBytes(Ticks)) & Environment.NewLine & "Mouse movement value(x,y): " & Mouse_cords & Environment.NewLine & "Miliseconds since 1970: " & (DateTime.Now - New DateTime(1970, 1, 1)).TotalMilliseconds
            Thread.Sleep(1)
            If CheckBox1.Checked = False Then
                Label1.Invoke(Sub() Label1.Text = output)
            End If

        End While
    End Sub
    Sub milliTick()
        While True
            '    For i As Integer = 0 To Ticks.Length - 1
            '        If Ticks(i) < 255 Then
            '            Ticks(i) = Ticks(i) + 1
            '            Exit For
            '        Else
            '            Ticks(i) = 0
            '        End If

            '    Next
            Ticks += 1
            Thread.Sleep(1)
        End While

    End Sub

    Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        End
    End Sub
End Class

