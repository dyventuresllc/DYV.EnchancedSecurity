﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DYV.EnchancedSecurity.Agent.DataAccess.Queries {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SELECT {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SELECT() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DYV.EnchancedSecurity.Agent.DataAccess.Queries.SELECT", typeof(SELECT).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to     SELECT
        ///	    JobId,
        ///        JobName,
        ///        JobExecute_Time_Hour,
        ///        JobExecute_Time_Minute,
        ///        JobExecute_Interval,
        ///        JobLastExecute_DateTime
        ///    FROM EDDS.qe.QuinnJobQueue 
        ///    WHERE JobId = 1;.
        /// </summary>
        internal static string JobParams {
            get {
                return ResourceManager.GetString("JobParams", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT 
        ///	u.ArtifactID, u.FirstName, u.LastName, u.EmailAddress
        ///FROM EDDS.eddsdbo.[user] u with (NOLOCK)
        ///JOIN EDDS.eddsdbo.GroupUser gu WITH (NOLOCK) 
        ///	ON gu.UserArtifactID = u.ArtifactID
        ///JOIN EDDS.eddsdbo.[Group] g WITH (NOLOCK) 
        ///	ON g.ArtifactID = gu.GroupArtifactID
        ///JOIN
        ///(
        ///	SELECT gu.UserArtifactID, COUNT(gu.GroupArtifactID) &apos;Group Count&apos;
        ///	FROM EDDS.eddsdbo.GroupUser gu WITH (NOLOCK)
        ///	GROUP BY gu.UserArtifactID
        ///	HAVING COUNT(gu.GroupArtifactID) = 1
        ///) gc
        ///	ON gc.UserArtifactID = u.ArtifactID 
        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Users_WithNoGroups {
            get {
                return ResourceManager.GetString("Users_WithNoGroups", resourceCulture);
            }
        }
    }
}