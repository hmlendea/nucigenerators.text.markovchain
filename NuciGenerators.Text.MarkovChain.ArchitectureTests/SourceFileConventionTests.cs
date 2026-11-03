using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using NUnit.Framework;

namespace NuciGenerators.Text.MarkovChain.ArchitectureTests
{
    [TestFixture]
    public sealed class SourceFileConventionTests
    {
        private static readonly string[] productionSourceFiles =
        [
            "MarkovNameGenerator.cs",
            Path.Combine("Models", "MarkovModel.cs")
        ];

        [Test]
        public void GivenProductionSources_WhenConventionInputsAreResolved_ThenAllExpectedFilesExist()
        {
            string sourceDirectory = ProductionSourceDirectory();

            Assert.That(sourceDirectory, Is.Not.Null.And.Not.Empty);
            Assert.That(
                productionSourceFiles.All(file => File.Exists(Path.Combine(sourceDirectory, file))),
                Is.True);
        }

        [Test]
        public void GivenProductionSources_WhenDeclarationsAreCounted_ThenEachFileDeclaresOneType()
        {
            string sourceDirectory = ProductionSourceDirectory();

            foreach (string relativePath in productionSourceFiles)
            {
                string path = Path.Combine(sourceDirectory, relativePath);
                SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(path), path: path);
                int declaredTypeCount = syntaxTree.GetRoot().DescendantNodes().Count(IsTypeDeclaration);

                Assert.That(
                    declaredTypeCount,
                    Is.EqualTo(1),
                    $"{relativePath} must declare exactly one type.");
            }
        }

        [Test]
        public void GivenProductionSources_WhenTypeNamesAreCompared_ThenFileStemsMatchDeclaredTypes()
        {
            string sourceDirectory = ProductionSourceDirectory();

            foreach (string relativePath in productionSourceFiles)
            {
                string path = Path.Combine(sourceDirectory, relativePath);
                SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(path), path: path);
                string declaredTypeName = syntaxTree.GetRoot().DescendantNodes()
                    .OfType<BaseTypeDeclarationSyntax>()
                    .Single()
                    .Identifier
                    .ValueText;

                Assert.That(
                    declaredTypeName,
                    Is.EqualTo(Path.GetFileNameWithoutExtension(path)),
                    $"{relativePath} must match its declared type name.");
            }
        }

        [Test]
        public void GivenProductionSources_WhenGeneratedDirectoriesAreConsidered_ThenTheyAreNotConventionInputs()
            => Assert.That(
                productionSourceFiles.Any(file => file.Contains("bin", StringComparison.Ordinal) || file.Contains("obj", StringComparison.Ordinal)),
                Is.False);

        private static string ProductionSourceDirectory()
            => Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "NuciGenerators.Text.MarkovChain"));

        private static bool IsTypeDeclaration(SyntaxNode node)
            => node is BaseTypeDeclarationSyntax || node is DelegateDeclarationSyntax;
    }
}
