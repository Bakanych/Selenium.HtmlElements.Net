﻿using System;

using NUnit.Framework;

using Selenium.HtmlElements.Demo.Pages;

namespace Selenium.HtmlElements.Demo.Tests {

    public class JaHomePageTests : BaseWebDriverTest {

        [SetUp]
        public void OpenHomePage() {
            NavigateToUrl();
            ClearCookies();
            NavigateToUrl();
        }

        [Test]
        public void DoSeleniumMagic() {
            On<JaHomePage>().DoSeleniumMagic();
        }

        [Test]
        public void AskQuestion() {
            On<JaHomePage>().TabbedQuestionBox.AskQuestion(Guid.NewGuid().ToString());
        }

        [Test]
        public void OpenBecameAnExpert() {
            On<JaHomePage>().OpenBecameAnExpert();
        }

    }

}