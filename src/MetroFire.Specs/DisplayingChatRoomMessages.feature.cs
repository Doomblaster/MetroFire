﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.8.1.0
//      SpecFlow Generator Version:1.8.0.0
//      Runtime Version:4.0.30319.17020
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace MetroFire.Specs
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.8.1.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Receiving chat room messages")]
    public partial class ReceivingChatRoomMessagesFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "DisplayingChatRoomMessages.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Receiving chat room messages", "In order to see messages from others\r\nAs a CampFire user\r\nI want to see the messa" +
                    "ges from others", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 6
#line 7
 testRunner.Given("a room called \"Test\"");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Receive a single message")]
        public virtual void ReceiveASingleMessage()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Receive a single message", ((string[])(null)));
#line 10
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 11
 testRunner.When("the message \"Hello world\" is received for room \"Test\"");
#line 12
 testRunner.Then("the message \"Hello world\" should be displayed in room \"Test\"");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Receive multiple messages")]
        public virtual void ReceiveMultipleMessages()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Receive multiple messages", ((string[])(null)));
#line 14
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "Message"});
            table1.AddRow(new string[] {
                        "1",
                        "Ohai"});
            table1.AddRow(new string[] {
                        "2",
                        "Hai"});
            table1.AddRow(new string[] {
                        "3",
                        "Test"});
#line 15
 testRunner.When("the following messages are received for room \"Test\" in order:", ((string)(null)), table1);
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "Message"});
            table2.AddRow(new string[] {
                        "1",
                        "Ohai"});
            table2.AddRow(new string[] {
                        "2",
                        "Hai"});
            table2.AddRow(new string[] {
                        "3",
                        "Test"});
#line 20
 testRunner.Then("the following messages should be displayed in room \"Test\" in order:", ((string)(null)), table2);
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Receive multiple messages multiple times")]
        public virtual void ReceiveMultipleMessagesMultipleTimes()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Receive multiple messages multiple times", ((string[])(null)));
#line 26
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "Message"});
            table3.AddRow(new string[] {
                        "1",
                        "Ohai"});
            table3.AddRow(new string[] {
                        "2",
                        "Hai"});
            table3.AddRow(new string[] {
                        "3",
                        "Test"});
#line 27
 testRunner.When("the following messages are received for room \"Test\" in order:", ((string)(null)), table3);
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "Message"});
            table4.AddRow(new string[] {
                        "4",
                        "Blah"});
            table4.AddRow(new string[] {
                        "5",
                        "Nah"});
            table4.AddRow(new string[] {
                        "6",
                        "Zah"});
#line 32
 testRunner.And("the following messages are received for room \"Test\" in order:", ((string)(null)), table4);
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Message"});
            table5.AddRow(new string[] {
                        "Ohai"});
            table5.AddRow(new string[] {
                        "Hai"});
            table5.AddRow(new string[] {
                        "Test"});
            table5.AddRow(new string[] {
                        "Blah"});
            table5.AddRow(new string[] {
                        "Nah"});
            table5.AddRow(new string[] {
                        "Zah"});
#line 37
 testRunner.Then("the following messages should be displayed in room \"Test\" in order:", ((string)(null)), table5);
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Show disconnections")]
        [NUnit.Framework.CategoryAttribute("backgroundtestscheduler")]
        public virtual void ShowDisconnections()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Show disconnections", new string[] {
                        "backgroundtestscheduler"});
#line 47
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 48
 testRunner.When("the streaming is disconnected for room \"Test\"");
#line 49
 testRunner.Then("room \"Test\" should show that it is disconnected");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Show reconnections")]
        [NUnit.Framework.CategoryAttribute("backgroundtestscheduler")]
        public virtual void ShowReconnections()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Show reconnections", new string[] {
                        "backgroundtestscheduler"});
#line 52
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 53
 testRunner.When("the streaming is disconnected for room \"Test\"");
#line 54
 testRunner.And("the streaming is reconnected for room \"Test\"");
#line 55
 testRunner.Then("room \"Test\" should show that it is connected");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Retrieve old messages when disconnected")]
        [NUnit.Framework.CategoryAttribute("backgroundtestscheduler")]
        public virtual void RetrieveOldMessagesWhenDisconnected()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Retrieve old messages when disconnected", new string[] {
                        "backgroundtestscheduler"});
#line 58
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 59
 testRunner.Given("that streaming has started for room \"Test\"");
#line 60
 testRunner.When("the streaming is disconnected for room \"Test\"");
#line 61
 testRunner.And("the streaming is reconnected for room \"Test\"");
#line 62
 testRunner.And("we wait 20 seconds");
#line 63
 testRunner.Then("older messages should have been requested for room \"Test\"");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Show old messages sent while disconnected")]
        public virtual void ShowOldMessagesSentWhileDisconnected()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Show old messages sent while disconnected", ((string[])(null)));
#line 65
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "Message"});
            table6.AddRow(new string[] {
                        "1",
                        "One"});
            table6.AddRow(new string[] {
                        "2",
                        "Two"});
            table6.AddRow(new string[] {
                        "3",
                        "Three"});
#line 66
 testRunner.Given("that the following messages are received for room \"Test\" in order:", ((string)(null)), table6);
#line 71
 testRunner.When("the streaming is disconnected for room \"Test\"");
#line 72
 testRunner.And("the streaming is reconnected for room \"Test\"");
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "Message"});
            table7.AddRow(new string[] {
                        "6",
                        "Six"});
            table7.AddRow(new string[] {
                        "7",
                        "Seven"});
#line 73
 testRunner.And("the following messages are received for room \"Test\" in order:", ((string)(null)), table7);
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "Message"});
            table8.AddRow(new string[] {
                        "1",
                        "One"});
            table8.AddRow(new string[] {
                        "2",
                        "Two"});
            table8.AddRow(new string[] {
                        "3",
                        "Three"});
            table8.AddRow(new string[] {
                        "4",
                        "Four"});
            table8.AddRow(new string[] {
                        "5",
                        "Five"});
            table8.AddRow(new string[] {
                        "6",
                        "Six"});
            table8.AddRow(new string[] {
                        "7",
                        "Seven"});
#line 77
 testRunner.And("the following messages are received for room \"Test\" in order:", ((string)(null)), table8);
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "Message"});
            table9.AddRow(new string[] {
                        "1",
                        "One"});
            table9.AddRow(new string[] {
                        "2",
                        "Two"});
            table9.AddRow(new string[] {
                        "3",
                        "Three"});
            table9.AddRow(new string[] {
                        "4",
                        "Four"});
            table9.AddRow(new string[] {
                        "5",
                        "Five"});
            table9.AddRow(new string[] {
                        "6",
                        "Six"});
            table9.AddRow(new string[] {
                        "7",
                        "Seven"});
#line 86
 testRunner.Then("the following messages should be displayed in room \"Test\" in order:", ((string)(null)), table9);
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Change topic")]
        public virtual void ChangeTopic()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change topic", ((string[])(null)));
#line 96
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 97
 testRunner.When("the topic is changed to \"To pic\" for room \"Test\"");
#line 98
 testRunner.Then("the topic should be \"To pic\" for room \"Test\"");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
