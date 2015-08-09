using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection sqlcon = new SqlConnection("Server=7402fdb9-611a-4a2c-8ce6-a4ef00aece3d.sqlserver.sequelizer.com;Database=db7402fdb9611a4a2c8ce6a4ef00aece3d;User ID=olajscujyglsryfi;Password=yNUVfoKWJqTTbiKr3dZDNLSawq6kaFEMzo3q8CTtZmVAudvYLiXMgQNV5TxWryWd;");
    SqlDataAdapter da = new SqlDataAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            show();

        }

    }


    public void show()
    {
        da = new SqlDataAdapter("select * from mytbl12",sqlcon);

        DataTable dt = new DataTable();
        da.Fill(dt);
        Response.Write(DataTableToJsonObj(dt));
    

    }



    public string DataTableToJsonObj(DataTable dt)
    {
        DataSet ds = new DataSet();
        ds.Merge(dt);
        StringBuilder JsonString = new StringBuilder();
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            JsonString.Append("{");
            JsonString.Append("\"data\"");
            JsonString.Append(":");
            JsonString.Append("[");
           
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                JsonString.Append("{");
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    if (j < ds.Tables[0].Columns.Count - 1)
                    {
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                    }
                    else if (j == ds.Tables[0].Columns.Count - 1)
                    {
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
                    }
                }
                if (i == ds.Tables[0].Rows.Count - 1)
                {
                    JsonString.Append("}");
                }
                else
                {
                    JsonString.Append("},");
                }
            }
            JsonString.Append("]");
            JsonString.Append("}");
            return JsonString.ToString();
        }
        else
        {
            return null;
        }
    }  

    protected void Button1_Click(object sender, EventArgs e)
    {
       
    }
}