using System;
using System.Collections;
using NUnit.Framework;
using PlantSimulator.Runtime.Parameters;

namespace PlantSimulatorTests.UnitTests.Runtime.Parameters
{
    [TestFixture]
    public class ParameterParserTests
    {
        private IParameterParser parser;

        [SetUp]
        public void Setup()
        {
            parser = new ParameterParser();
        }

        [Test]
        public void Parse_WithZeroArgs_ShouldParse()
        {
            string[] args = new string[0];

            var parameters = parser.Parse(args);

            Assert.NotNull(parameters);
        }

        [TestCaseSource(typeof(ArgumentData), nameof(ArgumentData.Arguments))]
        public void Parse_WithOneArg_ShouldParseAndSetValue(string key, string value)
        {
            string[] args = new string[1];

            args[0] = key + "=" + value;

            var parameters = parser.Parse(args);

            Assert.NotNull(parameters);
            Assert.AreEqual(parameters.SettingsPath, value);
        }

        [TestCaseSource(typeof(ArgumentData), nameof(ArgumentData.InvalidArguments))]
        public void Parse_WithInvalidArg_ShouldThrowException(string arg)
        {
            string[] args = new string[1];
            
            args[0] = arg;

            Assert.DoesNotThrow(() =>
            {
                var parameters = parser.Parse(args);
                Assert.NotNull(parameters);
                Assert.AreEqual("appsettings.json", parameters.SettingsPath);
            });
        }
    }


    internal class ArgumentData
    {
        public static IEnumerable InvalidArguments
        {
            get
            {
                yield return new TestCaseData("settingspath");
                yield return new TestCaseData("settingspath=");
            }
        }
        public static IEnumerable Arguments
        {
            get
            {
                yield return new TestCaseData("settingspath", "notappsettings.json");
            }
        }
    }

}