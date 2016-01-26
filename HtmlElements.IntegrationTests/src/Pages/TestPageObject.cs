﻿using System.Collections.Generic;
using HtmlElements.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace HtmlElements.IntegrationTests.Pages {

    public class TestPageObject : HtmlPage {

        public TestPageObject(ISearchContext wrapped) : base(wrapped) {}

        [FindsBy(How = How.CssSelector, Using = "#element-list")]
        public HtmlElement ElementListContainer { get; private set; }

        [FindsBy(How = How.CssSelector, Using = "#element-list li")]
        public IList<HtmlElement> ElementListItems { get; private set; }

        [FindsBy(How = How.CssSelector, Using = "#element-list li"), CacheLookup]
        public IList<HtmlElement> CachedElementListItems { get; private set; } 
    }

}