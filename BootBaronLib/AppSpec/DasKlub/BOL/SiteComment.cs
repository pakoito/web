﻿//  Copyright 2012 
//  Name: Ryan Williams
//  URL: http://ryanmichaelwilliams.com | http://dasklub.com

//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at

//       http://www.apache.org/licenses/LICENSE-2.0

//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
using System;
using System.Data.Common;
using BootBaronLib.BaseTypes;
using BootBaronLib.DAL;
using BootBaronLib.Operational;

namespace BootBaronLib.AppSpec.DasKlub.BOL
{
    public class SiteComment : BaseIUserLogCRUD
    {
        #region properties 

        private int _siteCommentID = 0;

        public int SiteCommentID
        {
            get { return _siteCommentID; }
            set { _siteCommentID = value; }
        }

        private string _detail = string.Empty;

        public string Detail
        {
            get { return _detail; }
            set { _detail = value; }
        }

        #endregion

        #region methods


        public override int Create()
        {
            // get a configured DbCommand object
            DbCommand comm = DbAct.CreateCommand();

            // set the stored procedure name
            comm.CommandText = "up_AddSiteComment";

            ADOExtenstion.AddParameter(comm, "detail", this.Detail);
            ADOExtenstion.AddParameter(comm, "createdByUserID", this.CreatedByUserID);

            // the result is their ID
            string result = string.Empty;
            // execute the stored procedure
            result = DbAct.ExecuteScalar(comm);

            if (string.IsNullOrEmpty(result)) return 0;

            this.SiteCommentID = Convert.ToInt32(result);

            return this.SiteCommentID;
        }

        #endregion
    }
}
