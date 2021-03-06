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
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using BootBaronLib.AppSpec.DasKlub.BLL;
using BootBaronLib.BaseTypes;
using BootBaronLib.DAL;
using BootBaronLib.Enums;
using BootBaronLib.Interfaces;
using BootBaronLib.Operational;
using BootBaronLib.Operational.Converters;
using BootBaronLib.Resources;

namespace BootBaronLib.AppSpec.DasKlub.BOL
{
    public class StatusUpdate : BaseIUserLogCRUD, ICacheName, IUnorderdListItem, IJSONResponse
    {
        #region constructors

        public StatusUpdate(DataRow dr)
        {
            Get(dr);
        }

        public StatusUpdate()
        {

        }

        public StatusUpdate(int statusUpdateID)
        {
            Get(statusUpdateID);
        }

        #endregion

        #region properties

        private int _statusUpdateID = 0;

        public int StatusUpdateID
        {
            get { return _statusUpdateID; }
            set { _statusUpdateID = value; }
        }


        private bool _isMobile = false;

        public bool IsMobile
        {
            get { return _isMobile; }
            set { _isMobile = value; }
        }

        private int? _photoItemID = null;

        public int? PhotoItemID
        {
            get { return _photoItemID; }
            set { _photoItemID = value; }
        }

        private int? _zoneID = null;

        public int? ZoneID
        {
            get { return _zoneID; }
            set { _zoneID = value; }
        }

        private int _userAccountID = 0;

        public int UserAccountID
        {
            get { return _userAccountID; }
            set { _userAccountID = value; }
        }

        private string _message = string.Empty;

        public string Message
        {
            get {
                if (_message == null) return _message;
                else return _message.Trim(); }
            set { _message = value; }
        }

        private char _statusType = char.MinValue;



        public char StatusType
        {
            get { return _statusType; }
            set { _statusType = value; }
        }

        #endregion

        public void GetMostAcknowledgedStatus(int daysBack, char acknowledgementType)
        {

            // get a configured DbCommand object
            DbCommand comm = DbAct.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "up_GetMostAcknowledgedStatus";

            ADOExtenstion.AddParameter(comm, "daysBack", daysBack);
            ADOExtenstion.AddParameter(comm, "acknowledgementType", acknowledgementType);


            // execute the stored procedure
            DataTable dt = DbAct.ExecuteSelectCommand(comm);

            // was something returned?
            if (dt != null && dt.Rows.Count > 0)
            {
                Get(FromObj.IntFromObj(dt.Rows[0]["statusUpdateID"]));
            }


        }

        public override void Get(int statusUpdateID)
        {
            this.StatusUpdateID = statusUpdateID;

            // get a configured DbCommand object
            DbCommand comm = DbAct.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "up_GetStatusUpdateByID";

            ADOExtenstion.AddParameter(comm, StaticReflection.GetMemberName<string>(x => this.StatusUpdateID), StatusUpdateID);

            // execute the stored procedure
            DataTable dt = DbAct.ExecuteSelectCommand(comm);

            // was something returned?
            if (dt != null && dt.Rows.Count > 0)
            {
                Get(dt.Rows[0]);
            }
        }

        public override void Get(DataRow dr)
        {
            try
            {
                base.Get(dr);


                this.Message = FromObj.StringFromObj(dr[StaticReflection.GetMemberName<string>(x => this.Message)]);
                this.StatusType = FromObj.CharFromObj(dr[StaticReflection.GetMemberName<string>(x => this.StatusType)]);
                this.StatusUpdateID = FromObj.IntFromObj(dr[StaticReflection.GetMemberName<string>(x => this.StatusUpdateID)]);
                this.UserAccountID = FromObj.IntFromObj(dr[StaticReflection.GetMemberName<string>(x => this.UserAccountID)]);
                this.ZoneID = FromObj.IntNullableFromObj(dr[StaticReflection.GetMemberName<string>(x => this.ZoneID)]);
                this.PhotoItemID = FromObj.IntNullableFromObj(dr[StaticReflection.GetMemberName<string>(x => this.PhotoItemID)]);
                this.IsMobile = FromObj.BoolFromObj(dr[StaticReflection.GetMemberName<string>(x => this.IsMobile)]);
                
            }
            catch { }

        }

