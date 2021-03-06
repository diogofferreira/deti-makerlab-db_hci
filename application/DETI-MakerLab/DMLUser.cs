﻿using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DETI_MakerLab
{

    [Serializable()]
    public class DMLUser
    {
        private static CreateProject createProject = new CreateProject(-1);
        private int _numMec;
        private String _firstName;
        private String _lastName;
        private String _email;
        private String _pathToImage;
        private int _roleID;
        private String _roleDescription;
        private Regex EmailValidator = new Regex(@"[\w]+@ua.pt", RegexOptions.IgnoreCase);

        public int NumMec
        {
            get { return _numMec;  }
            set
            {
                // Check for valid mec. num.
                if (value > 100000) 
                    throw new Exception("Invalid MecNum");
                _numMec = value;
            }
        }

        public String FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public String LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public String Email
        {
            get { return _email; }
            set
            {
                // Check if the email is valid
                var addr = new System.Net.Mail.MailAddress(value);
                if (addr.Address != value)
                    throw new Exception("Invalid email");
                if (!EmailValidator.IsMatch(value))
                    throw new Exception("You must use an UA email account!");
                _email = value;
            }
        }

        public String PathToImage
        {
            get { return _pathToImage; }
            set { _pathToImage = value; }
        }

        public int RoleID
        {
            get { return _roleID; }
            set { _roleID = value; }
        }

        public String RoleDescription
        {
            get { return _roleDescription; }
            set { _roleDescription = value; }
        }

        public String FullName
        {
            get { return _firstName + ' ' + _lastName; }
        }

        public CreateProject CreateProject
        {
            get { return createProject; }
        }

        public override String ToString()
        {
            return FullName + "(" + NumMec.ToString() + ")";
        }

        public DMLUser() { }

        public DMLUser(int NumMec, String FirstName, String LastName, 
            String Email, String PathToImage)
        {
            this.NumMec = NumMec;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.PathToImage = PathToImage;
        }

    }

    [Serializable()]
    public class Professor : DMLUser
    {
        private String _scientificArea;

        public String ScientificArea
        {
            get { return _scientificArea; }
            set { _scientificArea = value; }
        }

        public override String ToString()
        {
            return "Professor: " + NumMec.ToString();
        }

        public Professor(int NumMec, String FirstName, String LastName,
            String Email, String PathToImage, String ScientificArea)
        {
            this.NumMec = NumMec;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.PathToImage = PathToImage;
            this.ScientificArea = ScientificArea;
        }
    }

    [Serializable()]
    public class Student : DMLUser
    {
        private String _course;

        public String Course
        {
            get { return _course; }
            set {
                if (value == null)
                    throw new Exception("Invalid course");
                _course = value; }
        }

        public override String ToString()
        {
            return "Student: " + NumMec.ToString();
        }

        public Student(int NumMec, String FirstName, String LastName,
            String Email, String PathToImage, String Course)
        {
            this.NumMec = NumMec;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.PathToImage = PathToImage;
            this.Course = Course;
        }
    }
}