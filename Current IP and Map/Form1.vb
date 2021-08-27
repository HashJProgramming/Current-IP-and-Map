Imports System.Text
Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        WebBrowser1.Navigate("http://maps.google.com/maps?q=")
        WebBrowser2.Navigate("http://iplocation.com")
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Stop()

        Dim arreyTag As New ArrayList

        For Each items As HtmlElement In WebBrowser2.Document.GetElementsByTagName("td")
            arreyTag.Add(items.InnerText)
        Next

        lblYourIP.Text = arreyTag(0)
        lblLatitude.Text = arreyTag(1)
        lblLongitude.Text = arreyTag(2)
        lblCountry.Text = arreyTag(3)
        lblRegion.Text = arreyTag(4)
        lblCity.Text = arreyTag(5)
        lblProvider.Text = arreyTag(6)

        btnLat.PerformClick()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLat.Click
        If lblLatitude.Text = String.Empty Or lblLongitude.Text = String.Empty Then
            MessageBox.Show("Supply a latitude and longitude value.", "Missing Data")
        End If

        Try
            Dim lat As String = String.Empty
            Dim lon As String = String.Empty

            Dim queryAddress As New StringBuilder()
            queryAddress.Append("http://maps.google.com/maps?q=")

            ' build latitude part of query string
            If lblLatitude.Text <> String.Empty Then
                lat = lblLatitude.Text
                queryAddress.Append(lat + "%2C")
            End If

            ' build longitude part of query string
            If lblLongitude.Text <> String.Empty Then
                lon = lblLongitude.Text
                queryAddress.Append(lon)
            End If

            WebBrowser1.Navigate(queryAddress.ToString())

        Catch ex As Exception

            MessageBox.Show(ex.Message.ToString(), "Error")

        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCity.Click
        Try
            Dim street As String = String.Empty
            Dim city As String = String.Empty
            Dim state As String = String.Empty
            Dim zip As String = String.Empty

            Dim queryAddress As New StringBuilder()
            queryAddress.Append("http://maps.google.com/maps?q=")

            ' build city part of query string
            If lblCity.Text <> String.Empty Then
                city = lblCity.Text.Replace(" ", "+")
                queryAddress.Append(city + "," & "+")
            End If

            ' pass the url with the query string to web browser control
            WebBrowser1.Navigate(queryAddress.ToString())

        Catch ex As Exception

            MessageBox.Show(ex.Message.ToString(), "Unable to Retrieve Map")

        End Try
    End Sub
End Class
