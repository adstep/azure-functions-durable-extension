﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebJobs.Extensions.DurableTask.CodeGeneration.SourceGenerator.Utils;

namespace WebJobs.Extensions.DurableTask.CodeGeneration.SourceGenerator.Generators
{
    public class IGeneratedDurableClientGenerator : WrapperInterfaceGenerator
    {
        public const string OrchestrationPropertyName = "Orchestration";

        private static readonly string[] requiredNamespaces = new[]
        {
            "Microsoft.Azure.WebJobs.Extensions.DurableTask"
        };

        protected override INamedTypeSymbol NamedTypeSymbol { get; }
        protected override string InterfaceName => Names.IGeneratedDurableClient;

        private IGeneratedDurableClientGenerator(INamedTypeSymbol symbol)
        {
            NamedTypeSymbol = symbol;
        }

        public static bool TryGenerate(INamedTypeSymbol namedTypeSymbol, out CompilationUnitSyntax compilationSyntax)
        {
            var generator = new IGeneratedDurableClientGenerator(namedTypeSymbol);
            compilationSyntax = generator.Generate();
            return true;
        }

        protected override SyntaxList<UsingDirectiveSyntax> GetAdditionalUsings()
        {
            return SyntaxFactory.List(
                requiredNamespaces.Select(AsUsing)
            );
        }

        protected override List<PropertyDeclarationSyntax> GetAdditionalProperties()
        {
            return new List<PropertyDeclarationSyntax>()
            {
                AsPropertyWithGetter(Names.IGeneratedDurableOrchestrationStarter, OrchestrationPropertyName)
            };
        }
    }
}
