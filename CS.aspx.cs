using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Ionic.Zip;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Data;
using System.Configuration;

public partial class CS : System.Web.UI.Page
{
    private string constring = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DeleteFile();
            GetData();
        }
    }

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    if (!IsPostBack)
    //    {
    //        string[] filePaths = Directory.GetFiles(Server.MapPath("~/Files/"));
    //        List<ListItem> files = new List<ListItem>();
    //        foreach (string filePath in filePaths)
    //        {
    //            files.Add(new ListItem(Path.GetFileName(filePath), filePath));
    //        }
    //        GridView1.DataSource = files;
    //        GridView1.DataBind();
    //    }
    //}
#region Download File From Folder
    protected void DownloadFiles(object sender, EventArgs e)
    {
        using (ZipFile zip = new ZipFile())
        {
            zip.AlternateEncodingUsage = ZipOption.AsNecessary;
            zip.AddDirectoryByName("Files");
            foreach (GridViewRow row in GridView1.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string filePath = (row.FindControl("lblFilePath") as Label).Text;
                    zip.AddFile(filePath, "Files");
                }
            }
            Response.Clear();
            Response.BufferOutput = false;
            string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "attachment; filename=" + zipName);  
            zip.Save(Response.OutputStream);
            Response.End();
        }
    }
    //DAtabase
    private void GetData()
    {
        SqlConnection con = new SqlConnection(constring);
        SqlCommand cmd = new SqlCommand("Select TOP 5 * from tblFiles ", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        GridView2.DataSource = dt;
        GridView2.DataBind();
    }
#endregion
    protected void OnDownload(object sender, EventArgs e)
    {
        string filePath = string.Empty;
        using (ZipFile zip = new ZipFile())
        {
            zip.AlternateEncodingUsage = ZipOption.AsNecessary;
            zip.AddDirectoryByName("Files");
            foreach (GridViewRow row in GridView2.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    filePath = (row.FindControl("lblFilePath") as Label).Text;
                    zip.AddFile(filePath, "Files");
                }
            }

            Response.Clear();
            Response.BufferOutput = false;
            string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
            zip.Save(Response.OutputStream);
            Response.End();
        }
    }

    private void DeleteFile()
    {
        System.IO.DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/Files/"));
        foreach (FileInfo file in di.GetFiles())
        {
            file.Delete();
        }
    }

    public string ConvertImage(object binaryImage)
    {
        byte[] bytes = (byte[])(binaryImage);
        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
        return "data:image/png;base64," + base64String;
    }

    public string FilePath(object fileData)
    {

        //New Changes
        //byte[] bytes = (byte[])(binaryImage);
        //string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
        //return "data:image/png;base64," + base64String;
        //End
        byte[] bytes = (byte[])(fileData);
        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
        MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length);
        ms.Write(bytes, 0, bytes.Length);
        //System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
        System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
        System.Drawing.Image image = (System.Drawing.Image)converter.ConvertFrom(bytes);

        string newFile = Guid.NewGuid().ToString() + ".jpeg";
        string filePath = Path.Combine(Server.MapPath("~/Files/"), newFile);
        image.Save(filePath, ImageFormat.Jpeg);
        return filePath;
    }
}