        public override int Create()
        {

            // get a configured DbCommand object
            DbCommand comm = DbAct.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "up_AddStatusUpdate";

            ADOExtenstion.AddParameter(comm, StaticReflection.GetMemberName<string>(x => this.CreatedByUserID), CreatedByUserID);
            ADOExtenstion.AddParameter(comm, StaticReflection.GetMemberName<string>(x => this.UserAccountID), UserAccountID);
            ADOExtenstion.AddParameter(comm, StaticReflection.GetMemberName<string>(x => this.Message), Message);
            ADOExtenstion.AddParameter(comm, StaticReflection.GetMemberName<string>(x => this.StatusType), StatusType);
            ADOExtenstion.AddParameter(comm, StaticReflection.GetMemberName<string>(x => this.PhotoItemID), PhotoItemID);
            ADOExtenstion.AddParameter(comm, StaticReflection.GetMemberName<string>(x => this.ZoneID), ZoneID);
            ADOExtenstion.AddParameter(comm, StaticReflection.GetMemberName<string>(x => this.IsMobile), IsMobile); 

            // the result is their ID
            string result = string.Empty;
            // execute the stored procedure
            result = DbAct.ExecuteScalar(comm);

            if (string.IsNullOrEmpty(result))
            {
                return 0;
            }
            else
            {
                this.StatusUpdateID = Convert.ToInt32(result);

                return this.StatusUpdateID;
            }
        }

        public override bool Delete()
        {
            if (this.StatusUpdateID == 0) return false;

            // get a configured DbCommand object
            DbCommand comm = DbAct.CreateCommand();

            // set the stored procedure name
            comm.CommandText = "up_DeleteStatusUpdate";

            ADOExtenstion.AddParameter(comm, StaticReflection.GetMemberName<string>(x => this.StatusUpdateID), StatusUpdateID);

            RemoveCache();

            // execute the stored procedure

            return DbAct.ExecuteNonQuery(comm) > 0;
        }

        #region ICacheName Members

        public string CacheName
        {
            get { throw new NotImplementedException(); }
        }

        public void RemoveCache()
        {
            return;
        }

