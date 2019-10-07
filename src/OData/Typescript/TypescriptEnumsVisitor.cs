﻿using System.IO;
using System.Linq;
using SenseNet.ContentRepository.Schema.Metadata;

namespace SenseNet.OData.Typescript
{
    internal class TypescriptEnumsVisitor : TypescriptModuleWriter
    {
        public TypescriptEnumsVisitor(TypescriptGenerationContext context, TextWriter writer) : base(context, writer) { }

        protected override IMetaNode VisitSchema(ContentRepository.Schema.Metadata.Schema schema)
        {
            // do not call the base functionality

            #region Write filestart
            _writer.WriteLine(@"/**
 *
 * @module Enums
 *
 * @preferred
 *
 * @description Module for enums types defined in SenseNet helps you to use enums with dot notation.
 *
 * This module is autogenerated from Sense/Net metadata (/Odata/$metadata)
 *
 * ```
 * const car = new ContentTypes.Car({
 *  Id: 1,
 *  Name: 'MyCar',
 *  DisplayName: 'My Car',
 *  Style: Style.Cabrio
 * });
 * ```
 */
");
            #endregion

            foreach (var enumeration in Context.Enumerations)
                Visit(enumeration);

            return schema;
        }

        protected override IMetaNode VisitEnumeration(Enumeration enumeration)
        {
            // do not call base functionality in this method

            var options = enumeration.Options.Select(o => $"{o.Name} = '{o.Value}'").ToArray();

            var names = Context.EmittedEnumerationNames
                .Where(x => x.Value == enumeration.Key)
                .Select(x => x.Key)
                .ToArray();

            foreach (var name in names)
            {
                WriteLine($"export enum {name} {{");
                _indentCount++;
                foreach (var option in options)
                {
                    WriteLine($"{option},");
                }
                _indentCount--;
                WriteLine($"}}");
            }

            return enumeration;
        }
    }
}