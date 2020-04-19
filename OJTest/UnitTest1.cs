using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderedJson.Tokenize;

namespace OJTest
{
    [TestClass]
    public class TokenizeTest
    {
        private const string Fliename = "hello.oj";

        [TestMethod]
        public void TestMethod1()
        {
            string buffer = File.ReadAllText(Fliename);
            var tokens = DoTokenize.Tokenize(buffer, Fliename);
            DoTokenize.PrintTokens(tokens);
        }
    }
}
