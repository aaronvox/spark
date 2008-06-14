/*
   Copyright 2008 Louis DeJardin - http://whereslou.com

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System.Collections.Generic;

namespace Spark.Compiler
{
    public class Chunk
    {

    }

    public class SendLiteralChunk : Chunk
    {
        public string Text { get; set; }
    }

    public class SendExpressionChunk : Chunk
    {
        public string Code { get; set; }
    }

    public class GlobalVariableChunk : Chunk
    {
        public GlobalVariableChunk()
        {
            Type = "object";
        }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class LocalVariableChunk : Chunk
    {
        public LocalVariableChunk()
        {
            Type = "var";
        }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class ViewDataChunk : Chunk
    {
        public ViewDataChunk()
        {
            Type = "object";
        }
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class ViewDataModelChunk : Chunk
    {
        public string TModel { get; set; }
    }

    public class AssignVariableChunk : Chunk
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class UseContentChunk : Chunk
    {
        public UseContentChunk()
        {
            Default = new List<Chunk>();
        }
        public string Name { get; set; }
        public IList<Chunk> Default { get; set; }
    }

    public class RenderPartialChunk : Chunk
    {
        public string Name { get; set; }

        public FileContext FileContext { get; set; }
    }

    public class UseNamespaceChunk : Chunk
    {
        public string Namespace { get; set; }
    }

    public class ContentChunk : Chunk
    {
        public ContentChunk()
        {
            Body = new List<Chunk>();
        }
        public string Name { get; set; }
        public IList<Chunk> Body { get; set; }
    }

    public class ForEachChunk : Chunk
    {
        public ForEachChunk()
        {
            Body = new List<Chunk>();
        }
        public string Code { get; set; }
        public IList<Chunk> Body { get; set; }
    }

    public class ScopeChunk : Chunk
    {
        public ScopeChunk()
        {
            Body = new List<Chunk>();
        }
        public IList<Chunk> Body { get; set; }
    }

    public class ConditionalChunk : Chunk
    {
        public ConditionalChunk()
        {
            Body = new List<Chunk>();
        }

        public ConditionalType Type { get; set; }
        public string Condition { get; set; }
        public IList<Chunk> Body { get; set; }
    }

    public enum ConditionalType
    {
        If,
        Else,
        ElseIf,
    }

    public class ExtensionChunk : Chunk
    {
        public ExtensionChunk()
        {
            Body = new List<Chunk>();
        }

        public ISparkExtension Extension { get; set; }
        public IList<Chunk> Body { get; set; }
    }
}