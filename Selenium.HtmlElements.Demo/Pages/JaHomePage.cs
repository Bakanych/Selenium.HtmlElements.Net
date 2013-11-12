﻿using System;
using System.Collections.Generic;
using System.Threading;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

using Selenium.HtmlElements.Demo.Elements;
using Selenium.HtmlElements.Elements;
using Selenium.HtmlElements.Locators;

#pragma warning disable 649

namespace Selenium.HtmlElements.Demo.Pages {

    internal class JaHomePage : CustomElement {

        [FindsBy(How = How.CssSelector, Using = "a[href='/expert.aspx']")]
        [FindsBy(How = How.CssSelector, Using = "a[href='/professional']")]
        private readonly IHtmlElement _becomeAnExpertLink;

        [FindsBy(How = How.Id, Using = "JA_HeaderHelp")]
        private readonly IHtmlElement _contactAsLink;

        [FindsBy(How = How.CssSelector, Using = "a[id*=lnkQuestionTitle]")]
        private readonly IList<IHtmlElement> _questionLinks;

        [FindsBy(How = How.CssSelector, Using = "a[href$='referFriend.aspx']")]
        private readonly IHtmlElement _tellFriendLink;

        public JaHomePage(ISearchContext wrapped) : base(wrapped) {}

        [FindsBy(How = How.Id, Using = "JA_tabbedQuestionBox")]
        public JaTabbedQuestionBox TabbedQuestionBox { get; private set; }

        [FindsBy(How = How.Custom, Using = "$('#JA_HeaderLogin').get(0)", CustomFinderType = typeof(ByJs))]
        public HtmlElement LinkByJs { get; private set; }

        [FindsBy(How = How.Custom, Using = "$('a').get()", CustomFinderType = typeof(ByJs))]
        public IList<HtmlElement> LinkListByJs { get; private set; } 

        public void OpenContactUs() {
            _contactAsLink.Click();
        }

        public void OpenBecameAnExpert() {
            _becomeAnExpertLink.Click();
        }

        public void DoSeleniumMagic() {
            TabbedQuestionBox.InnerHtml = @"
                <p style='font-size: 40px;'>Some Selenium Magic!!!</p>
                <code>TabbedQuestionBox.InnerHtml = 'Some Selnium magic'</code>";

            Thread.Sleep(TimeSpan.FromSeconds(10));
        }

    }

}