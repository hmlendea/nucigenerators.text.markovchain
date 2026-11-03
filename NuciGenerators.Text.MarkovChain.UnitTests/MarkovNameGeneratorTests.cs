using System.Collections.Generic;

using NuciGenerators.Text.MarkovChain;
using NuciGenerators.Text.Models;

using NUnit.Framework;

namespace NuciGenerators.Text.MarkovChain.UnitTests
{
    public sealed class MarkovNameGeneratorTests
    {
        private static readonly List<Word> trainingWords =
        [
            new Word { Id = "bobert", Values = ["Bobert"] }
        ];

        [Test]
        public void GivenTrainingWords_WhenConstructed_ThenConfigurationAndDataAreRetained()
        {
            MarkovNameGenerator generator = new(trainingWords, 1, 0.5f);

            Assert.That(generator.Order, Is.EqualTo(1));
            Assert.That(generator.Prior, Is.EqualTo(0.5f));
            Assert.That(generator.Data, Has.Count.EqualTo(1));
            Assert.That(generator.Data[0].Values, Is.EqualTo(trainingWords[0].Values));
        }

        [Test]
        public void GivenAUniqueTrainingWordAndNoPrior_WhenGenerating_ThenTheTrainingWordIsReproduced()
            => Assert.That(
                new MarkovNameGenerator(trainingWords, 1, 0).Generate(),
                Is.EqualTo("#Bobert"));

        [TestCase("#", "B")]
        [TestCase("t", "#")]
        public void GivenAUniqueTrainingWordAndNoPrior_WhenGettingALetter_ThenTheExpectedTransitionIsReturned(
            string context,
            string expectedLetter)
            => Assert.That(
                new MarkovNameGenerator(trainingWords, 1, 0).GetLetter(context),
                Is.EqualTo(expectedLetter));
    }
}
