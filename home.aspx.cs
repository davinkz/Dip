using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class home : System.Web.UI.Page
{
    private string connectionString = WebConfigurationManager.ConnectionStrings["MyConsString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillBookList();
        }
    }


    private void FillBookList()
    {
        DropDownList2.Items.Clear();
        string selectSql = "SELECT  PolicyName FROM tableDip";

        //define ADO.NET objects

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSql, con);
        SqlDataReader reader;

        //openning database connection
        //implementing operations
        try
        {
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ListItem newItem = new ListItem();
                newItem.Text = reader["PolicyName"].ToString();
               // newItem.Value = reader["BookId"].ToString();
                DropDownList2.Items.Add(newItem);
            }
            reader.Close();
        }
        catch (Exception e)
        {
            Label2.Text = "Error reading list of names. ";
            Label2.Text += e.Message;
        }
        finally
        {
            con.Close();
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        if (TextBox1.Text != "" )
        {
            if (RadioButton1.Checked == true) { 
            //call the method to execute insert to the database
            ExecuteInsert(DropDownList1.SelectedItem.Text,
                          TextBox1.Text,
                          RadioButton1.Text);
            Response.Write("Record was successfully added!");
            ClearControls(Page);
            }
            else
            {
                ExecuteInsert(DropDownList1.SelectedItem.Text,
                         TextBox1.Text,
                         RadioButton2.Text);
                Response.Write("Record was successfully added!");
                ClearControls(Page);
            }
        }
        else
        {
            //Response.Write("Password did not match");
          //  TxtPassword.Focus();
        }
    }

    public static void ClearControls(Control Parent)
    {

        if (Parent is TextBox)
        { (Parent as TextBox).Text = string.Empty; }
        else
        {
            foreach (Control c in Parent.Controls)
                ClearControls(c);
        }
    }

    public string GetConnectionString()
    {
        //sets the connection string from your web config file "ConnString" is the name of your Connection String
        return System.Configuration.ConfigurationManager.ConnectionStrings["MyConsString"].ConnectionString;
    }

    private void ExecuteInsert(string name, string number, string business)
    {
        SqlConnection conn = new SqlConnection(GetConnectionString());
        string sql = "INSERT INTO tableDip (policyName, PolicyNo, business) VALUES "
                    + " (@name,@number,@business)";

        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@id", SqlDbType.Int, 20);
            param[0] = new SqlParameter("@name", SqlDbType.VarChar, 50);
            param[1] = new SqlParameter("@number", SqlDbType.VarChar, 50);
            param[2] = new SqlParameter("@business", SqlDbType.VarChar, 50);
            //param[3] = new SqlParameter("@Gender", SqlDbType.Char, 10);
           // param[4] = new SqlParameter("@Age", SqlDbType.Int, 100);
            //param[5] = new SqlParameter("@Address", SqlDbType.VarChar, 50);

            param[0].Value = name;
            param[1].Value = number;
            param[2].Value = business;
            

            for (int i = 0; i < param.Length; i++)
            {
                cmd.Parameters.Add(param[i]);
            }

            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
        finally
        {
            conn.Close();
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

            string FilePath = Server.MapPath(FolderPath + FileName);
            FileUpload1.SaveAs(FilePath);
            Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text);
        }
    }

    private void Import_To_Grid(string FilePath, string Extension, string isHDR)
    {
        string conStr = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                         .ConnectionString;
                break;
            case ".xlsx": //Excel 07
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                          .ConnectionString;
                break;
        }
        conStr = String.Format(conStr, FilePath, isHDR);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        cmdExcel.Connection = connExcel;

        //Get the name of First Sheet
        connExcel.Open();
        DataTable dtExcelSchema;
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        connExcel.Close();

        //Read Data from First Sheet
        connExcel.Open();
        cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();

        //Bind Data to GridView
        GridView2.Caption = Path.GetFileName(FilePath);
        GridView2.DataSource = dt;
        GridView2.DataBind();
    }

    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
        string FileName = GridView2.Caption;
        string Extension = Path.GetExtension(FileName);
        string FilePath = Server.MapPath(FolderPath + FileName);

        Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text);
        GridView2.PageIndex = e.NewPageIndex;
        GridView2.DataBind();
    }
}