﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SenseNet.ContentRepository;
using System.Web;
using Microsoft.AspNetCore.Http;
using SenseNet.ContentRepository.Storage;
using SenseNet.ContentRepository.Schema;
using SenseNet.ApplicationModel;
using SenseNet.Diagnostics;

namespace Compatibility.SenseNet.Portal.Handlers
{
    [ContentHandler]
    public class HttpHandlerApplication : Application
    {
        public HttpHandlerApplication(Node parent) : this(parent, null) { }
        public HttpHandlerApplication(Node parent, string nodeTypeName) : base(parent, nodeTypeName) { }
        protected HttpHandlerApplication(NodeToken nt) : base(nt) { }

        public bool IsReusable { get { return false; } }

        public void ProcessRequest(HttpContext context)
        {
            //var httpHandlerAction = this.CreateAction(Content.Create(PortalContext.Current.ContextNode), null, null) as IHttpHandler;
            //if (httpHandlerAction != null)
            //    httpHandlerAction.ProcessRequest(context);
            throw new NotImplementedException(); //UNDONE:ODATA: Not implemented: HttpHandlerApplication.ProcessRequest
        }
    }
}
