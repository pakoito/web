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
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using BootBaronLib.BaseTypes;
using BootBaronLib.DAL;
using BootBaronLib.Operational;
using BootBaronLib.Operational.Converters;
using System.Web.Caching;
using BootBaronLib.AppSpec.DasKlub.BLL;
using System.Web;

namespace BootBaronLib.AppSpec.DasKlub.BOL
{
    public class BlockedUser : BaseIUserLogCRUD
    {
        public BlockedUser() { }

        public BlockedUser(DataRow dr)
        {
            Get(dr);
        }

        #region properties

        private int _blockedUserID = 0;

        public int BlockedUserID
        {
            get { return _blockedUserID; }
            set { _blockedUserID = value; }
        }

        private int _userAccountIDBlocking = 0;

        public int UserAccountIDBlocking
        {
            get { return _userAccountIDBlocking; }
            set { _userAccountIDBlocking = value; }
        }

        private int _userAccountIDBlocked = 0;

        public int UserAccountIDBlocked
        {
            get { return _userAccountIDBlocked; }
            set { _userAccountIDBlocked = value; }
        }

        #endregion

        public override int Create()
        { 
            // get a configured DbCommand object
            DbCommand comm = DbAct.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "up_AddBlockedUser";

            // create a new parameter

            ADOExtenstion.AddParameter(comm, StaticReflection.GetMemberName<string>(x => this.UserAccountIDBlocking), UserAccountIDBlocking);
            ADOExtenstion.AddParameter(comm, StaticReflection.GetMemberName<string>(x => this.UserAccountIDBlocked), UserAccountIDBlocked);
            ADOExtenstion.AddParameter(comm, StaticReflection.GetMemberName<string>(x => this.CreatedByUserID), CreatedByUserID);
 
            // the result is their ID
            string result = string.Empty;
            // execute the stored procedure
            result = DbAct.ExecuteScalar(comm);

            if (string.IsNullOrEmpty(result)) return 0;

            this.BlockedUserID = Convert.ToInt32(result);

            return this.BlockedUserID;
        }

        public override void Get(DataRow dr)
        {
            base.Get(dr);

            this.BlockedUserID = FromObj.IntFromObj(dr[StaticReflection.GetMemberName<string>(x => this.BlockedUserID)]);
            this.UserAccountIDBlocked = FromObj.IntFromObj(dr[StaticReflection.GetMemberName<string>(x => this.UserAccountIDBlocked)]);
            this.UserAccountIDBlocking = FromObj.IntFromObj(dr[StaticReflection.GetMemberName<string>(x => this.UserAccountIDBlocking)]);
        }


        public override bool Delete()
        {
            return Delete(this.UserAccountIDBlocking, this.UserAccountIDBlocked);
        }


        public static bool Delete(int userAccountIDBlocking, int userAccountIDBlocked)
        {

            // get a configured DbCommand object
            DbCommand comm = DbAct.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "up_DeleteBlockedUser";

            ADOExtenstion.AddParameter(comm, "userAccountIDBlocking", userAccountIDBlocking);
            ADOExtenstion.AddParameter(comm, "userAccountIDBlocked", userAccountIDBlocked);
            // execute the stored procedure

            return DbAct.ExecuteNonQuery(comm) > 0;

        }


        public static bool IsBlockedUser(int userAccountIDBlocking, int userAccountIDBlocked)
        {
            string cacheName = "IsBlockedUser" + "-" + userAccountIDBlocking.ToString() + "-" +
                userAccountIDBlocked.ToString();

            bool rslt = false;


            if (HttpContext.Current.Cache[cacheName] == null)
            {
                DbCommand comm = DbAct.CreateCommand();
                // set the stored procedure name
                comm.CommandText = "up_IsBlockedUser";

                ADOExtenstion.AddParameter(comm, "userAccountIDBlocking", userAccountIDBlocking);
                ADOExtenstion.AddParameter(comm, "userAccountIDBlocked", userAccountIDBlocked);

                // execute the stored procedure
                rslt = DbAct.ExecuteScalar(comm) == "1";

                HttpContext.Current.Cache.AddObjToCache(rslt, cacheName);
            }
            else
            {
                rslt = (bool)HttpContext.Current.Cache[cacheName];
            }
            return rslt;
        }

        public static bool IsBlockingUser(int userAccountIDBlocking, int userAccountIDBlocked)
        {

            string cacheName = "IsBlockingUser" + "-" + userAccountIDBlocking.ToString() + "-" +
    userAccountIDBlocked.ToString();

            bool rslt = false;

            if (HttpContext.Current.Cache[cacheName] == null)
            {
                DbCommand comm = DbAct.CreateCommand();
                // set the stored procedure name
                comm.CommandText = "up_IsBlockingUser";

                ADOExtenstion.AddParameter(comm, "userAccountIDBlocking", userAccountIDBlocking);
                ADOExtenstion.AddParameter(comm, "userAccountIDBlocked", userAccountIDBlocked);

                // execute the stored procedure
                rslt = DbAct.ExecuteScalar(comm) == "1";

                HttpContext.Current.Cache.AddObjToCache(rslt, cacheName);
            }
            else
            {
                rslt = (bool)HttpContext.Current.Cache[cacheName];
            }
            return rslt;


        }
    }

    public class BlockedUsers : List< BlockedUser>
    {

        public static bool HasBlockedUsers(int userAccountIDBlocking)
        {
            BlockedUsers bus = new BlockedUsers();

            bus.GetBlockedUsers(userAccountIDBlocking);

            return (bus != null && bus.Count > 0);
        }

        public void GetBlockedUsers(int userAccountIDBlocking)
        {
            // get a configured DbCommand object
            DbCommand comm = DbAct.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "up_GetBlockedUsers";

            ADOExtenstion.AddParameter(comm, "userAccountIDBlocking", userAccountIDBlocking);

            // execute the stored procedure
            DataTable dt = DbAct.ExecuteSelectCommand(comm);

            // was something returned?
            if (dt != null && dt.Rows.Count > 0)
            {
                BlockedUser ccomm = null;

                foreach (DataRow dr in dt.Rows)
                {
                    ccomm = new BlockedUser(dr);
                    this.Add(ccomm);
                }
            }

        }
    }
}
