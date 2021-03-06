﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DETI_MakerLab
{
    [Serializable()]
    public class Requisition
    {
        private int _requisitionID;
        private Project _reqProject;
        private DMLUser _user;
        private DateTime _reqDate;
        private List<Resources> _resources = new List<Resources>();

        public int RequisitionID
        {
            get { return _requisitionID; }
            set { _requisitionID = value; }
        }

        public Project ReqProject
        {
            get { return _reqProject; }
            set
            {
                if (value == null)
                    throw new Exception("Invalid Project");
                _reqProject = value;
            }
        }

        public DMLUser User
        {
            get { return _user; }
            set { _user = value; }
        }

        public DateTime ReqDate
        {
            get { return _reqDate; }
            set
            { _reqDate = value; }
        }

        public List<Resources> Resources
        {
            get { return _resources; }
        }

        public void addResource(Resources resourceID)
        {
            Resources.Add(resourceID);
        }

        public override string ToString()
        {
            return "ID #" + RequisitionID.ToString() + " at " + ReqDate.ToShortDateString() + " by " + User.FullName;
        }

        public override bool Equals(object obj)
        {
            if (!typeof(Requisition).IsInstanceOfType(obj))
                return false;
            return this.RequisitionID == (obj as Requisition).RequisitionID;
        }

        public Requisition(int RequisitionID, Project ReqProject, DMLUser User, DateTime ReqDate)
        {
            this.RequisitionID = RequisitionID;
            this.ReqProject = ReqProject;
            this.User = User;
            this.ReqDate = ReqDate;
        }
    }
}
