using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace UploadFile
{
    public partial class UploadFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Request.HttpMethod == "POST" 
                    && (Request.InputStream.Length > 0))
                && !(Request.HttpMethod == "GET"
                    && Request["DocumentId"] != null))
            {
                throw new NotSupportedException("A File or File Id must be provided.");
            }

            Document doc = new Document();

            LocalTestSpaceEntities context = new LocalTestSpaceEntities();
            if (Request.HttpMethod == "GET")
            {
                int docId = int.Parse(Request["DocumentId"].ToString());
                Response.BinaryWrite((context.Documents.Where(x => x.Id == docId).SingleOrDefault().Document1));
            }
            else
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Request.InputStream.CopyTo(ms);
                    doc.Document1 = ms.ToArray();
                }

                
                context.Documents.Add(doc);
                context.SaveChanges();
        
                
                Response.Write(doc.Id);
            }
        }
    }
}