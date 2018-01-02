﻿#region License
/* Copyright (c) 2017 Wes Hampson
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using WHampson.Cascara.Interpreter;
using WHampson.Cascara.Interpreter.Xml;
using static WHampson.Cascara.Interpreter.ReservedWords;

namespace WHampson.Cascara
{
    public abstract partial class BinaryLayout
    {
        internal sealed class XmlBinaryLayout : BinaryLayout
        {
            private const string VersionAttrName = "version";

            public static XmlBinaryLayout LoadSource(string path)
            {
                // Load XML document
                XDocument doc;
                try
                {
                    doc = XDocument.Load(path, LoadOptions.SetLineInfo);
                }
                catch (Exception e)
                {
                    if (!(e is IOException) && !(e is XmlException))
                    {
                        throw;
                    }

                    throw LayoutException.Create<LayoutException>(null, e, null, Resources.LayoutExceptionLoadFailure);
                }

                // Create and return layout
                return CreateXmlBinaryLayout(doc, path);
            }

            public static XmlBinaryLayout ParseSource(string source)
            {
                // Parse XML document string
                XDocument doc;
                try
                {
                    doc = XDocument.Parse(source, LoadOptions.SetLineInfo);
                }
                catch (XmlException e)
                {
                    throw LayoutException.Create<LayoutException>(null, e, null, Resources.LayoutExceptionLoadFailure);
                }

                // Create and return layout
                return CreateXmlBinaryLayout(doc, null);
            }

            private static XmlBinaryLayout CreateXmlBinaryLayout(XDocument doc, string sourcePath)
            {
                // Read name; ensure it's present
                string name = GetLayoutName(doc);
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw LayoutException.Create<LayoutException>(null, null, Resources.SyntaxExceptionMissingLayoutName);
                }

                // Read version
                Version ver = GetLayoutVersion(doc);

                // Create and return layout object
                return new XmlBinaryLayout(doc, name, ver, sourcePath);
            }

            private static string GetLayoutName(XDocument doc)
            {
                XAttribute nameAttr = doc.Root.Attribute(ReservedWords.Parameters.Name);

                return (nameAttr == null)
                    ? null
                    : nameAttr.Value;
            }

            private static Version GetLayoutVersion(XDocument doc)
            {
                XAttribute versionAttr = doc.Root.Attribute(VersionAttrName);
                if (versionAttr == null)
                {
                    return AssemblyInfo.AssemblyVersion;
                }

                if (!Version.TryParse(versionAttr.Value, out Version ver))
                {
                    throw LayoutException.Create<LayoutException>(null, null, Resources.LayoutExceptionMalformattedLayoutVersion);
                }

                return ver;
            }

            private XmlBinaryLayout(XDocument document, string name, Version version, string sourcePath)
                : base(name, version, sourcePath)
            {
                Document = document;

                Initialize();
            }

            public XDocument Document
            {
                get;
            }

            protected override void Initialize()
            {
                try
                {
                    RootStatement = XmlStatement.Parse(Document.Root);
                }
                catch (LayoutException e)
                {
                    throw LayoutException.Create<LayoutException>(this, null, Resources.LayoutExceptionLoadFailure);
                }
            }
        }
    }
}