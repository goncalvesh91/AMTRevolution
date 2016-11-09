using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace AppCore.FileWriter
{
    class userLogsWriter
    {
        /// <summary>
        /// Class to write/read all user logs
        /// </summary>
        private string userLocalLogsPath {get;set;}
        public userLogsWriter(string path)
        {
        	userLocalLogsPath = path;
        }
        

        // User logs structs
        // Remedy template struct
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        [Serializable]
        public struct remedyTroubleshootTemplate
        {
        	// User data
        	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string userName;
        	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string machineName;
            // Template data
        	public int siteId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string siteOwner;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 500)]
            public string siteAddress;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string siteCctRef;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string sitepowerComp;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string remedyInc;
            public bool otherSitesimpacted;
            public bool coos;
            // coos data
            public bool fullSiteOutage; 
            public int coos2G;
            public int coos3G;
            public int coos4G;
            //
            public bool performanceIssue;     
			public bool intermittentIssue;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string relatedIncCrq;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1000)]
            public string activeAlarms;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1000)]
            public string alarmHistory;            
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1000)]
            public string troubleshoot;  
            
            public remedyTroubleshootTemplate(string _userName, string _machineName, int _siteId, string _siteOwner,
                                              string _siteAddress, string _siteCctRef, string _sitepowerComp,
                                              string _remedyInc, bool _otherSitesimpacted, bool _coos, bool _fullSiteOutage,
                                              int _coos2G, int _coos3G, int _coos4G, bool _performanceIssue, bool _intermittentIssue,
                                              string _relatedIncCrq, string _activeAlarms, string _alarmHistory, string _troubleshoot)
            {
                this.userName = _userName;
                this.machineName = _machineName;
                this.siteId = _siteId;
                this.siteOwner = _siteOwner;
                this.siteAddress = _siteAddress;
                this.siteCctRef = _siteCctRef;
                this.sitepowerComp = _sitepowerComp;
                this.remedyInc = _remedyInc;
                this.otherSitesimpacted = _otherSitesimpacted;
                this.coos = _coos;
                this.fullSiteOutage = _fullSiteOutage;
                this.coos2G = _coos2G;
                this.coos3G = _coos3G;
                this.coos4G = _coos4G;
                this.performanceIssue = _performanceIssue;
                this.intermittentIssue = _intermittentIssue;
                this.relatedIncCrq = _relatedIncCrq;
                this.activeAlarms = _activeAlarms;
                this.alarmHistory = _alarmHistory;
                this.troubleshoot = _troubleshoot;
            }
        }
        
    }
}
