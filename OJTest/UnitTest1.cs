using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OJApp;
using OrderedJson.Core;
using OrderedJson.Tokenize;

namespace OJTest
{
    [TestClass]
    public class OJRunTest
    {
        private const string Filename = "hello.oj";
        OJParser parser;
        GameContext context;

        [TestInitialize]
        public void Setup()
        {
            context = new GameContext();
            context.hostCard = new Card() { health = 10 };

            Dictionary<string, string> remaps = new Dictionary<string, string>();
            remaps.Add("iftrue", "ifelse:true,$1,$2");

            parser = new OJParser(typeof(CardCommondDefiner), remaps);
        }

        [TestMethod]
        public void RemapTest1()
        {
            string code = "iftrue: {IncreaseHealth:5}, IncreaseHealth:1";
            IOJMethod method = parser.Parse(code, Filename);
            method.Invoke(context);
            Assert.AreEqual(context.hostCard.health, 15);
        }
        [TestMethod]
        public void DefalutArgTest()
        {
            string code = "IncreaseHealth";
            IOJMethod method = parser.Parse(code, Filename);
            method.Invoke(context);
            Assert.AreEqual(context.hostCard.health, 15);
        }
    }
}
