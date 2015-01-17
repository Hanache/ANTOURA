using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Configuration;

namespace EMSTemplate.Areas.EMS.Helpers
{
    public static class MyHelpers
    {
        public static MvcHtmlString UploaderFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> attributes = null)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string propertyName = data.PropertyName;
            var Url = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            string controller = htmlHelper.ViewContext.RouteData.Values["controller"].ToString().ToLower();

            #region Default Attributes
            string mainFilesPath = ConfigurationManager.AppSettings["MainFilesPath"].ToString();
            string uploaderDefaultText = ConfigurationManager.AppSettings["UploaderDefaultText"].ToString();
            string uploaderDefaultNote = ConfigurationManager.AppSettings["UploaderDefaultNote"].ToString();
            string uploadTemplateId = ConfigurationManager.AppSettings["UploadTemplateId"].ToString();
            string downloadTemplateId = ConfigurationManager.AppSettings["DownloadTemplateId"].ToString();

            string directory = (ConfigurationManager.AppSettings[controller+"Directory"]??controller).ToString();
            string allowedExtensions = (ConfigurationManager.AppSettings[controller + "AllowedExtensions"] ?? ConfigurationManager.AppSettings["AllowedExtensions"]).ToString();
            int maxFileSize = Convert.ToInt32(ConfigurationManager.AppSettings[controller + "MaxFileSize"] ?? ConfigurationManager.AppSettings["MaxFileSize"]);
            string maxNumberOfFiles = (ConfigurationManager.AppSettings[controller + "MaxNumberOfFiles"] ?? ConfigurationManager.AppSettings["MaxNumberOfFiles"]).ToString();
            #endregion

            #region Attributes
            if (attributes != null && attributes.Any())
            {
                foreach (var item in attributes)
                {
                    if (item.Key.ToLower() == "uploadtemplateid")
                    {
                        uploadTemplateId = item.Value.ToString();
                    }
                    if (item.Key.ToLower() == "downloadtemplateid")
                    {
                        downloadTemplateId = item.Value.ToString();
                    }
                    if (item.Key.ToLower() == "uploaderdefaultnote")
                    {
                        uploaderDefaultText = item.Value.ToString();
                    }
                    if (item.Key.ToLower() == "uploaderdefaulttext")
                    {
                        uploaderDefaultNote = item.Value.ToString();
                    }
                    if (item.Key.ToLower() == "mainfilespath")
                    {
                        mainFilesPath = item.Value.ToString();
                    }
                    if (item.Key.ToLower() == "directory")
                    {
                        directory = item.Value.ToString();
                    }
                    if (item.Key.ToLower() == "allowedextensions")
                    {
                        allowedExtensions = item.Value.ToString();
                    }
                    if (item.Key.ToLower() == "maxfilesize")
                    {
                        maxFileSize = Convert.ToInt32(item.Value.ToString());
                    }
                    if (item.Key.ToLower() == "maxnumberoffiles")
                    {
                        maxNumberOfFiles = item.Value.ToString();
                    }
                }
            }
            #endregion

            MvcHtmlString result = new MvcHtmlString("<div  id='fileupload_" + propertyName + @"' class='row fileupload-buttonbar'>
                    <div class='col-lg-9'>
                        <span class='btn btn-success fileinput-button green'>
                            <i class='glyphicon glyphicon-plus'></i>
                            <span>"+uploaderDefaultText+@"</span>
                            <input type='file' multiple/>
                            <input type='hidden' id='" + propertyName + "' name='" + propertyName + "' value='" + (data.Model) + @"'/>
                        </span>" + uploaderDefaultNote + @"                        
                    </div>
                    <table role='presentation' class='table table-striped'><tbody class='files'></tbody></table>
                </div>
                <script>
                 $(function(){
                    'use strict'; 
                   $('#fileupload_" + propertyName + @"').fileupload({
                    uploadTemplateId: '" +uploadTemplateId+@"',
                    downloadTemplateId: '" + downloadTemplateId + @"',
                    autoUpload: true,
                    maxNumberOfFiles: " + maxNumberOfFiles + @",
                    maxFileSize: " + maxFileSize + @",
                    acceptFileTypes: /(\.|\/)(" + allowedExtensions + @")$/i,
                    url: '" + Url.Content("~/ems/UploadHandler/UploadFiles?inputname=" + propertyName + "&directory=" + mainFilesPath + directory)+ @"'
                        });
                    
                
        var files = [
    {
        'name': 'fileName.jpg',
        'size': 775702,
        'type': 'image/jpeg',
        'url': 'http://mydomain.com/files/fileName.jpg',
        'deleteUrl': 'http://mydomain.com/files/fileName.jpg',
        'deleteType': 'DELETE'
    },
    {
        'name': 'file2.jpg',
        'size': 68222,
        'type': 'image/jpeg',
        'url': 'http://mydomain.com/files/file2.jpg',
        'deleteUrl': 'http://mydomain.com/files/file2.jpg',
        'deleteType': 'DELETE'
    }];
        $('#fileupload_" + propertyName + @"').fileupload('option', 'done').call(this, $.Event('done'), { result: {files: files} });
});
</script>
");


            return result;
        }

        public static MvcHtmlString TextEditor(this HtmlHelper htmlHelper, string name, object value, IDictionary<string, object> htmlAttributes = null)
        {
            return MvcHtmlString.Create(htmlHelper.Kendo().Editor()
                      .Name(name)
                      .HtmlAttributes(new { style = "width: 563px;height:200px" })
                      .Tools(tools => tools
                          .InsertFile()
                          .SubScript()
                          .SuperScript()
                          .ViewHtml()
                          .FontName()
                          .FontSize()
                          .FontColor().BackColor()
                      )
                      .Value(value.ToString())
                      .ImageBrowser(imageBrowser => imageBrowser
                        .Image("~/Content/Uploads/Editor/{0}")
                        .Read("Read", "Editor")
                        .Create("Create", "Editor")
                        .Destroy("Destroy", "Editor")
                        .Upload("Upload", "Editor")
                        .Thumbnail("Thumbnail", "Editor"))
                     .FileBrowser(fileBrowser => fileBrowser
                         .File("~/Content/Uploads/Editor/{0}")
                         .Read("Read", "Editor")
                         .Create("Create", "Editor")
                         .Destroy("Destroy", "Editor")
                         .Upload("Upload", "Editor")
                      ).ToString());
        }

        public static MvcHtmlString TextEditorFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes = null)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string propertyName = data.PropertyName;
            return MvcHtmlString.Create(htmlHelper.Kendo().Editor()
                      .Name(propertyName)
                      .HtmlAttributes(new { style = "width: 563px;height:200px" })
                      .Tools(tools => tools
                          .InsertFile()
                          .SubScript()
                          .SuperScript()
                          .ViewHtml()
                          .FontName()
                          .FontSize()
                          .FontColor()
                          .BackColor()
                      )
                      .Value(HttpUtility.HtmlDecode(data.Model + ""))
                      .ImageBrowser(imageBrowser => imageBrowser
                        .Image("~/Content/Uploads/Editor/{0}")
                        .Read("Read", "Editor")
                        .Create("Create", "Editor")
                        .Destroy("Destroy", "Editor")
                        .Upload("Upload", "Editor")
                        .Thumbnail("Thumbnail", "Editor"))
                     .FileBrowser(fileBrowser => fileBrowser
                         .File("~/Content/Uploads/Editor/{0}")
                         .Read("Read", "Editor")
                         .Create("Create", "Editor")
                         .Destroy("Destroy", "Editor")
                         .Upload("Upload", "Editor")
                      ).ToString());
        }
    }
}
