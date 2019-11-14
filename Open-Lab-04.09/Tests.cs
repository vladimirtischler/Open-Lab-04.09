using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Open_Lab_04._09
{
    [TestFixture]
    public class Tests
    {

        private ArrayTools tools;
        private bool shouldStop;

        private const int RandArrMinSize = 5;
        private const int RandArrMaxSize = 20;
        private const int RandStrMinSize = 3;
        private const int RandStrMaxSize = 10;
        private const int RandSeed = 409409409;
        private const int RandTestCasesCount = 97;

        [OneTimeSetUp]
        public void Init()
        {
            tools = new ArrayTools();
            shouldStop = false;
        }

        [TearDown]
        public void TearDown()
        {
            var outcome = TestContext.CurrentContext.Result.Outcome;

            if (outcome == ResultState.Failure || outcome == ResultState.Error)
                shouldStop = true;
        }

        [TestCase(new []{ "The", "big", "cat" }, new []{ "The", "big", "cat" })]
        [TestCase(new []{ "John", "Taylor", "John" }, new []{ "John", "Taylor" })]
        [TestCase(new []{ "Reddit", "Instagram", "Reddit", "Facebook", "Instagram" }, new []{ "Reddit", "Instagram", "Facebook" })]
        public void RemoveDupsTest(string[] arr, string[] expected) =>
            Assert.That(tools.RemoveDups(arr), Is.EqualTo(expected));

        [TestCaseSource(nameof(GetRandom))]
        public void RemoveDupsTestRandom(string[] arr, string[] expected)
        {
            if (shouldStop)
                Assert.Ignore("Previous test failed!");

            RemoveDupsTest(arr, expected);
        }

        private static IEnumerable GetRandom()
        {
            var rand = new Random(RandSeed);

            for (var i = 0; i < RandTestCasesCount; i++)
            {
                var arr = new string[rand.Next(RandArrMinSize, RandArrMaxSize + 1)];
                var dupsCount = rand.Next(arr.Length);

                for (var j = 0; j < arr.Length - dupsCount; j++)
                {
                    var chars = new char[rand.Next(RandStrMinSize, RandStrMaxSize + 1)];

                    for (var k = 0; k < chars.Length; k++)
                        chars[k] = (char) rand.Next('a', 'z' + 1);

                    arr[j] = new string(chars);
                }

                for (var j = arr.Length - dupsCount; j < arr.Length; j++)
                    arr[j] = arr[rand.Next(arr.Length - dupsCount)];

                arr = arr.OrderBy(_ => rand.Next()).ToArray();

                yield return new TestCaseData(arr, arr.Distinct().ToArray());
            }
        }

    }
}
