﻿using System;
using Spark.Parser.Markup;
using Spark.Parser;
using System.Collections.Generic;
using Spark.Compiler.NodeVisitors;
using System.Collections;

namespace SparkSense.Parsing
{
    public class SparkSyntax
    {
        public static IList<Node> ParseNodes(string content)
        {
            var grammar = new MarkupGrammar();
            var result = grammar.Nodes(Source(content));
            return result.Value;
        }

        public static Node ParseNode(string content, int position)
        {
            int start, end;
            GetElementStartAndEnd(content, position, out start, out end);
            if (PositionIsOutsideANode(position, start, end) || PositionIsInClosingElement(content, start))
                return null;

            string openingElement = GetOpeningElement(content, position, start);
            var nodes = ParseNodes(openingElement);

            if (nodes.Count > 1 && nodes[0] is TextNode)
            {
                var firstSpaceAfterStart = content.IndexOf(' ', start) - start;
                var elementWithoutAttributes = content.Substring(start, firstSpaceAfterStart) + "/>";
                nodes = ParseNodes(elementWithoutAttributes);
            }

            return (nodes[0]);
        }

        public static Type ParseContext(string content, int position)
        {
            var previousChar = content.ToCharArray()[position - 1];
            switch (previousChar)
            {
                case '<':
                    return typeof(ElementNode);
                case ' ':
                    return typeof(AttributeNode);
                default:
                    break;
            }
            return typeof(TextNode);
        }

        public static bool IsSparkNode(Node currentNode, out Node sparkNode)
        {
            var visitor = new SpecialNodeVisitor(new VisitorContext());
            visitor.Accept(currentNode);
            sparkNode = visitor.Nodes.Count > 0 ? visitor.Nodes[0] : null;
            return sparkNode != null && sparkNode is SpecialNode;
        }

        private static void GetElementStartAndEnd(string content, int position, out int start, out int end)
        {
            start = content.LastIndexOf('<', position > 0 ? position - 1 : 0);
            end = content.LastIndexOf('>', position > 0 ? position - 1 : 0);
        }
        private static string GetOpeningElement(string content, int position, int start)
        {
            var nextStart = content.IndexOf('<', position);

            var fullElement = nextStart != -1
                ? content.Substring(start, nextStart - start)
                : content.Substring(start);
            if (!fullElement.Contains(">")) fullElement += "/>";
            else if (!fullElement.Contains("/>")) fullElement = fullElement.Replace(">", "/>");
            return fullElement;
        }

        private static bool PositionIsOutsideANode(int position, int start, int end)
        {
            return (end > start && end < position) || position == 0;
        }

        private static bool PositionIsInClosingElement(string content, int start)
        {
            return content.ToCharArray()[start + 1] == '/';
        }

        private static Position Source(string content)
        {
            return new Position(new SourceContext(content));
        }

        private IList<INodeVisitor> BuildNodeVisitors(VisitorContext context)
        {
            return new INodeVisitor[]
                       {
                           //new NamespaceVisitor(context),
                           //new IncludeVisitor(context),
                           //new PrefixExpandingVisitor(context),
                           new SpecialNodeVisitor(context),
                           //new CacheAttributeVisitor(context),
                           //new ForEachAttributeVisitor(context),
                           //new ConditionalAttributeVisitor(context),
                           //new OmitExtraLinesVisitor(context),
                           //new TestElseElementVisitor(context),
                           //new OnceAttributeVisitor(context),
                           //new UrlAttributeVisitor(context),
                           //new BindingExpansionVisitor(context)
                       };
        }

    }
}