using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int s1 = int.Parse(Sub1Txt.Text);
        int s2 = int.Parse(Sub2Txt.Text);
        int s3 = int.Parse(Sub3Txt.Text);
        int total = s1 + s2 + s3;
        TotalTxt.Text = total.ToString();
        if (total >= 70)
            GradeTxt.Text = "A";
        else if (total >= 60 && total < 70)
        {
            GradeTxt.Text = "B";
        }
        else if (total >= 50 && total < 60)
        {
            GradeTxt.Text = "C";
        }
        else
        {
            GradeTxt.Text = "D";
        }


    }
}