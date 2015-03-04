﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BEx {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BEx.ErrorMessages", typeof(ErrorMessages).Assembly);
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
        ///   Looks up a localized string similar to Executed command {0}, but the server replied that the request was bad (HTTP 400)..
        /// </summary>
        internal static string RESTBadRequest {
            get {
                return ResourceManager.GetString("RESTBadRequest", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Check the Inner Exception for more details..
        /// </summary>
        internal static string RESTCheckInnerException {
            get {
                return ResourceManager.GetString("RESTCheckInnerException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The response content contained the following: {0}..
        /// </summary>
        internal static string RESTErrorResponseContent {
            get {
                return ResourceManager.GetString("RESTErrorResponseContent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occured while executing the API Command {0}.  .
        /// </summary>
        internal static string RESTExecuteException {
            get {
                return ResourceManager.GetString("RESTExecuteException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Executed command {0}, but the server replied that the action is forbidden (HTTP 403)..
        /// </summary>
        internal static string RESTForbidden {
            get {
                return ResourceManager.GetString("RESTForbidden", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Executed command {0}, but the server had an internal error preventing execution (HTTP 500)..
        /// </summary>
        internal static string RESTInternalServerError {
            get {
                return ResourceManager.GetString("RESTInternalServerError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The path {0} does not exist on the server.  Executed Command {1} (HTTP 404)..
        /// </summary>
        internal static string RESTInvalidURL {
            get {
                return ResourceManager.GetString("RESTInvalidURL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Attempted to execute command {0} but the server responded that the method was not allowed (HTTP 405).  The method used was {1}..
        /// </summary>
        internal static string RESTMethodNotAllowed {
            get {
                return ResourceManager.GetString("RESTMethodNotAllowed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Attempted to execute command {0}, but the server replied that our request timed out (HTTP 408)..
        /// </summary>
        internal static string RESTRequestTimeout {
            get {
                return ResourceManager.GetString("RESTRequestTimeout", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Attempted to execute command {0}, but the server responded that the service was unavailable (HTTP 503)..
        /// </summary>
        internal static string RESTServiceUnavailable {
            get {
                return ResourceManager.GetString("RESTServiceUnavailable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Server returned status OK (HTTP 200) when executing command {0}, but an exception was raised on the response. .
        /// </summary>
        internal static string RESTSuccessButHasException {
            get {
                return ResourceManager.GetString("RESTSuccessButHasException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Executed command {0}, but the server replied that the request is unauthorized (HTTP 401).  Is your API Key/Secret correctly specified and active?.
        /// </summary>
        internal static string RESTUnauthorized {
            get {
                return ResourceManager.GetString("RESTUnauthorized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An HTTP status of {0} was returned, but is unhandled by the API..
        /// </summary>
        internal static string RESTUnhandledStatus {
            get {
                return ResourceManager.GetString("RESTUnhandledStatus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Attempted to execute command {0}, but the server replied that the URI was too long (HTTP 414)..
        /// </summary>
        internal static string RESTURITooLong {
            get {
                return ResourceManager.GetString("RESTURITooLong", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The currency pair {0}/{1} is not currently supported by the exchange {2}.
        /// </summary>
        internal static string UnsupportedCurrencyPair {
            get {
                return ResourceManager.GetString("UnsupportedCurrencyPair", resourceCulture);
            }
        }
    }
}