        #endregion

       
        public string StatusAcknowledgements
        {
            get
            {
                StringBuilder sb = new StringBuilder(100);
                Acknowledgement ack = null;

                MembershipUser mu = Membership.GetUser();

                if (mu == null) return string.Empty;


                Acknowledgements acks = new Acknowledgements();
                acks.GetAcknowledgementsForStatus(this.StatusUpdateID);

                UserAccounts uaApplauds = new UserAccounts();
                UserAccounts uaBeats = new UserAccounts();
                UserAccount uaRsp = null;

                foreach (Acknowledgement ack1 in acks)
                {
                    uaRsp = new UserAccount(ack1.CreatedByUserID);

                    if (ack1.AcknowledgementType == Convert.ToChar(SiteEnums.AcknowledgementType.A.ToString()))
                    {
                        uaApplauds.Add(uaRsp);
                    }
                    else if (ack1.AcknowledgementType == Convert.ToChar(SiteEnums.AcknowledgementType.B.ToString()))
                    {
                        uaBeats.Add(uaRsp);
                    }
                }


                if (mu != null && Acknowledgement.IsUserAcknowledgement(this.StatusUpdateID, Convert.ToInt32(mu.ProviderUserKey)))
                {
                    sb.Append(@"<div class=""left_float"">");

                    StringBuilder sbApplaud = new System.Text.StringBuilder(100);

                    int i = 0;

                    foreach (UserAccount uar1 in uaApplauds)
                    {
                        i++;

                        if (i == uaApplauds.Count)
                        {
                            sbApplaud.AppendFormat("{0}", uar1.UserName);
                        }
                        else
                        {
                            sbApplaud.AppendFormat("{0}, ", uar1.UserName);
                        }
                    }
                 
                    sb.AppendFormat(@"<span class=""status_count_applaud"" title=""{0}"">", sbApplaud.ToString());
                    sb.Append(Acknowledgements.GetAcknowledgementCount(this.StatusUpdateID, Convert.ToChar( SiteEnums.AcknowledgementType.A.ToString())));
                    sb.Append(@"</span>");
 
                    ack = new Acknowledgement();
                    ack.GetAcknowledgement(this.StatusUpdateID, Convert.ToInt32(mu.ProviderUserKey));

                    if (ack.AcknowledgementID > 0 && ack.AcknowledgementType ==  Convert.ToChar( SiteEnums.AcknowledgementType.A.ToString()))
                    {
                        sb.AppendFormat(@"<button title=""{0}"" name=""status_update_id_applaud""", Messages.YouResponded);
                        sb.Append(@" class=""applaud_status_complete""  type=""button"" value=""");
                        sb.Append(this.StatusUpdateID.ToString());
                        sb.AppendFormat(@""">{0}</button>", Messages.Applaud);
                    }
                    else
                    {
                        sb.AppendFormat(@"<button title=""{0}"" name=""status_update_id_applaud""", Messages.YouResponded);
                        sb.Append(@" disabled=""disabled"" class=""applaud_status""  type=""button"" value=""");
                        sb.Append(this.StatusUpdateID.ToString());
                        sb.AppendFormat(@""">{0}</button>", Messages.Applaud);
                    }

                    sb.Append(@"</div>");

                    sb.Append(@"<div class=""left_float"">");

                    StringBuilder sbBeatDowns = new System.Text.StringBuilder(100);

                    foreach (UserAccount uar1 in uaBeats)
                    {
                        i++;

                        if (i == uaBeats.Count)
                        {
                            sbBeatDowns.AppendFormat("{0}", uar1.UserName);
                        }
                        else
                        {
                            sbBeatDowns.AppendFormat("{0}, ", uar1.UserName);
                        }
                    }

                    sb.AppendFormat(@"<span class=""status_count_beatdown"" title=""{0}"">", sbBeatDowns.ToString());
                    sb.Append(Acknowledgements.GetAcknowledgementCount(this.StatusUpdateID, Convert.ToChar( SiteEnums.AcknowledgementType.B.ToString())));
                    sb.Append(@"</span>");

                    if (ack.AcknowledgementID > 0 && ack.AcknowledgementType == Convert.ToChar( SiteEnums.AcknowledgementType.B.ToString()))
                    {
                        sb.AppendFormat(@"<button title=""{0}""", Messages.YouResponded);
                        sb.Append(@" class=""beat_status_complete"" name=""status_update_id_beat"" type=""button"" value=""");
                        sb.Append(this.StatusUpdateID.ToString());
                        sb.AppendFormat(@""">{0}</button>", Messages.BeatDown);
                    }
                    else
                    {
                        sb.AppendFormat(@"<button title=""{0}""", Messages.YouResponded);
                        sb.Append(@" class=""beat_status"" disabled=""disabled"" name=""status_update_id_beat"" type=""button"" value=""");
                        sb.Append(this.StatusUpdateID.ToString());
                        sb.AppendFormat(@""">{0}</button>", Messages.BeatDown);
                    }

                    sb.Append(@"</div>");
 
                }
                else
                {
                    sb.Append(@"<div class=""left_float"">");


                    StringBuilder sbApplaud = new System.Text.StringBuilder(100);

                    int i = 0;

                    foreach (UserAccount uar1 in uaApplauds)
                    {
                        i++;

                        if (i == uaApplauds.Count)
                        {
                            sbApplaud.AppendFormat("{0}", uar1.UserName);
                        }
                        else
                        {
                            sbApplaud.AppendFormat("{0}, ", uar1.UserName);
                        }
                    }

                    sb.AppendFormat(@"<span class=""status_count_applaud"" title=""{0}"">", sbApplaud.ToString());
                    sb.Append(Acknowledgements.GetAcknowledgementCount(this.StatusUpdateID, Convert.ToChar( SiteEnums.AcknowledgementType.A.ToString())));
                    sb.Append(@"</span>");
                    sb.AppendFormat(@"<button title=""{0}"" name=""status_update_id_applaud""", Messages.Applaud);
                    sb.Append(@" class=""applaud_status"" type=""button"" value=""");
                    sb.Append(this.StatusUpdateID.ToString());
                    sb.AppendFormat(@""">{0}</button>", Messages.Applaud);

                    sb.Append(@"</div>");

                    sb.Append(@"<div class=""left_float"">");

                    StringBuilder sbBeatDowns = new System.Text.StringBuilder(100);

                    i = 0; // reset count

                    foreach (UserAccount uar1 in uaBeats)
                    {
                        i++;

                        if (i == uaBeats.Count)
                        {
                            sbBeatDowns.AppendFormat("{0}", uar1.UserName);
                        }
                        else
                        {
                            sbBeatDowns.AppendFormat("{0}, ", uar1.UserName);
                        }
                    }

                    sb.AppendFormat(@"<span class=""status_count_beatdown"" title=""{0}"">", sbBeatDowns.ToString());

  
                    sb.Append(Acknowledgements.GetAcknowledgementCount(this.StatusUpdateID, Convert.ToChar(SiteEnums.AcknowledgementType.B.ToString())));
                    sb.Append(@"</span>");
                    sb.AppendFormat(@"<button title=""{0}"" name=""status_update_id_beat""", Messages.BeatDown);
                    sb.Append(@" class=""beat_status"" type=""button"" value=""");
                    sb.Append(this.StatusUpdateID.ToString());
                    sb.AppendFormat(@""">{0}</button>", Messages.BeatDown);

                    sb.Append(@"</div>");
 
                }

                return sb.ToString();
            }

        }


        public void GetMostRecentUserStatus(int userAccountID)
        {
            this.UserAccountID = userAccountID;

            // get a configured DbCommand object
            DbCommand comm = DbAct.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "up_GetMostRecentUserStatus";

            ADOExtenstion.AddParameter(comm, StaticReflection.GetMemberName<string>(x => this.UserAccountID), UserAccountID);

            DataTable dt = DbAct.ExecuteSelectCommand(comm);

            if (dt != null && dt.Rows.Count > 0)
            {
                Get(dt.Rows[0]);
            }
        }





        public string RenderOut
        {
            get
            {
                return Video.IFrameVideo(this.Message);
            }

        }



        public string JSONResponse
        {
            get
            {
                return @"{""StatusMessage"": """ + HttpUtility.HtmlEncode(this.ToUnorderdListItem) + @"""}";
            }
        }

        private bool _photoDisplay = false;

        public bool PhotoDisplay
        {
            get { return _photoDisplay; }
            set { _photoDisplay = value; }
        }

        public string ToUnorderdListItem
        {
            get
            {
                if (this.StatusUpdateID == 0 || this.UserAccountID == 0) return string.Empty;

                StringBuilder sb = new StringBuilder(100);

                string statusCss = string.Empty;
                UserAccount ua = new UserAccount(this.UserAccountID);
                bool isUsersPost = false;

                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    UserAccount currentUser = new UserAccount(HttpContext.Current.User.Identity.Name);

                    if (currentUser.UserAccountID != 0 && ua.UserAccountID == currentUser.UserAccountID)
                    {
                        isUsersPost = true;
                    }
                }


                sb.AppendFormat(@"<li class=""status_post"" id=""status_update_id_{0}"">", StatusUpdateID);

                if (this.PhotoItemID != null)
                {
                    PhotoItem pitem = new PhotoItem(Convert.ToInt32(this.PhotoItemID));

                    sb.AppendFormat(@"<div class=""row"">
                                      <div class=""span6"">
                        <a class=""m_over"" href=""{0}"" target=""_blank""><img src=""{1}"" alt=""{2}"" title=""{2}"" /></a>
                                       ",
                        Utilities.S3ContentPath(pitem.FilePathRaw),
                        Utilities.S3ContentPath(pitem.FilePathStandard),
                        Messages.SourceFile);




                    if (isUsersPost)
                    {
                        if (this.PhotoItemID != null && this.PhotoItemID > 0)
                        {
                            sb.AppendFormat(@"<br /><span class=""rotate_photo""><a href=""{0}"">{1}</a></span>",

                                System.Web.VirtualPathUtility.ToAbsolute("~/account/RotateStatusImage?statusUpdateID=" + this.StatusUpdateID.ToString()),
                                Messages.RotatePhoto);
                        }

                    }

                    sb.Append(@"</div></div>");

                }

                sb.AppendFormat(@"<div>");

                #region user icon



                if (!PhotoDisplay)
                {
                    UserAccountDetail uad = new UserAccountDetail();
                    uad.GetUserAccountDeailForUser(ua.UserAccountID);

                    sb.AppendFormat(@"<div class=""user_account_thumb"">{0}</div>", uad.SmallUserIcon);
                }
                else
                {
                    sb.AppendFormat(@"<div>{0}: <a href=""{1}"">{2}</a></div>", Messages.Uploader,
                     System.Web.VirtualPathUtility.ToAbsolute("~/" + ua.UserName), ua.UserName);
                }


               


                #endregion

                #region acknowledgements

                if (!PhotoDisplay)
                {
                    sb.AppendFormat(@"<div class=""acknowlege_options""><div id=""status_ack_{0}"">{1}</div></div>",
                        this.StatusUpdateID, this.StatusAcknowledgements);
                }
                else
                {
                    sb.AppendFormat(@"<div>{0}: {1}</div>",
                        Messages.Applauded,
                        Acknowledgements.GetAcknowledgementCount(this.StatusUpdateID,
                        Convert.ToChar(SiteEnums.AcknowledgementType.A.ToString())));

                    sb.AppendFormat(@"<div>{0}: {1}</div>",
                        Messages.BeatenDown,
                        Acknowledgements.GetAcknowledgementCount(this.StatusUpdateID,
                        Convert.ToChar(SiteEnums.AcknowledgementType.B.ToString())));
                }

                #endregion

                sb.AppendFormat(@"</div>");

                sb.Append(@"<div class=""clear""></div>");

                #region message

                if (this.IsMobile)
                {
                    sb.AppendFormat(@"<img src=""{0}"" alt=""{1}"" title=""{1}"" />&nbsp;",
                         System.Web.VirtualPathUtility.ToAbsolute("~/content/images/icons/icon_mobile.png"),
                         Messages.FromMobile);
                }
                else
                {
                    sb.AppendFormat(@"<img src=""{0}"" alt=""{1}"" title=""{1}"" />&nbsp;",
                        System.Web.VirtualPathUtility.ToAbsolute("~/content/images/icons/icon_desktop.png"),
                        Messages.FromDesktop);
                }

                string timeElapsed = Utilities.TimeElapsedMessage(CreateDate);



                sb.AppendFormat(@"<i title=""{1}"">{0}</i>", timeElapsed, CreateDate.ToString("o"));


                if (!string.IsNullOrWhiteSpace(this.Message))
                {
                    sb.Append("<br />");
                }
                sb.Append("<br />");

                if (this.PhotoItemID == null)
                {
                    sb.AppendFormat(@"<div class=""post_content"">{0}</div>", Video.IFrameVideo(FromString.ReplaceNewLineWithHTML(this.Message)));
                }
                else
                {
                  //  sb.AppendFormat(@"<div class=""post_content"">{0}</div>", FromString.ReplaceNewLineWithHTML(this.Message));
                    sb.AppendFormat(@"<div class=""post_content"">{0}</div>", Utilities.MakeLink(FromString.ReplaceNewLineWithHTML(this.Message)));
                }
                #endregion

                sb.Append(@"<br />");

                #region comments

                sb.Append(@"<div class=""row"">");

                sb.Append(@"<div class=""span6"">");

                // begin: comments
                StatusComments statcoms = new StatusComments();
                statcoms.GetAllStatusCommentsForUpdate(StatusUpdateID);


                sb.Append(@"<div class=""status_accordion_child"">");

                sb.AppendFormat(@"{1}: <span class=""status_comment_count"" id=""status_comments_count_{0}"">{2}</span>",
                       StatusUpdateID, Messages.Comments, StatusComments.GetStatusCommentCount(StatusUpdateID));


                sb.AppendFormat(@"<div class=""status_comment_content""  id=""status_comments_{1}"">{0}</div>", statcoms.ToUnorderdList, StatusUpdateID);


                // end: comments



                sb.Append(@"<div class=""status_comment_outer"">");

                sb.Append("<br />");


                sb.AppendFormat(@"<textarea class=""status_comment input-large expand50-200"" name=""status_comment_{0}"" id=""status_comment_{0}""></textarea>", StatusUpdateID);

                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    sb.AppendFormat(@"<button   title=""{0}"" name=""comment_status_id"" 
                                class=""btn btn-success comment_on_status""  type=""button"" value=""{1}"">{0}</button>",
                                    Messages.Comment, StatusUpdateID);
                }
                else
                {
                    sb.AppendFormat(@"<button disabled=""disabled"" title=""{0}"" name=""comment_status_id"" 
                                class=""btn btn-success comment_on_status""  type=""button"" value=""{1}"">{0}</button> ",
                Messages.Comment, StatusUpdateID);

                    sb.AppendFormat(@" &nbsp;<a href=""{0}"">{1}</a>", System.Web.VirtualPathUtility.ToAbsolute("~/account/logon"), Messages.SignIn); ;
                }


                sb.Append(@"</div>");



          

                sb.Append(@"</div>");

                MembershipUser mu = Membership.GetUser();

                if (mu != null)
                {
                    UserAccount ua1 = new UserAccount(Convert.ToInt32(mu.ProviderUserKey));

                    if (isUsersPost || (ua != null && ua1.IsAdmin))
                    {
                        sb.AppendFormat(@"<button title=""{0}"" name=""delete_status_id"" 
                    class=""delete_icon btn btn-danger btn-mini"" type=""button"" value=""{1}"">{0}</button>", Messages.Delete, StatusUpdateID);
                    }
                }

           

                sb.Append(@"</div>");

                sb.Append(@"</div>");

                #endregion

                sb.Append(@"</li>");

                return sb.ToString();
            }
        }

        public void GetStatusUpdateByPhotoID(int photoItemID)
        {
            this.PhotoItemID = photoItemID;

            // get a configured DbCommand object
            DbCommand comm = DbAct.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "up_GetStatusUpdateByPhotoID";

            ADOExtenstion.AddParameter(comm, StaticReflection.GetMemberName<string>(x => this.PhotoItemID), PhotoItemID);

            DataTable dt = DbAct.ExecuteSelectCommand(comm);

            if (dt != null && dt.Rows.Count > 0)
            {
                Get(dt.Rows[0]);
            }
        }


    
 
    }


    public class StatusUpdates : List<StatusUpdate>, IUnorderdList
    {

        public static string MostFrequentStatusMessages()
        {
            string output = null;

            string cacheName = "MostFrequentStatusMessages";

            if (HttpContext.Current != null && HttpContext.Current.Cache[cacheName] == null)
            {

                // get a configured DbCommand object
                DbCommand comm = DbAct.CreateCommand();
                // set the stored procedure name
                comm.CommandText = "up_MostFrequentStatusMessages";

                DataTable dt = DbAct.ExecuteSelectCommand(comm);


                // was something returned?
                if (dt != null && dt.Rows.Count > 0)
                {
                    Dictionary<string, int> bandCount = new Dictionary<string, int>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        string[] bandsList = FromObj.StringFromObj(dr["message"]).Split(' ');

                        foreach (string sss in bandsList)
                        {
                            if (string.IsNullOrWhiteSpace(sss)) continue;

                            if (bandCount.ContainsKey(sss.Trim().ToLower()))
                            {
                                bandCount[sss.Trim().ToLower()] = bandCount[sss.Trim().ToLower().ToLower()] + 1;
                            }
                            else
                            {
                                bandCount[sss.Trim().ToLower()] = 1;
                            }
                            //  dt.Rows.Add(new object[] { sss.Trim() });
                        }
                    }

                    List<KeyValuePair<string, int>> myList = bandCount.ToList();

                    myList.Sort(
                        delegate(KeyValuePair<string, int> firstPair,
                        KeyValuePair<string, int> nextPair)
                        {
                            return nextPair.Value.CompareTo(firstPair.Value);
                        }
                    );

                    StringBuilder sb = new StringBuilder();

                    sb.Append("<ol>");

                    int counter = 0;

                    while (counter < 100)
                    {
                        counter++;
                        // do something with entry.Value or entry.Key\
                        // Response.Write(entry.
                        sb.AppendFormat("<li>{0} : <i>{1}</i></li>", myList[counter].Key, myList[counter].Value);
                    }

                    sb.Append("</ol>");

                    output = sb.ToString();

                    HttpContext.Current.Cache.AddObjToCache(output, cacheName);
                }

            }
            else
            {
                output = (string)HttpContext.Current.Cache[cacheName];
            }


            return output;

        }

        public void GetMostAcknowledgedStatus(int daysBack, char acknowledgementType /* TODO: USE ENUMS*/)
        {

            // get a configured DbCommand object
            DbCommand comm = DbAct.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "up_GetMostAcknowledgedStatus";

            ADOExtenstion.AddParameter(comm, "daysBack", daysBack);
            ADOExtenstion.AddParameter(comm, "acknowledgementType", acknowledgementType);


            // execute the stored procedure
            DataTable dt = DbAct.ExecuteSelectCommand(comm);

            StatusUpdate su = null;

            // was something returned?
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    su = new StatusUpdate(FromObj.IntFromObj(dr["statusUpdateID"]));
                    this.Add(su);
                }
            }
        }


        public int GetStatusUpdatesPageWise(int pageIndex, int pageSize)
        {
            // get a configured DbCommand object
            DbCommand comm = DbAct.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "up_GetStatusUpdatesPageWise";

            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@RecordCount";
            //http://stackoverflow.com/questions/3759285/ado-net-the-size-property-has-an-invalid-size-of-0
            param.Size = 1000;
            param.Direction = ParameterDirection.Output;
            comm.Parameters.Add(param);

            ADOExtenstion.AddParameter(comm, "PageIndex", pageIndex);
            ADOExtenstion.AddParameter(comm, "PageSize", pageSize);

            DataSet ds = DbAct.ExecuteMultipleTableSelectCommand(comm);

            int recordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);

            if (ds.Tables[0].Rows.Count > 0)
            {
                StatusUpdate statup = null;

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    statup = new StatusUpdate(dr);
                    this.Add(statup);
                }
            }

            return recordCount;
        }


        public static bool DeleteAllStatusUpdates(int userAccountID)
        {
            if (userAccountID == 0) return false;

            // get a configured DbCommand object
            DbCommand comm = DbAct.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "up_DeleteAllStatusUpdates";

            ADOExtenstion.AddParameter(comm, "userAccountID", userAccountID);

            // execute the stored procedure
            return DbAct.ExecuteNonQuery(comm) > 0;
        }


        public void GetAllUserStatusUpdates(int userAccountID)
        {
            // get a configured DbCommand object
            DbCommand comm = DbAct.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "up_GetAllUserStatusUpdates";

            ADOExtenstion.AddParameter(comm, "userAccountID", userAccountID);


            // execute the stored procedure
            DataTable dt = DbAct.ExecuteSelectCommand(comm);

            // was something returned?
            if (dt != null && dt.Rows.Count > 0)
            {
                StatusUpdate art = null;
                foreach (DataRow dr in dt.Rows)
                {
                    art = new StatusUpdate(dr);
                    this.Add(art);
                }
            }

        }




        public void GetMostRecentStatusUpdates()
        {
            // get a configured DbCommand object
            DbCommand comm = DbAct.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "up_GetMostRecentStatusUpdates";

            // execute the stored procedure
            DataTable dt = DbAct.ExecuteSelectCommand(comm);

            // was something returned?
            if (dt != null && dt.Rows.Count > 0)
            {
                StatusUpdate art = null;
                foreach (DataRow dr in dt.Rows)
                {
                    art = new StatusUpdate(dr);
                    this.Add(art);
                }
            }

        }

        public void GetRecentStatusUpdates()
        {
            // get a configured DbCommand object
            DbCommand comm = DbAct.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "up_GetRecentStatusUpdates";

            // execute the stored procedure
            DataTable dt = DbAct.ExecuteSelectCommand(comm);

            // was something returned?
            if (dt != null && dt.Rows.Count > 0)
            {
                StatusUpdate art = null;
                foreach (DataRow dr in dt.Rows)
                {
                    art = new StatusUpdate(dr);
                    this.Add(art);
                }
            }

        }


        private bool _includeStartAndEndTags = true;

        public bool IncludeStartAndEndTags
        {
            get { return _includeStartAndEndTags; }
            set { _includeStartAndEndTags = value; }
        }


        public string ToUnorderdList
        {
            get
            {
                if (this.Count == 0) return string.Empty;

                StringBuilder sb = new StringBuilder(100);

                if (IncludeStartAndEndTags) sb.Append(@"<ul>");


                foreach (StatusUpdate su in this)
                {

                    sb.Append(su.ToUnorderdListItem);


                }

                if (IncludeStartAndEndTags) sb.Append(@"</ul>");

                return sb.ToString();
            }
        }



    }
}



