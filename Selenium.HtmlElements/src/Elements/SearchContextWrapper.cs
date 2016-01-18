﻿using System;
using System.Collections.ObjectModel;
using HtmlElements.Extensions;
using HtmlElements.Locators;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace HtmlElements.Elements {

    public abstract class SearchContextWrapper : ISearchContext, IWrapsDriver, IJavaScriptExecutor {

        private readonly ISearchContext _wrapped;

        protected SearchContextWrapper(ISearchContext wrapped) {
            _wrapped = (wrapped is SearchContextWrapper) ? (wrapped as SearchContextWrapper)._wrapped : wrapped;
        }

        public object ExecuteScript(string script, params object[] args) {
            var jsExecutor = WrappedDriver.ToJavaScriptExecutor();

            if (jsExecutor != null) return jsExecutor.ExecuteScript(script, args);

            throw new InvalidOperationException(string.Format("[{0}] cannot execute JavaScript", this));
        }

        public object ExecuteAsyncScript(string script, params object[] args) {
            var jsExecutor = WrappedDriver.ToJavaScriptExecutor();

            if (jsExecutor != null) return jsExecutor.ExecuteAsyncScript(script, args);

            throw new InvalidOperationException(string.Format("[{0}] cannot execute JavaScript", this));
        }

        public ReadOnlyCollection<IWebElement> FindElements(By @by) {
            return _wrapped.FindElements(@by);
        }

        public IWebElement FindElement(By @by) {
            return _wrapped.FindElement(@by);
        }

        public IWebDriver WrappedDriver {
            get { return _wrapped.ToWebDriver(); }
        }

        protected IElementProvider RelativeLocator(By by) {
            return new ElementProvider(this, by);
        }

        public override string ToString() {
            return string.Format("[{1}] wrapped by [{0}]", GetType(), _wrapped);
        }

    }

}