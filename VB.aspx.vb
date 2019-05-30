Imports System.IO
Imports Ionic.Zip
Imports System.Collections.Generic

Partial Class VB
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim filePaths As String() = Directory.GetFiles(Server.MapPath("~/Files/"))
            Dim files As New List(Of ListItem)()
            For Each filePath As String In filePaths
                files.Add(New ListItem(Path.GetFileName(filePath), filePath))
            Next
            GridView1.DataSource = files
            GridView1.DataBind()
        End If
    End Sub
    Protected Sub DownloadFiles(sender As Object, e As EventArgs)
        Using zip As New ZipFile()
            zip.AlternateEncodingUsage = ZipOption.AsNecessary
            zip.AddDirectoryByName("Files")
            For Each row As GridViewRow In GridView1.Rows
                If TryCast(row.FindControl("chkSelect"), CheckBox).Checked Then
                    Dim filePath As String = TryCast(row.FindControl("lblFilePath"), Label).Text
                    zip.AddFile(filePath, "Files")
                End If
            Next
            Response.Clear()
            Response.BufferOutput = False
            Dim zipName As String = [String].Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"))
            Response.ContentType = "application/zip"
            Response.AddHeader("content-disposition", "attachment; filename=" + zipName)
            zip.Save(Response.OutputStream)
            Response.[End]()
        End Using
    End Sub
End Class
