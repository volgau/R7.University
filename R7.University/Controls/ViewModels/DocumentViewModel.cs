﻿//
// DocumentViewModel.cs
//
// Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
// Copyright (c) 2015 
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DotNetNuke.Services.Localization;
using R7.University.Utilities;

namespace R7.University.Controls
{
    [Serializable]
    public class DocumentViewModel: DocumentInfo
    {
        public int ViewItemID { get; set; }

        [XmlIgnore]
        protected ViewModelContext Context { get; set; }

        [XmlIgnore]
        public string LocalizedType
        { 
            get
            {
                if (DocumentType != null)
                {
                    var localizedType = Localization.GetString ("SystemDocumentType_" + DocumentType.Type + ".Text", 
                        Context.LocalResourceFile);
                    
                    return (!string.IsNullOrEmpty (localizedType)) ? localizedType : DocumentType.Type;
                }

                return string.Empty;
            }
        }

        [XmlIgnore]
        public string FormattedUrl
        {
            get
            { 
                if (!string.IsNullOrWhiteSpace (Url))
                {
                    return string.Format ("<a href=\"{0}\" target=\"_blank\">{1}</a>",
                        UrlUtils.LinkClickIdnHack (Url, Context.Module.TabId, Context.Module.ModuleId),
                        Localization.GetString ("DocumentUrlLabel.Text", Context.LocalResourceFile)
                    );
                }

                return string.Empty;
            }
        }

        public DocumentViewModel ()
        {
            ViewItemID = ViewNumerator.GetNextItemID ();
        }

        public DocumentViewModel (DocumentInfo document, ViewModelContext viewContext): this ()
        {
            CopyCstor.Copy<DocumentInfo> (document, this);
            Context = viewContext;
        }

        public static void BindToView (IEnumerable<DocumentViewModel> documents, ViewModelContext context)
        {
            foreach (var document in documents)
            {
                document.Context = context;
            }
        }

        public DocumentInfo NewDocumentInfo ()
        {
            return new DocumentInfo {
                DocumentID = DocumentID,
                DocumentTypeID = DocumentTypeID,
                ItemID = ItemID,
                Title = Title,
                Url = Url,
                SortIndex = SortIndex,
                StartDate = StartDate,
                EndDate = EndDate
            };
        }
    }
}

