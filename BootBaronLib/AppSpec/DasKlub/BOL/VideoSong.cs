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
using System.Linq;
using System.Text;
using System.Data.Common;
using BootBaronLib.DAL;
using System.Data;
using BootBaronLib.Operational;

namespace BootBaronLib.AppSpec.DasKlub.BOL
{
    public class VideoSong
    {
        private int _videoID = 0;

        public int VideoID
        {
            get { return _videoID; }
            set { _videoID = value; }
        }

        private int _songID = 0;

        public int SongID
        {
            get { return _songID; }
            set { _songID = value; }
        }


        private int _rankOrder = 0;

        public int RankOrder
        {
            get { return _rankOrder; }
            set { _rankOrder = value; }
        }


        public static bool AddVideoSong(int songID, int videoID, int rankOrder)
        {
            // get a configured DbCommand object
            DbCommand comm = DbAct.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "up_AddVideoSong";

            ADOExtenstion.AddParameter(comm, "videoID", videoID);
            ADOExtenstion.AddParameter(comm, "songID",  songID);
            ADOExtenstion.AddParameter(comm, "rankOrder", rankOrder);

            // the result is their ID
            string result = string.Empty;
            // execute the stored procedure
            result = DbAct.ExecuteScalar(comm);

            return true;// this isn't really true


 

        }

        public static bool DeleteSongsForVideo(int videoID)
        {
              // get a configured DbCommand object
            DbCommand comm = DbAct.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "up_DeleteSongsForVideo";

            ADOExtenstion.AddParameter(comm, "videoID",   videoID);
           
            // execute the stored procedure
            int result = DbAct.ExecuteNonQuery(comm);

            return result > 0;



        }
  
    }
}
