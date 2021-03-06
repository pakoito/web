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
using System.Configuration;

namespace BootBaronLib.Configs
{
    public class GeneralConfigs
    {
        static GeneralConfigs()
        {
            // other

            _mailServer = ConfigurationManager.AppSettings["MailServer"];

            _enableErrorLogEmail = bool.Parse(ConfigurationManager.AppSettings["EnableErrorLogEmail"]);

            _useNetworkForMail = bool.Parse(ConfigurationManager.AppSettings["UseNetworkForMail"]);

            _postInterval = int.Parse(ConfigurationManager.AppSettings["PostInterval"]);
            _siteDomain = ConfigurationManager.AppSettings["SiteDomain"];

            _payPalPDTKey = ConfigurationManager.AppSettings["PayPalPDTKey"];
            _payPalURL = ConfigurationManager.AppSettings["PayPalURL"];

            _emailSettingsURL = ConfigurationManager.AppSettings["EmailSettingsURL"];

            _sendToErrorEmail = ConfigurationManager.AppSettings["SendToErrorEmail"];

            _googleAPIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];


            _photoToEmail = ConfigurationManager.AppSettings["PhotoToEmail"];
            _photoToPassword = ConfigurationManager.AppSettings["PhotoToPassword"];

            _defaultLanguage = ConfigurationManager.AppSettings["DefaultLanguage"];


            _siteName = ConfigurationManager.AppSettings["SiteName"];


            _facebookLink = ConfigurationManager.AppSettings["FacebookLink"];
            _twitterLink = ConfigurationManager.AppSettings["TwitterLink"];
            _youtubeLink = ConfigurationManager.AppSettings["YoutubeLink"];

            _defaultVideo = ConfigurationManager.AppSettings["DefaultVideo"];

            _enableVideoCheck = bool.Parse(ConfigurationManager.AppSettings["EnableVideoCheck"]);

            _photoMailPort = int.Parse(ConfigurationManager.AppSettings["PhotoMailPort"]);

            _minimumAge = int.Parse(ConfigurationManager.AppSettings["MinimumAge"]);
            _userChatRoomSessionTimeout = int.Parse(ConfigurationManager.AppSettings["UserChatRoomSessionTimeout"]);



            _randomColors = ConfigurationManager.AppSettings["RandomColors"];

            _adminUserName = ConfigurationManager.AppSettings["AdminUserName"];

            _isGiveAway = bool.Parse(ConfigurationManager.AppSettings["IsGiveAway"]);

            _enableSameIP = bool.Parse(ConfigurationManager.AppSettings["EnableSameIP"]);
        

            _youTubeDevKey = ConfigurationManager.AppSettings["YouTubeDevKey"];
            _youTubeDevUser = ConfigurationManager.AppSettings["YouTubeDevUser"];
            _youTubeDevPass = ConfigurationManager.AppSettings["YouTubeDevPass"];

        }

        #region readonly variables 


        private readonly static bool _isGiveAway = false;

     

        private readonly static int _userChatRoomSessionTimeout = 0;




        private readonly static bool _enableSameIP = false;



        private readonly static string _adminUserName = string.Empty;

     
        private readonly static int _minimumAge = 0;

        private readonly static string _defaultVideo = string.Empty;


        private readonly static string _siteName = string.Empty;



        private readonly static string _googleAPIKey = string.Empty;


        private readonly static string _photoToEmail = string.Empty;

 

        
        private readonly static string _nonWebFilePath = string.Empty;
        private readonly static string _mailServer = string.Empty;
        private readonly static bool _enableErrorLogEmail = false;
        private readonly static string _errorLogFilename = string.Empty;
 
 
        private readonly static bool _useNetworkForMail = false;
  
        private readonly static int _postInterval = 0;

       

        private readonly static string _siteDomain = string.Empty;
        private readonly static string _payPalPDTKey = string.Empty;
        private readonly static string _payPalURL = string.Empty;
 
        private readonly static string _emailSettingsURL = string.Empty;

