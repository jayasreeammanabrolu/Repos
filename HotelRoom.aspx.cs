using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HotelRoom : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int rent = 0; int acost = 0; int total=0;
        if (RadioButton1.Checked)
            rent = 1000;
        else if (RadioButton2.Checked)
            rent = 500;
        if (CheckBox1.Checked)
            acost = 300;
        if (CheckBox2.Checked)
            acost += 200;
        total = rent + acost;
        TotalTxt.Text = total.ToString();

    }
}