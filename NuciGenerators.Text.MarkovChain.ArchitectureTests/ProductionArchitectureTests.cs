using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

using ArchUnitNet;

using NUnit.Framework;

namespace NuciGenerators.Text.MarkovChain.ArchitectureTests
{
    [TestFixture]
    public sealed class ProductionArchitectureTests
    {
        [Test]
        public void GivenProductionAssembly_WhenTypesAreLoaded_ThenTheProductionAssemblyIsPresent()
            => Assert.That(
                typeof(MarkovNameGenerator).Assembly.GetName().Name,
                Is.EqualTo("NuciGenerators.Text.MarkovChain"));

        [Test]
        public void GivenProductionAssembly_WhenTestReferencesAreInspected_ThenNoTestAssemblyIsReferenced()
            => Assert.That(
                typeof(MarkovNameGenerator).Assembly.GetReferencedAssemblies()
                    .Any(assembly => assembly.Name is not null && assembly.Name.Contains("Tests", StringComparison.Ordinal)),
                Is.False);

        [Test]
        public void GivenProductionTypes_WhenMarkovNameGeneratorIsInspected_ThenItIsPublic()
            => Assert.That(typeof(MarkovNameGenerator).IsPublic, Is.True);

        [Test]
        public void GivenProductionTypes_WhenMarkovModelIsInspected_ThenItIsInTheModelsNamespace()
            => Assert.That(
                typeof(Models.MarkovModel).Namespace,
                Is.EqualTo("NuciGenerators.Text.MarkovChain.Models"));

        [Test]
        public void GivenProductionTypes_WhenNamespacesAreInspected_ThenThereAreNoInterfaceNamespaces()
            => Assert.That(
                typeof(MarkovNameGenerator).Assembly.GetTypes()
                    .Any(type => type.Namespace is not null && type.Namespace.Contains(".Interfaces", StringComparison.Ordinal)),
                Is.False);

        [Test]
        public void GivenProductionTypes_WhenNamespacesAreInspected_ThenThereAreNoEnumerationNamespaces()
            => Assert.That(
                typeof(MarkovNameGenerator).Assembly.GetTypes()
                    .Any(type => type.Namespace is not null && type.Namespace.Contains(".Enums", StringComparison.Ordinal)),
                Is.False);

        [Test]
        public void GivenProductionAssembly_WhenInternalsVisibleToIsInspected_ThenNoTestFriendAssemblyIsDeclared()
            => Assert.That(
                typeof(MarkovNameGenerator).Assembly.GetCustomAttributes<InternalsVisibleToAttribute>()
                    .Any(attribute => attribute.AssemblyName.Contains("Tests", StringComparison.Ordinal)),
                Is.False);

        [Test]
        public void GivenMarkovNameGenerator_WhenPublicApiIsInspected_ThenItsExpectedMembersExist()
        {
            List<string> memberNames = typeof(MarkovNameGenerator).GetMembers(BindingFlags.Public | BindingFlags.Instance)
                .Select(member => member.Name)
                .Distinct(StringComparer.Ordinal)
                .ToList();

            Assert.That(memberNames, Does.Contain("Order"));
            Assert.That(memberNames, Does.Contain("Prior"));
            Assert.That(memberNames, Does.Contain("Data"));
            Assert.That(memberNames, Does.Contain("Generate"));
            Assert.That(memberNames, Does.Contain("GetLetter"));
        }

        [Test]
        public void GivenMarkovModel_WhenPublicApiIsInspected_ThenGenerateIsAvailable()
            => Assert.That(
                typeof(Models.MarkovModel).GetMethod("Generate", BindingFlags.Public | BindingFlags.Instance),
                Is.Not.Null);

        [Test]
        public void GivenProductionProject_WhenArchUnitLoadsIt_ThenTheProjectGraphIsAvailable()
            => Assert.That(
                ArchUnit.ProjectGraph().GetType(),
                Is.Not.Null);
    }
}