        private readonly static string _sendToErrorEmail = string.Empty;


        private readonly static string _defaultLanguage = string.Empty;


        private readonly static bool _enableVideoCheck = false;


        private readonly static int _photoMailPort = 995;



        private readonly static string _facebookLink = string.Empty;
        private readonly static string _twitterLink = string.Empty;
        private readonly static string _youtubeLink = string.Empty;

        private readonly static string _randomColors = string.Empty;


        private readonly static string _youTubeDevKey = string.Empty;



        private readonly static string _youTubeDevUser = string.Empty;



        private readonly static string _youTubeDevPass = string.Empty;

        #endregion





        #region properties


        public static bool EnableSameIP
        {
            get { return GeneralConfigs._enableSameIP; }
           
        }


        public static bool IsGiveAway
        {
            get { return GeneralConfigs._isGiveAway; }
        } 

 
 

        public static int UserChatRoomSessionTimeout
        {
            get { return GeneralConfigs._userChatRoomSessionTimeout; }
        } 

        public static string AdminUserName
        {
            get { return GeneralConfigs._adminUserName; }
        } 



        public static string RandomColors
        {
            get { return GeneralConfigs._randomColors; }
        } 



        public static int MinimumAge
        {
            get { return GeneralConfigs._minimumAge; }
        } 


        public static string FacebookLink
        {
            get { return GeneralConfigs._facebookLink; }
        }

        public static string TwitterLink
        {
            get { return GeneralConfigs._twitterLink; }
        }

        public static string YoutubeLink
        {
            get { return GeneralConfigs._youtubeLink; }
        } 

        public static int PhotoMailPort
        {
            get { return GeneralConfigs._photoMailPort; }
        }

        public static string DefaultVideo
        {
            get { return GeneralConfigs._defaultVideo; }
        } 


        public static int PostInterval
        {
            get { return GeneralConfigs._postInterval; }
        } 

        public static bool EnableVideoCheck
        {
            get { return GeneralConfigs._enableVideoCheck; }
        } 


        public static string SiteName
        {
            get { return GeneralConfigs._siteName; }
        } 


        public static string DefaultLanguage
        {
            get { return GeneralConfigs._defaultLanguage; }
        } 

        public static string PhotoToEmail
        {
            get { return GeneralConfigs._photoToEmail; }
        }

        private readonly static string _photoToPassword = string.Empty;

        public static string PhotoToPassword
        {
            get { return GeneralConfigs._photoToPassword; }
        } 



        public static string GoogleAPIKey
        {
            get { return GeneralConfigs._googleAPIKey; }
        } 



        public static string SendToErrorEmail
        {
            get { return GeneralConfigs._sendToErrorEmail; }
        } 


        public static string EmailSettingsURL
        {
            get { return GeneralConfigs._emailSettingsURL; }
        } 

        

        public static string PayPalURL
        {
            get { return GeneralConfigs._payPalURL; }
        } 


        public static string PayPalPDTKey
        {
            get { return GeneralConfigs._payPalPDTKey; }
        } 


      

 

        public static string SiteDomain
        {
            get
            {
                return GeneralConfigs._siteDomain;
            }
        } 
 

      
 
 


        public static bool UseNetworkForMail
        {
            get { return GeneralConfigs._useNetworkForMail; }
        } 


 
       
 

        public static string ErrorLogFilename
        {
            get { return _errorLogFilename; }
        }


        public static bool EnableErrorLogEmail
        {
            get { return _enableErrorLogEmail; }
        }
        public static string MailServer
        {
            get { return _mailServer; }
        }
        public static string NonWebFilePath
        {
            get { return _nonWebFilePath; }
        }

        public static string YouTubeDevKey
        {
            get { return GeneralConfigs._youTubeDevKey; }
        }
        public static string YouTubeDevUser
        {
            get { return GeneralConfigs._youTubeDevUser; }
        }
        public static string YouTubeDevPass
        {
            get { return GeneralConfigs._youTubeDevPass; }
        } 
 
        #endregion






    }
}