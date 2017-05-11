﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DETI_MakerLab
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private DMLUser _user;

        public DMLUser User
        {
            get { return _user; }
            set
            {
                if (value == null)
                    throw new Exception("Invalid User");
                _user = value;
            }
        }

        public UserPage(DMLUser User)
        {
            InitializeComponent();
            this.User = User;
            this.user_name.Text = this.User.FirstName + ' ' + this.User.LastName;
            this.user_email.Text = this.User.Email;
            this.user_nmec.Text = this.user_nmec.ToString();
            if (typeof(Professor).IsInstanceOfType(this.User))
            {
                //this.course_area.Content = 'Scientific Area';
                this.user_course_area.Text = ((Professor)this.User).ScientificArea;
            }
            else
            {
                //this.course_area.Content = 'Course';
                this.user_course_area.Text = ((Student)this.User).Course;
            }
        }

        internal class LastRequisitions : ObservableCollection<RequisitionInfo>
        {
            private SqlConnection cn;

            public LastRequisitions(int userID)
            {
                cn = getSGBDConnection();
                if (!verifySGBDConnection())
                    throw new Exception("Could not connect to database");

                SqlCommand cmd = new SqlCommand("SELECT * FROM LAST_REQUISITIONS_INFO WHERE UserID=@userid", cn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@userid", userID);
                SqlDataReader reader = cmd.ExecuteReader();
                CultureInfo provider = CultureInfo.InvariantCulture;

                while (reader.Read())
                {
                    Add(new RequisitionInfo(
                        int.Parse(reader["RequisitionID"].ToString()),
                        reader["PrjName"].ToString(),
                        int.Parse(reader["UserID"].ToString()),
                        reader["ProductDescription"].ToString(),
                        int.Parse(reader["Units"].ToString()),
                        DateTime.ParseExact(reader["ReqDate"].ToString(), "yyMMddHHmm", provider)
                        ));
                }
                cn.Close();
            }

            private SqlConnection getSGBDConnection()
            {
                //TODO: fix data source
                return new SqlConnection("data source= DESKTOP-H41EV9L\\SQLEXPRESS;integrated security=true;initial catalog=DML");
            }

            private bool verifySGBDConnection()
            {
                if (cn == null)
                    cn = getSGBDConnection();

                if (cn.State != ConnectionState.Open)
                    cn.Open();

                return cn.State == ConnectionState.Open;
            }
        }
    }
}